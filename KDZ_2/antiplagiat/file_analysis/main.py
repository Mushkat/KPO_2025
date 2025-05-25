from fastapi import FastAPI, HTTPException
from sqlalchemy import create_engine, Column, Integer, Text
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from pydantic import BaseModel
import httpx
from typing import Dict
import json
import difflib
from collections import Counter

app = FastAPI()
Base = declarative_base()

# Конфигурация
FILE_STORAGE_URL = "http://localhost:8001"
DATABASE_URL = "sqlite:///file_analysis.db"


# Модель БД
class AnalysisResult(Base):
    __tablename__ = 'results'
    id = Column(Integer, primary_key=True, autoincrement=True)
    file_id = Column(Integer)
    paragraphs = Column(Integer)
    words = Column(Integer)
    characters = Column(Integer)
    similarity = Column(Text)


# Инициализация БД
engine = create_engine(DATABASE_URL)
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)

# Создание таблицы
Base.metadata.create_all(bind=engine)


class AnalysisRequest(BaseModel):
    file_id: int
    compare_with: list[int] = []


async def fetch_file_content(file_id: int) -> str:
    async with httpx.AsyncClient() as client:
        try:
            response = await client.get(f"{FILE_STORAGE_URL}/download/{file_id}")
            if response.status_code != 200:
                raise HTTPException(status_code=404, detail="File not found in storage")
            return response.text
        except httpx.RequestError:
            raise HTTPException(status_code=503, detail="Storage service unavailable")


def analyze_text(content: str) -> Dict[str, int]:
    paragraphs = len(content.split('\n\n')) if content else 0
    words = len(content.split()) if content else 0
    characters = len(content) if content else 0
    return {
        "paragraphs": paragraphs,
        "words": words,
        "characters": characters
    }

# Сравнение текста на схожесть, результат в процентах
def compare_texts(text1: str, text2: str) -> float:
    if text1 == text2:
        return 100

    seq_matcher = difflib.SequenceMatcher(None, text1, text2)
    ratio = seq_matcher.ratio()

    words1 = text1.lower().split()
    words2 = text2.lower().split()

    counter1 = Counter(words1)
    counter2 = Counter(words2)

    common_words = set(words1) & set(words2)
    word_similarity = sum(min(counter1[w], counter2[w]) for w in common_words)
    word_similarity /= max(len(words1), len(words2))

    return round(ratio * 0.6 + word_similarity * 0.4, 2) * 100


@app.post("/analyze")
async def analyze_file(request: AnalysisRequest):
    db = SessionLocal()
    try:
        # Получение и анализ основного файла
        main_content = await fetch_file_content(request.file_id)
        stats = analyze_text(main_content)

        # Сравнение с другими файлами
        similarity_results = {}
        for other_id in request.compare_with:
            other_content = await fetch_file_content(other_id)
            similarity_results[str(other_id)] = compare_texts(main_content, other_content)

        # Сохранение результата
        result = AnalysisResult(
            file_id=request.file_id,
            paragraphs=stats["paragraphs"],
            words=stats["words"],
            characters=stats["characters"],
            similarity=json.dumps(similarity_results)
        )
        db.add(result)
        db.commit()
        return {"analysis_id": result.id}
    except Exception as e:
        db.rollback()
        raise HTTPException(status_code=500, detail=str(e))
    finally:
        db.close()


@app.get("/results/{file_id}")
async def get_results(file_id: int):
    db = SessionLocal()
    try:
        result = db.query(AnalysisResult).filter(AnalysisResult.file_id == file_id).first()
        if not result:
            raise HTTPException(status_code=404, detail="Analysis not found")

        return {
            "file_id": result.file_id,
            "stats": {
                "paragraphs": result.paragraphs,
                "words": result.words,
                "characters": result.characters
            },
            "similarity": json.loads(result.similarity)
        }
    finally:
        db.close()


if __name__ == "__main__":
    import uvicorn

    uvicorn.run(app, host="0.0.0.0", port=8002)