from sqlalchemy import Column, Integer, Boolean, String
from .utils.database import Base

""" Later
class Course(Base):
    __tablename__ = "courses"

    id = Column(Integer, primary_key=True, index=True)
    name = Column(String, index=True)
    description = Column(String, index=True)
"""

class User(Base):
    __tablename__ = "users"

    id = Column(Integer, primary_key=True, index=True)
    email = Column(String(50), unique=True)
    username = Column(String(50), unique=True)
    full_name = Column(String(50))
    is_admin = Column(Boolean) # If not admin, only have professor privileges
    password = Column(String(64))


class Student(Base):
    __tablename__ = "students"

    # Student Information
    ufid = Column(String(8), unique=True, primary_key=True, index=True)
    iso = Column(String(16))
    full_name = Column(String(50))
    email = Column(String(50))

    # Course IDs. eg: CEN3908C
    course1 = Column(String(8))
    course2 = Column(String(8))
    course3 = Column(String(8))
    course4 = Column(String(8))
    course5 = Column(String(8))
