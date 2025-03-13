from fastapi import APIRouter

from app.api.routes import users, students, kiosks, exams, courses

api_router = APIRouter()
api_router.include_router(users.router)
api_router.include_router(students.router)
api_router.include_router(kiosks.router)
api_router.include_router(exams.router)
api_router.include_router(courses.router)

#api_router.include_router(login.router)
#api_router.include_router(utils.router)