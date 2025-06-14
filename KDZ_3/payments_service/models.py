from sqlalchemy import Column, String, Float, Integer, Boolean, DateTime, Enum
from sqlalchemy.ext.declarative import declarative_base
import enum, datetime

Base = declarative_base()

class InboxStatus(enum.Enum):
    success = "success"
    fail = "fail"

class PaymentInbox(Base):
    __tablename__ = "payment_inbox"
    order_id = Column(Integer, primary_key=True)
    user_id = Column(String)
    status = Column(Enum(InboxStatus))
    processed = Column(Boolean, default=False)
    created_at = Column(DateTime, default=datetime.datetime.utcnow)

class PaymentOutbox(Base):
    __tablename__ = "payment_outbox"
    order_id = Column(Integer, primary_key=True)
    user_id = Column(String)
    status = Column(Enum(InboxStatus))
    processed = Column(Boolean, default=False)
    created_at = Column(DateTime, default=datetime.datetime.utcnow)

class Account(Base):
    __tablename__ = "accounts"
    user_id = Column(String, primary_key=True)
    balance = Column(Float, default=0.0)
