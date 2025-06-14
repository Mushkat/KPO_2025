import json
import threading
import time
from kafka import KafkaProducer, KafkaConsumer
from db import SessionLocal
from models import OutboxOrder, Order, OrderStatus

KAFKA_BROKERS = ["kafka:29092"]

def send_outbox():
    producer = KafkaProducer(
        bootstrap_servers=KAFKA_BROKERS,
        value_serializer=lambda v: json.dumps(v).encode()
    )
    while True:
        session = SessionLocal()
        try:
            tasks = session.query(OutboxOrder).filter_by(processed=False).all()
            for task in tasks:
                print(f"[OrderService] Sending order {task.order_id} to payment_tasks")
                producer.send("payment_tasks", {
                    "order_id": task.order_id,
                    "user_id": task.user_id,
                    "amount": task.amount
                })
                task.processed = True
            session.commit()
        finally:
            session.close()
        time.sleep(2)

def handle_results():
    consumer = KafkaConsumer(
        "payment_results",
        bootstrap_servers=KAFKA_BROKERS,
        group_id="orders_service",
        value_deserializer=lambda v: json.loads(v)
    )
    for msg in consumer:
        data = msg.value
        print(f"[OrderService] Received payment result: {data}")
        session = SessionLocal()
        try:
            order = session.query(Order).get(data["order_id"])
            if order:
                order.status = (
                    OrderStatus.FINISHED if data["status"] == "success" else OrderStatus.CANCELLED
                )
                session.commit()
                print(f"[OrderService] Order {order.id} updated to status {order.status}")
        finally:
            session.close()

def run_order_workers():
    time.sleep(5)
    threading.Thread(target=handle_results, daemon=True).start()
    threading.Thread(target=send_outbox, daemon=True).start()
