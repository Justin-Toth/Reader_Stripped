from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, EmailStr
from typing import Optional

# Relative Imports (want to fix this eventually)
from ...utils.database import db_dependency
from ...models import Exam

# Pydantic Model for Exam Creation
# May need to change as db should have all data inited and we just need to update time to "create" an exam
class ExamBase(BaseModel):
    course_code: str
    course_name: str
    instructors: str
    sections: str
    room: str
    date: str
    start_time: str
    end_time: str

class ExamUpdate(BaseModel):
    # Only fields that can be updated
    room: Optional[str] = None
    date: Optional[str] = None
    start_time: Optional[str] = None
    end_time: Optional[str] = None


# FastAPI Router for Exam Routes
router = APIRouter(prefix="/exams", tags=["exams"])

# Create Exam
@router.post("/create", status_code=status.HTTP_201_CREATED)
async def create_exam(exam: ExamBase, db: db_dependency):
    # Check if exam already exists
    existing_exam = db.query(Exam).filter(Exam.course_code == exam.course_code).first()
    if existing_exam:
        raise HTTPException(
            status_code=status.HTTP_400_BAD_REQUEST,
            detail=f"Exam with course code {exam.course_code} already exists."
        )
    
    # Create new exam entry
    new_Exam = Exam(
        course_code=exam.course_code,
        course_name=exam.course_name,
        instructors=exam.instructors,
        sections=exam.sections,
        room=exam.room,
        date=exam.date,
        start_time=exam.start_time,
        end_time=exam.end_time
    )    
    db.add(new_Exam)
    db.commit()


# Get Specific Exam (by course_code)
@router.get("/get/{course_code}")
async def get_exam(course_code: str, db: db_dependency):
    exam = db.query(Exam).filter(Exam.course_code == course_code).all()
    if not exam:
        raise HTTPException(status_code=404, detail="Exam not found") 
    
    return exam

# Get Specific Exam (by room number)
@router.get("/get/room/{room_num}")
async def get_exam(room_num: str, db: db_dependency):
    exam = db.query(Exam).filter(Exam.room == room_num).all()
    if not exam:
        raise HTTPException(status_code=404, detail="Exam not found") 
    
    return exam

# Get Specific Exam (by date) (needs fixing cuase date format contains slashes)
@router.get("/get/date/{date}")
async def get_exam(date: str, db: db_dependency):
    exam = db.query(Exam).filter(Exam.date == date).all()
    if not exam:
        raise HTTPException(status_code=404, detail="Exam not found") 
    
    return exam


# Update Specific Exam
@router.patch("/update/{course_code}")
async def update_exam(course_code: str, exam: ExamUpdate, db: db_dependency):
    existing_exam = db.query(Exam).filter(Exam.course_code == course_code).first()
    if not existing_exam:
        raise HTTPException(status_code=404, detail="Exam not found")

    for key, value in exam.model_dump().items():
        if value:
            setattr(existing_exam, key, value)

    db.commit()
    return existing_exam


# Clear the Exam Time for an Exam
@router.patch("/clear/{course_code}")
async def clear_exam(course_code: str, db: db_dependency):
    existing_exam = db.query(Exam).filter(Exam.course_code == course_code).first()
    if not existing_exam:
        raise HTTPException(status_code=404, detail="Exam not found")

    # Clear the time fields
    existing_exam.room = None
    existing_exam.date = None
    existing_exam.start_time = None
    existing_exam.end_time = None

    db.commit()
    return existing_exam


# Delete Specific Exam (maybe delete the exam info not the exam row in the table)
@router.delete("/delete/{course_code}")
async def delete_exam(course_code: str, db: db_dependency):
    exam = db.query(Exam).filter(Exam.course_code == course_code).first()
    if not exam:
        raise HTTPException(status_code=404, detail="Exam not found")
    
    db.delete(exam)
    db.commit()
    return {"message": "Exam deleted successfully"}


# Get All Exams
@router.get("/all")
async def get_all_exams(db: db_dependency):
    exams = db.query(Exam).all()
    return exams