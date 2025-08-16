
from flask import Flask, render_template
import sqlite3

app = Flask(__name__)

def get_db_connection():
    conn = sqlite3.connect("users.db")
    conn.row_factory = sqlite3.Row  
    return conn

@app.route("/")
def user_list():
    conn = get_db_connection()
    users = conn.execute("SELECT * FROM users").fetchall()
    conn.close()
    return render_template("users.html", users=users)

if __name__ == "__main__":
    conn = get_db_connection()
    conn.execute("""
        CREATE TABLE IF NOT EXISTS users (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            name TEXT NOT NULL,
            email TEXT NOT NULL
        )
    """)
    existing = conn.execute("SELECT COUNT(*) as cnt FROM users").fetchone()["cnt"]
    if existing == 0:
        conn.executemany(
            "INSERT INTO users (name, email) VALUES (?, ?)",
            [("Alice", "alice@example.com"),
             ("Bob", "bob@example.com"),
             ("Charlie", "charlie@example.com")]
        )
    conn.commit()
    conn.close()

    app.run(debug=True)
