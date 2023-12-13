### AGDataTestProject

#### Description

This project is a .NET Core RESTful API application that manages user data. It includes endpoints for adding, updating, deleting, and retrieving user information. The application uses a MongoDB database for data persistence, an in-memory cache for improved performance, and is built using .NET Core 6.

#### Features

- **Add User**: Endpoint to add a new user with a unique name and address.
- **Update User**: Endpoint to update the address of an existing user.
- **Delete User**: Endpoint to delete a user by name.
- **Get All Users**: Endpoint to retrieve all users persisted in the database.

#### Technical Details

- **Technology Stack**: .NET Core 6, MongoDB, In-Memory Cache.
- **Dependency Injection**: Utilizes the Dependency Injection pattern.
- **SOLID Principles**: Adheres to SOLID principles for better maintainability.
- **Swagger Integration**: Includes Swagger for easy testing of API endpoints.

#### Local Setup

1. Clone the repository: `git https://github.com/dev-shoaib33/AGDataTestProject.git`
2. Open the project in Visual Studio or your preferred IDE.
3. Configure MongoDB connection string in `appsettings.json`.
4. Run the application.

#### AGDataTestProject.Tests

#### Description

This project contains unit tests for the AGDataTestProject project. The tests are written using xUnit and Moq, ensuring the functionality and correctness of the application's components.

#### Unit Tests

- **UserController Tests**: Unit tests for the UserController methods.

#### Technical Details

- **Testing Framework**: xUnit.
- **Mocking Framework**: Moq.

#### Running Tests

1. Clone the repository: `git clone https://github.com/dev-shoaib33/AGDataTestProject.git`
2. Open the project in Visual Studio or your preferred IDE.
3. Ensure the AGDataTestProject project is running.
4. Run the tests.

