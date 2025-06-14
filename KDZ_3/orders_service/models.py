from sqlalchemy import Column, Integer, String, Float, Enum, Boolean, DateTime
from sqlalchemy.ext.declarative import declarative_base
import datetime
import enum

Base = declarative_base()

class OrderStatus(enum.Enum):
    NEW = "NEW"
    FINISHED = "FINISHED"
    CANCELLED = "CANCELLED"

class Order(Base):
    __tablename__ = "orders"
    id = Column(Integer, primary_key=True, autoincrement=True)
    user_id = Column(String, index=True)
    amount = Column(Float)
    description = Column(String)
    status = Column(Enum(OrderStatus), default=OrderStatus.NEW)
    created_at = Column(DateTime, default=datetime.datetime.utcnow)

class OutboxOrder(Base):
    __tablename__ = "outbox_orders"
    id = Column(Integer, primary_key=True)
    order_id = Column(Integer, index=True)
    user_id = Column(String)
    amount = Column(Float)
    processed = Column(Boolean, default=False)
    created_at = Column(DateTime, default=datetime.datetime.utcnow)
