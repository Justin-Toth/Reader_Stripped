from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, EmailStr
from typing import Optional

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


# FastAPI Router for Student Routes
router = APIRouter(prefix="/students", tags=["students"])

# Create Student
@router.post("/create", status_code=status.HTTP_201_CREATED)
async def create_student(student: StudentBase, db: db_dependency):
    db_object = Student(
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
    db.add(db_object)
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