# UFID Reader Project

The UFID Reader system is designed to streamline attendance tracking by using kiosks equipped with magnetic stripe/NFC modules to read, validate, and save student information. The system consists of multiple components, including a backend, frontend, Raspberry Pi application, and a database.

A link to the original project repository can be found [here](https://github.com/brod2240/ufid_reader)

---

## Project Components

### 1. **Database**

The database is hosted in a Docker container using MariaDB. It stores data for courses, exams, kiosks, users, and student rosters. The backend and Raspberry Pi application interact with this database to validate and manage attendance data.

- **Details**: [Database README](Database/README.md)

### 2. **WebApp**

#### **Backend**

The backend is built using FastAPI and provides API endpoints for managing students, courses, exams, and kiosks.

- **Details**: [Backend README](UFID_Reader_Web_App/fastapi_backend/README.md)

#### **Frontend**

The frontend is a Nuxt 3 application that serves as the user interface for managing and interacting with the system. It allows administrators and professors to view and manage data.

- **Details**: [Frontend README](UFID_Reader_Web_App/nuxt_frontend/README.md)

---

### 3. **Raspberry Pi Application**

The Raspberry Pi application runs on kiosks and handles UFID validation, kiosk registration, and interaction with the backend. It is built using .NET and Avalonia for a cross-platform graphical interface.

- **Details**: [Raspberry Pi Application README](UFID_Reader/README.md)

---

### 4. **Kiosk Case**

The kiosk case is the physical housing for the Raspberry Pi and other hardware components. It is designed to securely hold the hardware and provide a user-friendly interface.

- **Details**: [Kiosk Case README](Misc/KioskCase/README.md)

---

## Setup Instructions

Each component has its own setup instructions. Refer to the respective README files linked above for detailed steps.

---

## System Overview

1. **Authentication**:

   - Validates student attendance for classes and exams.
   - Uses Raspberry Pi serial numbers to register kiosks and associate them with specific rooms.

2. **Database**:

   - Stores student, course, exam, and kiosk data.
   - MariaDB is used for backend storage, hosted in a Docker container.

3. **User Interface**:
   - Professors and administrators can manage data through the frontend.
   - Students interact with the kiosks for attendance validation.

---

## Future Recommendations

1. **Improved Security**:

   - Encrypt data in transit between the Raspberry Pi and backend.
   - Use environment variables for sensitive information like database credentials.

2. **Scalability**:

   - Add support for multiple kiosks and load balancing for the backend.
   - Optimize database queries for large datasets.

3. **Testing**:

   - Add unit and integration tests for all components.
   - Conduct stress testing for the backend and Raspberry Pi application.

4. **Deployment**:
   - Containerize the system using Docker for consistent deployment.
   - Automate deployment with CI/CD pipelines.

---

## Bugs/Issues

For a detailed list of known bugs and issues, refer to the respective component READMEs.

Unfortunately, we have not had time to extensively test this new system and there is no major bug tracker for any of the applicatios as we have not found any in our limited testing.

## Misc Information

### Project Setup Video

[![Setup](https://youtu.be/CkqPjx8nOnQ)]

### Public Course API

#### Credit:

This API is based on the [Rob Olsthoorn UF API](https://github.com/Rolstenhouse/uf_api?tab=readme-ov-file#courses).

#### How to Use:

**Base URL**:  
`GET https://one.ufl.edu/apix/soc/schedule/[parameters]`

- **Required Parameters**:
  - `category`: Program/Category (e.g., `RES`, `CWSP`, `UFO`, etc.).
  - `term`: Semester/Term (e.g., `20165` for Summer 2016).

**Sample Call**:

```plaintext
https://one.ufl.edu/apix/soc/schedule/?category=RES&term=20165
```

#### Parameters:

- **Program/Category (Required)**:

  - `RES`: Campus/Web/Special Program (Regular) (Summer 2018 and earlier).
  - `CWSP`: Campus/Web/Special Program (Regular) (Fall 2018 and later).
  - `UFO`: UF Online Program.
  - `IA`: Innovation Academy.
  - `HUR`: USVI and Puerto Rico.

- **Semester/Term (Required)**:  
  Format: `[Year (remove second digit)][Semester number][Optional Summer Session]`

  - Spring: `1`
  - Summer: `5` (Append `6W1` for A, `6W2` for B, `1` for C).
  - Fall: `8`
  - **Examples**:
    - Summer A 2024: `22456W1`
    - Fall 2024: `2248`

- **Pagination**:  
  The API limits the number of returned courses. Use the `last-control-number` parameter to retrieve the next set of results.

  - Example:
    ```plaintext
    https://one.ufl.edu/apix/soc/schedule/?category=RES&term=20165&last-control-number=50
    ```

- **Additional Parameters**:
  - `course-code`: Filter by course code (e.g., `course-code=eel3135`).
  - `class-num`: Filter by class/section number (e.g., `class-num=12345`).
  - `day-m`, `day-t`, `day-w`, `day-r`, `day-f`, `day-s`: Filter by meeting days. Use `'true'` or `'false'` as strings (e.g., `"day-m": 'true'`).

---

This README serves as an overview of the project. For detailed information, refer to the individual component READMEs linked above.
