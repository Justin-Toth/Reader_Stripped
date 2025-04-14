# UFID Reader Application

This is the main application that runs on the Raspberry Pi for the UFID Reader project. It handles authentication, kiosk management, and interaction with the backend services.

## Prerequisites

To set up and run the application, you will need:

1. **Hardware**:

   - A Raspberry Pi (tested on Raspberry Pi 4).
   - A display (touchescreen optional).
   - A card reader or scanner for UFID input (should be provided)

2. **Software**:

   - [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download) or newer installed on the Raspberry Pi.
     - Recommend using this [guide](https://www.petecodes.co.uk/install-and-use-microsoft-dot-net-9-with-the-raspberry-pi/)

3. **Dependencies For Development**:
   - Avalonia template framework for the graphical interface.
     - May not be needed as is just used to setup project
     - Relevant info can be found [here](https://docs.avaloniaui.net/docs/get-started/install)

---

## Setup Instructions on the Raspberry Pi

1. **Clone the Repository**:

   ```bash
   git clone <repository-url>
   cd UFID_Reader
   ```

2. **Configure the Database**:

   - Update the connection string in `App.axaml.cs` to point to your MySQL database:
     ```csharp
     const string connectionString = "Server=<your-server>;Port=3306;Database=ufid_database;User=<your-user>;Password=<your-password>;";
     ```

3. **Build the Application**:

   - If doing development on a windows/mac machine and locally hosting the database, you can start here for setting up the application.

   ```bash
   dotnet build
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

---

## File Structure

The application is organized as follows:

- **`App.axaml`**: Defines the main application layout and entry point.
- **`Program.cs`**: Configures and starts the Avalonia application.
- **`Views/`**: Contains the UI components for the application.
  - **`BaseFrameView.axaml`**: The base view for the application.
  - **`MainView.axaml`**: The main window of the application.
- **`ViewModels/`**: Contains the logic for the views.
  - **`MainViewModel.cs`**: Handles the main application logic, including authentication and kiosk initialization.
- **`Services/`**: Contains service classes for handling business logic.
  - **`AuthService.cs`**: Handles authentication for class and exam modes.
  - **`DbService.cs`**: Manages database interactions using Dapper.
  - **`KioskService.cs`**: Handles kiosk registration and retrieval.
  - **`RpiService.cs`**: Retrieves the Raspberry Pi serial number.
- **`Models/`**: Contains data models for database entities.
  - **`Student.cs`**: Represents a student record.
  - **`Course.cs`**: Represents a course record.
  - **`Exam.cs`**: Represents an exam record.
- **`Factory/`**: Contains the `FrameFactory` for managing view transitions.

---

## Major Features

1. **Validation**:

   - Supports both class and exam modes.
   - Validates student schedules and exams based on the current time and location.

2. **Kiosk Management**:

   - Automatically registers kiosks using the Raspberry Pi serial number.
   - Associates kiosks with specific rooms.

3. **User Interface**:
   - Built with Avalonia for a cross-platform graphical interface.
   - Displays success or failure messages based on authentication results.

---

## Future Recommendations

1. **Improved Authentication**:

   - Add a 15-minute grace period for class and exam start times.
   - Implement additional modes for special use cases.

2. **Error Handling**:

   - Add robust error handling for database and network failures.
   - Display user-friendly error messages on the UI.

3. **Performance Optimization**:

   - Optimize database queries for large datasets.

4. **Testing**:

   - Write unit tests for all services and view models.
   - Add integration tests for database interactions.

5. **Logging**:

   - Add/Improve logging to track application events and errors for debugging and monitoring.

6. **Configuration**:

   - Sensitive information like database credentials should be moved to environment variables or a configuration file.

---

## Miscellaneous Information

- **Default Serial Number**: During development, a default serial number (`10000000d340eb60`) is used for testing on non-Raspberry Pi platforms.
- **IDE**: Recommend using [Rider](https://www.jetbrains.com/rider/) for development of the application
- **Interaction**: There is a hidden text field on the left bar of the application that can be found by hovering over the field with a mouse. This allows for interacting with the input field and typing ids manually and pressing enter during development/testing. You can also just swipe a id card using the scanner as well.

---
