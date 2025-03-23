from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, EmailStr
from typing import Optional

from sqlalchemy import null

# Relative Imports (want to fix this eventually)
from ...utils.database import db_dependency
from ...models import Student

# Pydantic Model for Student Creation   
class StudentBase(BaseModel):
    ufid: str
    iso: str
    full_name: str
    email: EmailStr

    course1: Optional[str] = None
    course2: Optional[str] = None
    course3: Optional[str] = None
    course4: Optional[str] = None
    course5: Optional[str] = None


class StudentUpdate(BaseModel):
    ufid: Optional[str] = None
    iso: Optional[str] = None
    full_name: Optional[str] = None
    email: Optional[EmailStr] = None

    course1: Optional[str] = None
    course2: Optional[str] = None
    course3: Optional[str] = None
    course4: Optional[str] = None
    course5: Optional[str] = None


class StudentUpdateCourses(BaseModel):
    course1: Optional[str] = None
    course2: Optional[str] = None
    course3: Optional[str] = None
    course4: Optional[str] = None
    course5: Optional[str] = None

# FastAPI Router for Student Routes
router = APIRouter(prefix="/students", tags=["students"])

# Create Student
@router.post("/create", status_code=status.HTTP_201_CREATED)
async def create_student(student: StudentBase, db: db_dependency):
    # Check if student already exists
    existing_student = db.query(Student).filter(Student.ufid == student.ufid).first()
    if existing_student:
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail=f"Student with UFID {student.ufid} already exists."
        )

    # Create new student entry
    new_Student = Student(
        ufid=student.ufid,
        iso=student.iso,
        full_name=student.full_name,
        email=student.email,

        course1=student.course1,
        course2=student.course2,
        course3=student.course3,
        course4=student.course4,
        course5=student.course5
    )    
    db.add(new_Student)
    db.commit()


# Get Specific Student
@router.get("/get/{ufid}")
async def get_student(ufid: str, db: db_dependency):
    student = db.query(Student).filter(Student.ufid == ufid).first()
    if not student:
        raise HTTPException(status_code=404, detail="Student not found")
    
    return student


# Update Specific Student
@router.patch("/update/{ufid}")
async def update_student(ufid: str, student: StudentUpdate, db: db_dependency):
    existing_student = db.query(Student).filter(Student.ufid == ufid).first()
    if not existing_student:
        raise HTTPException(status_code=404, detail="Student not found")
    
    for key, value in student.model_dump().items():
        if value:
            setattr(existing_student, key, value)
    
    db.commit()
    return existing_student


# Update Specific Student Courses Only
@router.patch("/update/courses/{ufid}")
async def update_student_courses(ufid: str, student_courses: StudentUpdateCourses, db: db_dependency):
    existing_student = db.query(Student).filter(Student.ufid == ufid).first()
    if not existing_student:
        raise HTTPException(status_code=404, detail="Student not found")
    
    # if value is None, set field to None, otherwise update with new value
    # Data from the form is either the course value or empty
    # If empty, we set it to None in the database (No course)
    # Otherwise we update/retain the course value
    for key, value in student_courses.model_dump().items():
        if value is not None:
            setattr(existing_student, key, value)
        else: 
            setattr(existing_student, key, None)
    
    db.commit()
    return existing_student


# Delete Specific Student
@router.delete("/delete/{ufid}")
async def delete_student(ufid: str, db: db_dependency):
    student = db.query(Student).filter(Student.ufid == ufid).first()
    if not student:
        raise HTTPException(status_code=404, detail="Student not found")
    
    db.delete(student)
    db.commit()
    return {"message": "Student Deleted"}


# Get All Students
@router.get("/all")
async def get_all_students(db: db_dependency):
    students = db.query(Student).all()
    return students