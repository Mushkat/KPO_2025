from fastapi import FastAPI
from pydantic import BaseModel
import httpx
import os

app = FastAPI()

ORDERS = os.getenv("ORDERS_URL", "http://orders_service:8001")
PAYMENTS = os.getenv("PAYMENTS_URL", "http://payments_service:8002")

class CreateAccountReq(BaseModel):
    user_id: str

class TopUpReq(BaseModel):
    user_id: str
    amount: float

class OrderCreate(BaseModel):
    user_id: str
    amount: float
    description: str

@app.post("/orders")
async def create_order(body: OrderCreate):
    async with httpx.AsyncClient() as client:
        r = await client.post(f"{ORDERS}/orders", json=body.dict())
        r.raise_for_status()
        return r.json()

@app.get("/orders")
async def get_orders(user_id: str):
    async with httpx.AsyncClient() as client:
        r = await client.get(f"{ORDERS}/orders", params={"user_id": user_id})
        r.raise_for_status()
        return r.json()

@app.get("/orders/{order_id}")
async def get_order(order_id: int):
    async with httpx.AsyncClient() as client:
        r = await client.get(f"{ORDERS}/orders/{order_id}")
        r.raise_for_status()
        return r.json()

@app.post("/accounts")
async def create_account(body: CreateAccountReq):
    async with httpx.AsyncClient() as client:
        r = await client.post(f"{PAYMENTS}/accounts", json=body.dict())
        r.raise_for_status()
        return r.json()

@app.post("/accounts/topup")
async def topup(body: TopUpReq):
    async with httpx.AsyncClient() as client:
        r = await client.post(f"{PAYMENTS}/accounts/topup", json=body.dict())
        r.raise_for_status()
        return r.json()

@app.get("/accounts/{user_id}")
async def get_balance(user_id: str):
    async with httpx.AsyncClient() as client:
        r = await client.get(f"{PAYMENTS}/accounts/{user_id}")
        r.raise_for_status()
        return r.json()
