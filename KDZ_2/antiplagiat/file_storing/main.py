from fastapi import FastAPI, UploadFile, File, HTTPException
from fastapi.responses import FileResponse
from sqlalchemy import create_engine, Column, Integer, String, DateTime
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from datetime import datetime
import os
from pathlib import Path

app = FastAPI()
Base = declarative_base()

# Конфигурация
BASE_DIR = Path(__file__).parent
UPLOAD_DIR = BASE_DIR / "uploads"
UPLOAD_DIR.mkdir(exist_ok=True)
DATABASE_URL = f"sqlite:///{BASE_DIR}/file_storage.db"


# Модель БД
class FileRecord(Base):
    __tablename__ = 'files'
    id = Column(Integer, primary_key=True, autoincrement=True)
    filename = Column(String)
    filepath = Column(String)
    upload_date = Column(DateTime)
    size = Column(Integer)


# Инициализация БД
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)
Base.metadata.create_all(bind=engine)


@app.post("/upload")
async def upload_file(file: UploadFile = File(...)):
    if not file.filename.endswith('.txt'):
        raise HTTPException(400, "Only .txt files allowed")

    file_location = UPLOAD_DIR / file.filename
    content = await file.read()

    # Сохранение файла
    with open(file_location, "wb") as f:
        f.write(content)

    # Сохранение метаданных
    db = SessionLocal()
    try:
        record = FileRecord(
            filename=file.filename,
            filepath=str(file_location),
            upload_date=datetime.now(),
            size=len(content)
        )
        db.add(record)
        db.commit()
        return {"id": record.id, "filename": record.filename}
    finally:
        db.close()


@app.get("/download/{file_id}")
async def download_file(file_id: int):
    db = SessionLocal()
    try:
        file_record = db.query(FileRecord).filter(FileRecord.id == file_id).first()
        if not file_record:
            raise HTTPException(status_code=404, detail="File not found")

        if not os.path.exists(file_record.filepath):
            raise HTTPException(status_code=404, detail="File content missing")

        return FileResponse(
            path=file_record.filepath,
            filename=file_record.filename,
            media_type='text/plain'
        )
    finally:
        db.close()


if __name__ == "__main__":
    import uvicorn

    uvicorn.run(app, host="0.0.0.0", port=8001)