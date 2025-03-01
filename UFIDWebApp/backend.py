import pymysql
import os
from dotenv import load_dotenv

load_dotenv()

DB_CONFIG = {
    'host': os.getenv('DB_HOST', 'localhost'),
    'port': int(os.getenv('DB_PORT', 3306)),
    'user': os.getenv('DB_USER', 'myuser'),
    'password': os.getenv('DB_PASSWORD', 'mypass'),
    'db': os.getenv('DB_NAME', 'ufid_database')
}

def create_connection():
    return pymysql.connect(**DB_CONFIG)

# Function to create a table
def create_table():
    conn = create_connection()
    cursor = conn.cursor()
    cursor.execute('''
        CREATE TABLE IF NOT EXISTS test_data (
            id INT AUTO_INCREMENT PRIMARY KEY,
            name VARCHAR(255) NOT NULL,
            age INT NOT NULL
        )
    ''')
    conn.commit()
    conn.close()

# Function to insert data
def insert_data(name, age):
    conn = create_connection()
    cursor = conn.cursor()
    cursor.execute("INSERT INTO test_data (name, age) VALUES (%s, %s)", (name, age))
    conn.commit()
    conn.close()

# Function to read data
def read_data():
    conn = create_connection()
    cursor = conn.cursor()
    cursor.execute("SELECT * FROM courses")
    results = cursor.fetchall()
    conn.close()
    return results

if __name__ == "__main__":
    create_table()
    insert_data("Alice", 25)
    insert_data("Bob", 30)
    print("Stored Data:", read_data())