from fastapi.testclient import TestClient
from api_gateway.main import app
from unittest.mock import patch, AsyncMock
import pytest

client = TestClient(app)


@pytest.mark.asyncio
async def test_upload_file():
    with patch("httpx.AsyncClient.post") as mock_post:
        mock_post.return_value = AsyncMock(
            status_code=200,
            json=lambda: {"id": 1, "filename": "test.txt"}
        )

        response = client.post(
            "/upload",
            files={"file": ("test.txt", "some content")}
        )

        assert response.status_code == 200
        assert response.json()["id"] == 1


@pytest.mark.asyncio
async def test_analyze_file():
    with patch("httpx.AsyncClient.post") as mock_post:
        mock_post.return_value = AsyncMock(
            status_code=200,
            json=lambda: {"analysis_id": 123}
        )

        response = client.post(
            "/analyze",
            json={"file_id": 1, "compare_with": [2]}
        )

        assert response.status_code == 200
        assert "analysis_id" in response.json()