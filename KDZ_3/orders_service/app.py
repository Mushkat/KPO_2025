from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, ConfigDict
from models import Base, Order, OutboxOrder, OrderStatus
from db import SessionLocal, engine
from worker import run_order_workers

Base.metadata.create_all(bind=engine)

app = FastAPI()

class OrderCreate(BaseModel):
    user_id: str
    amount: float
    description: str

    model_config = ConfigDict(from_attributes=True)

class OrderResponse(BaseModel):
    id: int
    user_id: str
    amount: float
    description: str
    status: OrderStatus

    model_config = ConfigDict(from_attributes=True)

@app.on_event("startup")
def startup_event():
    run_order_workers()

@app.post("/orders", response_model=OrderResponse)
def create_order(req: OrderCreate):
    session = SessionLocal()
    try:
        order = Order(user_id=req.user_id, amount=req.amount, description=req.description)
        session.add(order)
        session.flush()
        session.add(OutboxOrder(order_id=order.id, user_id=req.user_id, amount=req.amount))
        session.commit()
        return OrderResponse.from_orm(order)
    except:
        session.rollback()
        raise
    finally:
        session.close()

@app.get("/orders", response_model=list[OrderResponse])
def list_orders(user_id: str):
    session = SessionLocal()
    orders = session.query(Order).filter(Order.user_id == user_id).all()
    session.close()
    return [OrderResponse.from_orm(o) for o in orders]

@app.get("/orders/{order_id}", response_model=OrderResponse)
def get_order(order_id: int):
    session = SessionLocal()
    order = session.query(Order).get(order_id)
    session.close()
    if not order:
        raise HTTPException(404, "Not found")
    return OrderResponse.from_orm(order)

@app.post("/orders/update_status")
async def update_order_status(order_id: int, status: str):
    db = SessionLocal()
    order = db.query(Order).filter(Order.id == order_id).first()
    if not order:
        raise HTTPException(status_code=404, detail="Order not found")
    order.status = OrderStatus(status)
    db.commit()
    db.refresh(order)
    return {"message": "Order status updated"}
