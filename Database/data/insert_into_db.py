import pymysql
import os

# Database connection settings
DB_CONFIG = {
    "host": "localhost",
    "user": "myuser",
    "password": "mypass",
    "database": "ufid_database",
    "port": 3306  # Adjust if your MariaDB is on a different port
}

def execute_sql_file(cursor, filepath):
    with open(filepath, "r", encoding="utf-8") as f:
        sql = f.read()

    # Split into individual statements
    statements = [s.strip() for s in sql.split(";") if s.strip()]
    for stmt in statements:
        cursor.execute(stmt + ";")

def main():
    term = input("Enter the term (e.g., 2248): ").strip()
    course_sql_file = f"courses_{term}.sql"
    exam_sql_file = f"exams_{term}.sql"

    if not os.path.exists(course_sql_file) or not os.path.exists(exam_sql_file):
        print("SQL files not found. Make sure you ran the SQL generation script first.")
        return

    try:
        connection = pymysql.connect(**DB_CONFIG)
        cursor = connection.cursor()

        print(f"Inserting courses from {course_sql_file}...")
        execute_sql_file(cursor, course_sql_file)
        print("Courses inserted.")

        print(f"Inserting exams from {exam_sql_file}...")
        execute_sql_file(cursor, exam_sql_file)
        print("Exams inserted.")

        connection.commit()
        print("All changes committed to the database.")
    except pymysql.MySQLError as e:
        print("Database error:", e)
    finally:
        if connection:
            connection.close()

if __name__ == "__main__":
    main()
