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


