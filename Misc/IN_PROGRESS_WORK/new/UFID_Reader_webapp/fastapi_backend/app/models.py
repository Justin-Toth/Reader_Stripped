from sqlalchemy import Column, Integer, Boolean, String
from .utils.database import Base

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


class Kiosk(Base):
    __tablename__ = "kiosks"

    serial_num = Column(String(16), unique=True, primary_key=True, index=True)
    room_num = Column(String(8))


class Exam(Base):
    __tablename__ = "exams"

    course_code = Column(String(8), primary_key=True, index=True)
    course_name = Column(String(255))
    instructors = Column(String(255))
    sections = Column(String(255))
    room = Column(String(8))
    date = Column(String(16))
    start_time = Column(String(16))
    end_time = Column(String(16))


class Course(Base):
    __tablename__ = "courses"

    course_code = Column(String(8), index=True)
    course_name = Column(String(255))
    class_num = Column(String(5), primary_key=True, index=True)
    instructors = Column(String(255))
    meet_no = Column(String(1), primary_key=True, index=True)
    meet_days = Column(String(16))
    meet_time_begin = Column(String(16))
    meet_time_end = Column(String(16))
    meet_room = Column(String(8))
    
