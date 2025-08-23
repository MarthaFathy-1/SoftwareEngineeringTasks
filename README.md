https://github.com/mostafaaElsherbiny/software-engineering-tasks-for-beginners.git

Task 1: REST API for CRUD Operations on a Book
Objective
Create a REST API that allows users to perform CRUD (Create, Read, Update, Delete) operations on a book.

Requirements
Use a web framework (e.g., Express for Node.js, Flask for Python).
Implement the following endpoints:
POST /books: Create a new book.
GET /books: Retrieve a list of all books.
GET /books/:id: Retrieve a specific book by ID.
PUT /books/:id: Update a specific book by ID.
DELETE /books/:id: Delete a specific book by ID.
Use a database to store book information (e.g., MongoDB, PostgreSQL).
Validate input data to ensure data integrity.
Handle errors gracefully and return appropriate HTTP status codes.
Example
Here is an example of how the API endpoints should work:

Create a new book

POST /books
{
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "publishedDate": "1925-04-10"
}
Retrieve all books

GET /books
Retrieve a specific book by ID

GET /books/1
Update a specific book by ID

PUT /books/1
{
  "title": "The Great Gatsby",
  "author": "F. Scott Fitzgerald",
  "publishedDate": "1925-04-10"
}
Delete a specific book by ID

DELETE /books/1

Task 2: REST API with Relations and Multiple CRUD Operations
Objective
Create a REST API that manages authors and their books, demonstrating relations and multiple CRUD operations.

Requirements
Use a web framework (e.g., Express for Node.js, Flask for Python).
Implement the following endpoints:
POST /authors: Create a new author.
GET /authors: Retrieve a list of all authors.
GET /authors/:id: Retrieve a specific author by ID.
PUT /authors/:id: Update a specific author by ID.
DELETE /authors/:id: Delete a specific author by ID.
POST /authors/:authorId/books: Create a new book for a specific author.
GET /authors/:authorId/books: Retrieve all books for a specific author.
GET /authors/:authorId/books/:bookId: Retrieve a specific book by ID for a specific author.
PUT /authors/:authorId/books/:bookId: Update a specific book by ID for a specific author.
DELETE /authors/:authorId/books/:bookId: Delete a specific book by ID for a specific author.
Use a database to store author and book information (e.g., MongoDB, PostgreSQL).
Validate input data to ensure data integrity.
Handle errors gracefully and return appropriate HTTP status codes.


Task 3: Books Management System
Objective
Create a books management system with user registration, login, and CRUD operations for books only for authenticated users.

Requirements
Implement user authentication and authorization:
POST /register: Register a new user.
POST /login: Login a user and return a token.
Implement the following book-related endpoints, accessible only to authenticated users:
POST /books: Create a new book.
GET /books: Retrieve a list of all books.
GET /books/:id: Retrieve a specific book by ID.
PUT /books/:id: Update a specific book by ID.
DELETE /books/:id: Delete a specific book by ID.
Use a database to store user and book information (e.g., MongoDB, PostgreSQL).
Validate input data to ensure data integrity.
Handle errors gracefully and return appropriate HTTP status codes.

Task 4: Authorization Challenge
Objective
Create a system that demonstrates different levels of authorization for users, including roles and permissions.

Requirements
Use a web framework (e.g., Express for Node.js, Flask for Python).
Implement user roles and permissions:
Roles: Admin, Editor, Viewer.
Permissions: Create, Read, Update, Delete.
Implement the following endpoints with role-based access control:
POST /books: Create a new book (Admin, Editor).
GET /books: Retrieve a list of all books (Admin, Editor, Viewer).
GET /books/:id: Retrieve a specific book by ID (Admin, Editor, Viewer).
PUT /books/:id: Update a specific book by ID (Admin, Editor).
DELETE /books/:id: Delete a specific book by ID (Admin).
Use a database to store user, role, and book information (e.g., MongoDB, PostgreSQL).
Validate input data to ensure data integrity.
Handle errors gracefully and return appropriate HTTP status codes.
