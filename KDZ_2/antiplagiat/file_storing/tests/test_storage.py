import os
from pathlib import Path
from fastapi.testclient import TestClient
from file_storing.main import app, FileRecord, SessionLocal
import pytest

client = TestClient(app)
TEST_FILE = Path(__file__).parent / "test_file.txt"


@pytest.fixture(scope="module", autouse=True)
def setup_and_teardown():
    # Создаем тестовый файл
    with open(TEST_FILE, "w", encoding="utf-8") as f:
        f.write("Это тестовый файл")

    yield

    # Очистка после тестов
    db = SessionLocal()
    db.query(FileRecord).delete()
    db.commit()
    if os.path.exists("uploads/test_file.txt"):
        os.remove("uploads/test_file.txt")
    TEST_FILE.unlink()


def test_upload_file():
    with open(TEST_FILE, "rb") as f:
        response = client.post("/upload", files={"file": ("test_file.txt", f)})
    assert response.status_code == 200
    assert "id" in response.json()


def test_download_file():
    # Сначала загружаем файл
    with open(TEST_FILE, "rb") as f:
        upload_response = client.post("/upload", files={"file": ("test_file.txt", f)})

    file_id = upload_response.json()["id"]
    response = client.get(f"/download/{file_id}")
    assert response.status_code == 200
    assert response.text == "Это тестовый файл"


def test_file_not_found():
    response = client.get("/download/999")
    assert response.status_code == 404
    assert response.json()["detail"] == "File not found"