import os
from flask import Flask, render_template, request
import pymysql
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

def insert_data(name, age):
    conn = create_connection()
    cursor = conn.cursor()
    cursor.execute("INSERT INTO test_data (name, age) VALUES (%s, %s)", (name, age))
    conn.commit()
    conn.close()

app = Flask(__name__)

# Create the table right after initializing the Flask app
create_table()

@app.route('/', methods=['GET', 'POST'])
def index():
    message = ""
    if request.method == 'POST':
        try:
            insert_data("Sam", 23)
            message = "Data inserted successfully!"
        except Exception as e:
            message = f"Error inserting data: {e}"
    return render_template('index.html', message=message)

if __name__ == '__main__':
    app.run(debug=True)
