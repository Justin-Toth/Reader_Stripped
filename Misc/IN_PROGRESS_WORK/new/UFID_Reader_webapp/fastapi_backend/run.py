import uvicorn
from app.main import app
from app.utils.database import init_db


def main():
    init_db()

    uvicorn.run(
        "app.main:app", 
        host="127.0.0.1",
        port=8000,
        reload=True,
    )


if __name__ == "__main__":
    main()
