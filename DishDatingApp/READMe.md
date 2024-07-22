# DishDatingApp

DishDatingApp is a web API developed with .NET 8 that matches users based on their favorite dishes. This application allows users to register, update their profiles, and filter other users based on gender, age, and favorite dishes.

## Features

- **User Registration**: Users can register via email or username.
- **Profile Management**: Users can update their name, gender, age, and favorite dishes.
- **User Filtering**: Filter users based on gender, age range, and favorite dishes.
- **Dish Liking**: Users can like dishes of other users.

## Simplifications and Comments

**This section is a list of my comments, what I simplified for speed up and what I would add.**


### Simplifications

- Validation(DataAnnotations in DTO)
- Fluent api and specify Keys and Length of columns in tables
- Create Non-Clustered Indexes on columns with filtering(Probably composite index) 
- Password hashing
- Password checking(for login purpose)
- Logging of errors
- Custom errors
- Separate DishController
- Tests (Will need to add Successfull paths and Fail paths) + Integration tests 

### What I would add

- Sign In (Use OAuth)
- Deactivation of account
- Load of image for dishes with usage of cloud storages (AWS S3, Azure BlobStorage)
- Total likes for dishes
- UserManager to work with Authentication, Authorization
- Google, Facebook, etc. Authentication
- Unit of Work pattern for repositories 
- SignalR to send real time "Matches" for dishes

### Ideas

- Searching not only by Dish but also by whole Cuisine like: Italian, Chinese, Spanish. I might like cuisine but know only most popular dishes

### My comments

I simplified a lot of things and didn't implement them for sake of time and speed up. I could be writting this app for weeks because of the ideas amount

Validation: If switched to CQRS Pattern and Mediatr then FluentValidation can be added with MediatR Validation Pipeline

**If I didn't mention something in above please ask me. I might just forgotten to write it up**


## Technologies Used

- .NET 8
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swashbuckle (Swagger)

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server

### Setup

1. **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/DishDatingApp.git
    cd DishDatingApp
    ```

2. **Set up the database:**

   Update the connection string in `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DishDatingAppDb;Trusted_Connection=True;"
      }
    }
    ```

3. **Apply migrations:**

    ```bash
    dotnet ef database update
    ```

4. **Run the application:**

    ```bash
    dotnet run
    ```

### Using Swagger

Swagger is configured for this API. You can explore the API endpoints and test them using the Swagger UI.

Once the application is running, navigate to:
https://localhost:5001/swagger


### API Endpoints

#### Register a new user

```http
POST /api/users/register
```
Request Body:
```json
{
  "name": "John Doe",
  "gender": 0,
  "age": 30,
  "favoriteDishes": ["Pizza", "Sushi"],
  "email": "johndoe@example.com",
  "password": "Password123!"
}

```
Available Genders :
- Male - 0 ,
- Female - 1,
- Lesbian - 2,
- Gay - 3,
- Bisexual - 4,
- Queer - 5

Update user details:
```http
PUT /api/users/{id}
```

Request Body:
```json
{
  "name": "John Doe",
  "gender": 0 ,
  "age": 29,
  "favoriteDishes": ["Burgers", "Salad"],
  "email": "johndoe@example.com",
  "password": "Password123!"
}
```
Get filtered users:
```http
GET /api/users
```

Query Parameters:

- gender (required),
- minAge (required),
- maxAge (required),
- dishes (optional, list of favorite dishes)

Like a dish:
```http
POST /api/users/{userId}/like/{dishId}
```

## Unit Tests
Unit tests are provided to ensure the functionality of the application.

Run the tests:
```bash
dotnet test
```

This `README.md` file provides a comprehensive overview of project, including setup instructions, API endpoint details





