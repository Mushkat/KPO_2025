import time
import json
import threading
from kafka import KafkaProducer, KafkaConsumer
from db import SessionLocal
from models import PaymentInbox, PaymentOutbox, Account, InboxStatus

KAFKA_BROKERS = ["kafka:29092"]

def process_tasks():
    consumer = KafkaConsumer(
        "payment_tasks",
        bootstrap_servers=KAFKA_BROKERS,
        group_id="payments_service",
        value_deserializer=lambda v: json.loads(v)
    )
    for msg in consumer:
        data = msg.value
        print(f"[PaymentService] Received payment task: {data}")
        session = SessionLocal()
        try:
            existing = session.get(PaymentInbox, data["order_id"])
            if existing:
                print(f"[PaymentService] Task {data['order_id']} already processed")
                continue
            acc = session.get(Account, data["user_id"])
            status = InboxStatus.fail
            if acc and acc.balance >= data["amount"]:
                acc.balance -= data["amount"]
                status = InboxStatus.success
                print(f"[PaymentService] Payment success for order {data['order_id']}")
            else:
                print(f"[PaymentService] Payment failed for order {data['order_id']}")
            session.add(PaymentInbox(order_id=data["order_id"], user_id=data["user_id"], status=status))
            session.add(PaymentOutbox(order_id=data["order_id"], user_id=data["user_id"], status=status))
            session.commit()
        finally:
            session.close()

def send_outbox():
    producer = KafkaProducer(
        bootstrap_servers=KAFKA_BROKERS,
        value_serializer=lambda v: json.dumps(v).encode()
    )
    while True:
        session = SessionLocal()
        try:
            records = session.query(PaymentOutbox).filter_by(processed=False).all()
            for rec in records:
                print(f"[PaymentService] Sending payment result for order {rec.order_id}")
                producer.send("payment_results", {
                    "order_id": rec.order_id,
                    "user_id": rec.user_id,
                    "status": rec.status.value
                })
                rec.processed = True
            session.commit()
        finally:
            session.close()
        time.sleep(2)

def run_payment_workers():
    time.sleep(5)

    threading.Thread(target=process_tasks, daemon=True).start()
    threading.Thread(target=send_outbox, daemon=True).start()
