from fastapi import FastAPI, HTTPException
from pydantic import BaseModel
from db import SessionLocal, engine
from models import Base, Account
from worker import run_payment_workers

Base.metadata.create_all(bind=engine)

app = FastAPI()

class CreateAccountReq(BaseModel):
    user_id: str

@app.on_event("startup")
def startup_event():
    run_payment_workers()

@app.post("/accounts")
def create_account(req: CreateAccountReq):
    session = SessionLocal()
    if session.get(Account, req.user_id):
        session.close()
        raise HTTPException(400, "Already exists")
    session.add(Account(user_id=req.user_id))
    session.commit()
    session.close()
    return {"user_id": req.user_id}

class TopUpReq(BaseModel):
    user_id: str
    amount: float

@app.post("/accounts/topup")
def topup(req: TopUpReq):
    session = SessionLocal()
    acc = session.get(Account, req.user_id)
    if not acc:
        session.close()
        raise HTTPException(404, "No account")
    acc.balance += req.amount
    session.commit()
    bal = acc.balance
    session.close()
    return {"balance": bal}

@app.get("/accounts/{user_id}")
def get_balance(user_id: str):
    session = SessionLocal()
    acc = session.get(Account, user_id)
    session.close()
    if not acc:
        raise HTTPException(404, "No account")
    return {"balance": acc.balance}
