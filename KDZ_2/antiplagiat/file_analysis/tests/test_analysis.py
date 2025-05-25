import pytest
from fastapi.testclient import TestClient
from file_analysis.main import app, AnalysisResult, SessionLocal
from unittest.mock import AsyncMock, patch

client = TestClient(app)


@pytest.fixture(scope="function", autouse=True)
def clear_db():
    db = SessionLocal()
    db.query(AnalysisResult).delete()
    db.commit()
    yield
    db.query(AnalysisResult).delete()
    db.commit()


@pytest.mark.asyncio
async def test_analyze_file():
    with patch("httpx.AsyncClient.get") as mock_get:
        mock_get.return_value = AsyncMock(
            status_code=200,
            text="Пример текста для анализа"
        )

        response = client.post(
            "/analyze",
            json={"file_id": 1, "compare_with": [2, 3]}
        )

        assert response.status_code == 200
        assert "analysis_id" in response.json()


def test_get_results():
    # Сначала создаем тестовый результат
    db = SessionLocal()
    db.add(AnalysisResult(
        file_id=1,
        paragraphs=2,
        words=10,
        characters=50,
        similarity='{"2": 0.5}'
    ))
    db.commit()

    response = client.get("/results/1")
    assert response.status_code == 200
    assert response.json()["file_id"] == 1
    assert response.json()["stats"]["words"] == 10


def test_results_not_found():
    response = client.get("/results/999")
    assert response.status_code == 404