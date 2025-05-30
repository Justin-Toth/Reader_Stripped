from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
from .api.router import api_router


app = FastAPI()

origins = [
    "http://localhost:3000",  # Nuxt app
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

app.include_router(api_router)