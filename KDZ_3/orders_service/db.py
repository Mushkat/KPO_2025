from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker

engine = create_engine("postgresql://user:pass@postgres_orders:5432/orders")
SessionLocal = sessionmaker(bind=engine)
