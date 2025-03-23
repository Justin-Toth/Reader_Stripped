from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, EmailStr
from typing import Optional

# Relative Imports (want to fix this eventually)
from ...utils.database import db_dependency
from ...models import Kiosk

# Pydantic Model for Kiosk Creation
class KioskBase(BaseModel):
    serial_num: str
    room_num: str

class KioskUpdate(BaseModel):
    serial_num: Optional[str] = None
    room_num: Optional[str] = None


# FastAPI Router for Kiosk Routes
router = APIRouter(prefix="/kiosks", tags=["kiosks"])

# Create Kiosk
@router.post("/create", status_code=status.HTTP_201_CREATED)
async def create_kiosk(kiosk: KioskBase, db: db_dependency):
    db_object = Kiosk(
        serial_num=kiosk.serial_num,
        room_num=kiosk.room_num
    )    
    db.add(db_object)
    db.commit()


# Get Specific Kiosk
@router.get("/get/{serial_num}")
async def get_kiosk(serial_num: str, db: db_dependency):
    kiosk = db.query(Kiosk).filter(Kiosk.serial_num == serial_num).first()
    if not kiosk:
        raise HTTPException(status_code=404, detail="Kiosk not found") 
    
    return kiosk


# Update Specific Kiosk
@router.patch("/update/{serial_num}")
async def update_kiosk(serial_num: str, kiosk: KioskUpdate, db: db_dependency):
    existing_kiosk = db.query(Kiosk).filter(Kiosk.serial_num == serial_num).first()
    if not existing_kiosk:
        raise HTTPException(status_code=404, detail="Kiosk not found")

    for key, value in kiosk.model_dump().items():
        if value:
            setattr(existing_kiosk, key, value)

    db.commit()
    return existing_kiosk


# Delete Specific Kiosk
@router.delete("/delete/{serial_num}")
async def delete_kiosk(serial_num: str, db: db_dependency):
    kiosk = db.query(Kiosk).filter(Kiosk.serial_num == serial_num).first()
    if not kiosk:
        raise HTTPException(status_code=404, detail="Kiosk not found")
    
    db.delete(kiosk)
    db.commit()
    return {"message": "Kiosk deleted successfully"}


# Get All Kiosks
@router.get("/all")
async def get_all_kiosks(db: db_dependency):
    kiosks = db.query(Kiosk).all()
    return kiosks