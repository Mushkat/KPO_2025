# Документация к системе антиплагиата

## Описание

Система предназначена для анализа студенческих работ на предмет плагиата. Основные возможности:

- Подсчёт статистики (количество абзацев, слов, символов)

- Сравнение файлов на схожесть

- Генерация облака слов (визуализация)

## Архитектура 

```
┌─────────────┐     ┌──────────────┐      ┌───────────────┐
│ API Gateway │ ◄──►│ File Storage │ ◄──► │ File Analysis │
└─────────────┘     └──────────────┘      └───────────────┘
       ▲
       │
       ▼
┌─────────────┐
│   Клиент    │
└─────────────┘
```

## Описание компонентов

### 1. API Gateway (порт 8000)

- Единственная точка входа для клиентов

- Маршрутизация запросов к сервисам

### 2. File Storage Service (порт 8001)

- Хранение загруженных файлов (.txt)

- Управление данными файлов

### 3. File Analysis Service (порт 8002)

- Анализ текстов

- Сравнение файлов

- Генерация статистики

## Запуск

1. Установка зависимостей

```bash
python -m venv .venv
.\.venv\Scripts\activate
pip install -r requirements.txt
```

2. Запуск сервисов в разных терминалах

```bash
# File Storage Service
cd file_storing
python main.py

# File Analysis Service
cd ../file_analysis
python main.py

# API Gateway
cd ../api_gateway
python main.py
```
![img.png](images/img3.png)
![img_1.png](images/img_1.png)
![img_2.png](images/img_2.png)
## Спецификация API

### API Gateway (http://localhost:8000)

1. Загрузка файлов
```
POST /upload
Content-Type: multipart/form-data

Параметры:
- file: текстовый файл (.txt)

Ответ:
{
  "id": int,       # ID файла
  "filename": str   # Имя файла
}
```
2. Анализ файлов
```POST /analyze
Content-Type: application/json

Тело запроса:
{
  "file_id": int,          # ID анализируемого файла
  "compare_with": [int]    # Список ID для сравнения
}

Ответ:
{
  "analysis_id": int  # ID анализа
}
```
3. Получение результатов по id сравниваемого файла
```
GET /results/{file_id}
Ответ:
{
  "file_id": int,
  "stats": {
    "paragraphs": int,
    "words": int,
    "characters": int
  },
  "similarity": {
    "file_id": float  # Процент схожести
  }
}
```
## Тестирование

Запуск тестов с проверкой покрытия
```bash
pytest --cov --cov-report=html
```
![img.png](images/img.png)
![img.png](images/img2.png)

## Пример работы с помощью Swagger
### Порт API gateway
1. Загрузка нескольких файлов

![img.png](images/img_ex.png)

2. Анализ файла

![img_1.png](images/img_ex_1.png)

3. Получение результата анализа по id файла

![img_2.png](images/img_ex_2.png)

### В swagger file_storing доступна возможность загрузки файла по его id
![img_3.png](images/img_ex_3.png)

## Примеры баз данных
### file_storage.db

![img_4.png](images/img_ex_4.png)

### file_analysis.db

![img_5.png](images/img_ex_5.png)