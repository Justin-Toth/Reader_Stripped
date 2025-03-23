from typing import Annotated
from fastapi import Depends
from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker, Session
from ..utils.security import get_password_hash

# Local Docker Database URL (SHOULD BE MOVED TO A CONFIG FILE)
DATABASE_URL = "mysql+pymysql://myuser:mypass@127.0.0.1:3306/ufid_database"

engine = create_engine(DATABASE_URL)
Base = declarative_base()
SessionLocal = sessionmaker(autocommit=False, autoflush=False, bind=engine)


def init_db():
    Base.metadata.create_all(bind=engine)

    # Check if admin user exists, if not create one
    from ..models import User
    db = SessionLocal()
    
    try:
        user = db.query(User).filter(User.email == "admin1@ufl.edu").first()
        if not user:
            # Admin Information (SHOULD BE MOVED TO A CONFIG FILE)
            admin = User(
                email="admin1@ufl.edu",
                username="admin1",
                password="admin1",
                full_name="Admin User",
                is_admin=True,
            )   
            # Hash the password before storing
            admin.password = get_password_hash(admin.password)
            db.add(admin)
            db.commit()
            print("Admin User Created")
    finally:
        db.close()



def get_db():
    database = SessionLocal()
    try:
        yield database
    finally:
        database.close()

db_dependency = Annotated[Session, Depends(get_db)]
