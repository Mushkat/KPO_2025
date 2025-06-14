from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

engine = create_engine("postgresql://user:pass@postgres_payments:5432/payments")
SessionLocal = sessionmaker(bind=engine)
