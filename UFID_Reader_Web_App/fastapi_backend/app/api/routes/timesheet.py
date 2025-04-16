from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel
from typing import Optional

from sqlalchemy import null

# Relative Imports (want to fix this eventually)
from ...utils.database import db_dependency
from ...models import Timesheet

# Pydantic Model for Timesheet
class TimesheetBase(BaseModel): 
    ufid: str
    full_name: str
    course_code : str
    section_num : str
    date : str
    swipe_time : str


# FastAPI Router for Timesheet Routes
router = APIRouter(prefix="/timesheet", tags=["timesheet"])

# Get all Timesheet Entries for Course Code and Section Number
@router.get("/{course_code}/{section_num}", response_model=TimesheetBase)
async def get_timesheet(course_code: str, section_num: str, db: db_dependency):
    timesheet = db.query(Timesheet).filter(Timesheet.course_code == course_code, Timesheet.section_num == section_num).all()
    if not timesheet:
        raise HTTPException(
            status_code=status.HTTP_404_NOT_FOUND,
            detail=f"Timesheet entry for Course Code {course_code} and Section Number {section_num} not found."
        )
    return timesheet


# Get All Timesheet Entries
@router.get("/all", response_model=list[TimesheetBase])
async def get_all_timesheets(db: db_dependency):
    timesheets = db.query(Timesheet).all()
    return timesheets