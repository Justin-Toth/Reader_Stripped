# UFID Reader Database

This folder contains everything needed to initialize and manage the database for the UFID Reader project. The database is hosted in a Docker container using MariaDB and is designed to store data for courses, exams, kiosks, users, and student rosters while being lightweight and efficient.

---

## Overview

- **Database**: MariaDB hosted in a Docker container.
- **Purpose**: Stores data for courses, exams, kiosks, users, and student rosters, which is accessed by the backend and Raspberry Pi application.
- **Connection**: Accessible via LAN connections using TCP protocol.

---

## Folder Structure

- **`docker-compose.yml`**:

  - Defines the Docker container for MariaDB.
  - To intialize the container: (under this directory)
    ```bash
    docker-compose up -d
    ```

- **`/data`**:
  - Contains the database backup and a Python script for grabbing UF semester data as SQL files to import into the database:
    - `ufid_database_backup.sql`:
      - Backup file for the database.
    - `API-to-Database.py`:
      - Script to use UF's public API to grab an entire semester's course information.
      - **Note**: This file is currently out of date and needs updating. It still works but generates SQLite3 database files instead of MariaDB-compatible SQL files.

---

## Prerequisites

1. **Docker**:

   - Install Docker and Docker Compose on your machine.
   - [Docker Installation Guide](https://docs.docker.com/get-docker/)

2. **MariaDB**:
   - Ensure MariaDB is installed locally if you need to export or import the database.
   - [MariaDB Releases](https://mariadb.org/mariadb/all-releases/)

---

## Setting Up the Database

1. **Start the Docker Container**:

   - Navigate to this directory and run:
     ```bash
     docker-compose up -d
     ```
   - This will create and start the necessary Docker containers, including the MariaDB container.
   - Docker network is also instantiated for groundwork towards containerizing other components into docker

2. **Access the Database**:

   - Ensure the container is running:

     ```bash
     docker ps
     ```

     If the container is not running, start it:

     ```bash
     docker start ufid_mariadb
     ```

   - Access the database via MySQL:
     ```bash
     mysql -h 127.0.0.1 -P 3306 --protocol=TCP -u myuser -p
     ```
     Or directly through Docker:
     ```bash
     docker exec -it ufid_mariadb mariadb -u myuser -p
     ```
     - **Password**: `mypass`

---

## Exporting and Importing the Database

### Exporting

To create a backup of the database:

1. Ensure the container is running.
2. Run the following command:
   ```bash
   docker exec ufid_mariadb mariadb-dump -umyuser -pmypass ufid_database > ufid_database_backup.sql
   ```
3. The `ufid_database_backup.sql` file will be created in this directory. Commit this file to version control if needed.

### Importing

To restore the database from a backup:

1. Ensure the container is running.
2. Run the following command:
   ```bash
   docker exec -i ufid_mariadb mariadb -umyuser -pmypass ufid_database < ufid_database_backup.sql
   ```
3. The database will now be populated with the data from the backup.

---

## Allowing Access Across Machines on the Same Network

To allow access from other machines on the same network:

1. Ensure the container is running.
2. Access the MariaDB container as root user:

   ```bash
   docker exec -it ufid_mariadb mariadb -u root -p
   ```
   - **Password**: `mypass` (should probably be updated for obvious security reasons)
3. Grant access to other machines:

   ```sql
   GRANT ALL PRIVILEGES ON *.* TO 'myuser'@'%' IDENTIFIED BY 'mypass' WITH GRANT OPTION;
   FLUSH PRIVILEGES;
   ```

   - **Note**: The `@'%'` allows connections from any IP. For better security, replace `%` with a specific IP address.


4. Open the firewall on the host machine:

   - Add an inbound rule to allow TCP traffic on port `3306`.
   - **Note**: This adds an inboud rule that exposes a port to the DB which on a public network can be risky. For better security, consider exposing this port to only VPNs or private networks. Additionally if ported to a cloud VM, be sure to configure security groups to allow inbound traffic.

5. Find the host machine's IP address:

   ```bash
   ipconfig # On Windows
   ```

   - Look for the `IPv4 Address` under the `Wireless LAN adapter Wi-Fi` section. It should look something like:
     ```
     IPv4 Address. . . . . . . . . . .: 192.168.1.100
     ```

6. Update connection strings:

   - Update the database connection strings in your project to use the host machine's IP address.

7. Verify the connection:
   - Test the connection from a secondary machine to ensure the database is accessible.

---

## Notes

- Because the docker container relies on LAN connectoins, and changes do not persist across environments/different docker containers. Use the export/import process to share updates.
- For development, it is recommended to use a Docker GUI for easier management of containers.

---

## Future Improvements

1. **Remote Access**:

   - Set up the database on a remote server for secure access from anywhere, instead of relying on LAN connections.
   - PLEASE PESTER DR. BLANCHARD ABOUT THIS!

2. **Security**:
   - Use environment variables for sensitive information like database credentials.
   - Encrypt data in transit.

---

For more details on how the database interacts with the system, refer to the [Backend README](../UFID_Reader_Web_App/fastapi_backend/README.md).
