# New Backend server for our Admin site/remote server

## Prerequisites

- Python 3.13 or newer
- [uv](https://github.com/astral-sh/uv) package manager

## Setup

1. CD into the backend folder.

2. Setup a virtual enivronment:

   ```sh
   uv venv
   source .venv/Scripts/activate  # On Linux/Mac use `.venv\bin\activate`
   ```

3. Install dependencies for the project:

   ```sh
   uv sync
   ```

## Running the server

1. Run the application:

   ```sh
   uv run run_app.py
   ```

2. The server will be running at `http://127.0.0.1:8000`.

## Backend Structure

The backend is organized into the following structure:

- **`app/`**: Contains the main application logic.
  - **`main.py`**: Entry point for the FastAPI application.
  - **`api/`**: Houses all API route definitions.
    - **`router.py`**: Combines all route modules into a single router.
    - **`routes/`**: Contains individual route modules for different entities:
      - `users.py`: User-related endpoints (create, update, delete, etc.).
      - `students.py`: Student-related endpoints (CRUD operations for students).
      - `courses.py`: Course-related endpoints (CRUD operations for courses).
      - `exams.py`: Exam-related endpoints (CRUD operations for exams).
      - `kiosks.py`: Kiosk-related endpoints (CRUD operations for kiosks).
  - **`utils/`**: Utility modules for shared functionality.
    - `database.py`: Database connection and session management.
    - `security.py`: Security-related utilities (e.g., password hashing).
  - **`models.py`**: SQLAlchemy models defining the database schema.
- **`run.py`**: Script to initialize and run the application.
- **`pyproject.toml`**: Dependency and project configuration file.
- **`.python-version`**: Specifies the Python version for the project.

## Future Recommendations

1. **Authentication and Authorization**:

   - Implement role-based access control (RBAC) to restrict access to certain endpoints.
   - Add token-based authentication (e.g., JWT) for secure API access.

2. **Environment Configuration**:

   - Move sensitive information (e.g., database credentials) to environment variables or a `.env` file.
   - Use a configuration management library like `pydantic` for environment validation.

3. **Database Enhancements**:

   - Add database migrations using a tool like Alembic to manage schema changes.
   - Optimize database queries to improve performance for large datasets.

4. **Testing**:

   - Write unit tests for all routes and utility functions.
   - Use a testing framework like `pytest` with a test database setup.

5. **Error Handling**:

   - Standardize error responses across all endpoints.
   - Add logging for better debugging and monitoring.

6. **Documentation**:

   - Expand API documentation with detailed examples for each endpoint.
   - Include versioning for the API to manage future updates.

7. **Scalability**:

   - Consider containerizing the application using Docker for easier deployment.

8. **Monitoring and Metrics**:

   - Integrate monitoring tools to track application performance.
   - Add logging for critical events and errors.

9. **Code Quality**:
   - Refactor relative imports to absolute imports for better maintainability.

## API/Schema Documentation

- You can view the FastAPI documentation by navigating to `http://127.0.0.1:8000/docs` in your web browser while the server is running.
