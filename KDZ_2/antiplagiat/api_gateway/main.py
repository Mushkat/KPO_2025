from fastapi import FastAPI, UploadFile, File
import httpx
from pydantic import BaseModel

app = FastAPI()

SERVICES = {
    "storage": "http://localhost:8001",
    "analysis": "http://localhost:8002"
}

class AnalysisRequest(BaseModel):
    file_id: int
    compare_with: list[int] = []

@app.post("/upload")
async def upload_file(file: UploadFile = File(...)):
    async with httpx.AsyncClient() as client:
        response = await client.post(
            f"{SERVICES['storage']}/upload",
            files={"file": (file.filename, await file.read())}
        )
        return response.json()

@app.post("/analyze")
async def analyze_file(request: AnalysisRequest):
    async with httpx.AsyncClient() as client:
        response = await client.post(
            f"{SERVICES['analysis']}/analyze",
            json=request.dict()
        )
        return response.json()

@app.get("/results/{file_id}")
async def get_results(file_id: int):
    async with httpx.AsyncClient() as client:
        response = await client.get(f"{SERVICES['analysis']}/results/{file_id}")
        return response.json()

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="0.0.0.0", port=8000)