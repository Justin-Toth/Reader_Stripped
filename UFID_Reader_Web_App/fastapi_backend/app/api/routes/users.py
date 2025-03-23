from fastapi import APIRouter, Depends, HTTPException, status
from pydantic import BaseModel, EmailStr
from typing import Optional

# Relative Imports (want to fix this eventually)
from ...utils.security import get_password_hash
from ...utils.database import db_dependency
from ...models import User

# Pydantic Model for User Creation
class UserBase(BaseModel):
    username: str
    email: EmailStr
    full_name: str
    password: str
    is_admin: bool = False

class UserUpdate(BaseModel):
    username: Optional[str] = None
    email: Optional[EmailStr] = None
    full_name: Optional[str] = None
    password: Optional[str] = None
    is_admin: Optional[bool] = None



####################################################################
## Note need to comeback through and add auth dependecy on routes ##
####################################################################


# FastAPI Router for User Routes
router = APIRouter(prefix="/users", tags=["users"])

# Create User
@router.post("/create", status_code=status.HTTP_201_CREATED)
async def create_user(user: UserBase, db: db_dependency):
    db_object = User(
        username=user.username,
        email=user.email,
        full_name=user.full_name,
        password=get_password_hash(user.password),
        is_admin=user.is_admin
    )    
    db.add(db_object)
    db.commit()


# Get Specific User
@router.get("/get/{user_id}")
async def get_user(user_id: int, db: db_dependency):
    user = db.query(User).filter(User.id == user_id).first()
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    
    # Remove password from response
    user.password = None
    return user


# Update Specific User
@router.patch("/update/{user_id}")
async def update_user(user_id: int, user: UserUpdate, db: db_dependency):
    existing_user = db.query(User).filter(User.id == user_id).first()
    if not existing_user:
        raise HTTPException(status_code=404, detail="User not found")
   
    update_data = user.model_dump(exclude_unset=True)
    if 'password' in update_data:
        update_data['password'] = get_password_hash(update_data['password'])

    for key, value in update_data.items():
        setattr(existing_user, key, value)

    db.commit()
    db.refresh(existing_user)
    return existing_user


# Delete Specific User
@router.delete("/delete/{user_id}")
async def delete_user(user_id: int, db: db_dependency):
    db.query(User).filter(User.id == user_id).delete()
    db.commit()
    return {"message": "User Deleted"}


# Get All Users
@router.get("/all")
async def get_all_users(db: db_dependency):
    users = db.query(User).all()
    return users



