from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, EmailStr
from typing import Optional

# Relative Imports (want to fix this eventually)
from ...utils.database import db_dependency
from ...models import Course


# Pydantic Model for Course Creation
class CourseBase(BaseModel):
    course_code: str
    course_name: str
    class_num: str
    instructors: str

    meet_no: str
    meet_days: str
    meet_time_begin: str
    meet_time_end: str
    meet_room: str


# Pydantic Model for Course Update
class CourseUpdate(BaseModel):
    course_code: Optional[str] = None
    course_name: Optional[str] = None
    class_num: Optional[str] = None
    instructors: Optional[str] = None

    meet_no: Optional[str] = None
    meet_days: Optional[str] = None
    meet_time_begin: Optional[str] = None
    meet_time_end: Optional[str] = None
    meet_room: Optional[str] = None


# FastAPI Router for Course Routes
router = APIRouter(prefix="/courses", tags=["courses"])

# Create Course
@router.post("/create", status_code=status.HTTP_201_CREATED)
async def create_course(course: CourseBase, db: db_dependency):
    db_object = Course(
        course_code=course.course_code,
        course_name=course.course_name,
        class_num=course.class_num,
        instructors=course.instructors,

        meet_no=course.meet_no,
        meet_days=course.meet_days,
        meet_time_begin=course.meet_time_begin,
        meet_time_end=course.meet_time_end,
        meet_room=course.meet_room
    )    
    db.add(db_object)
    db.commit()


# Get Specific Course (by course_code)
@router.get("/get/{course_code}")
async def get_course(course_code: str, db: db_dependency):
    course = db.query(Course).filter(Course.course_code == course_code).first()
    if not course:
        raise HTTPException(status_code=404, detail="Course not found") 
    
    return course


# Get Specific Course (by course_name)
@router.get("/get/name/{course_name}")
async def get_course(course_name: str, db: db_dependency):
    course = db.query(Course).filter(Course.course_name == course_name).all()
    if not course:
        raise HTTPException(status_code=404, detail="Course not found") 
    
    return course


# Update Specific Course
@router.patch("/update/{course_code}")
async def update_course(course_code: str, course: CourseUpdate, db: db_dependency):
    existing_course = db.query(Course).filter(Course.course_code == course_code).first()
    if not existing_course:
        raise HTTPException(status_code=404, detail="Course not found")

    for key, value in course.model_dump().items():
        if value:
            setattr(existing_course, key, value)

    db.commit()
    return existing_course


# Delete Specific Course
@router.delete("/delete/{course_code}")
async def delete_course(course_code: str, db: db_dependency):
    course = db.query(Course).filter(Course.course_code == course_code).first()
    if not course:
        raise HTTPException(status_code=404, detail="Course not found")
    
    db.delete(course)
    db.commit()
    return {"message": "Course deleted successfully"}


# Get All Courses
@router.get("/all")
async def get_all_courses(db: db_dependency):
    courses = db.query(Course).all()
    return courses