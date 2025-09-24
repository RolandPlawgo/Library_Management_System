# LibraryManagementSystem

This is a university project for the Software Engineering class.


## Description
A simple library management system that allows customers to log in, search, reserve, and borrow books, while employees can manage accounts, add books, and track borrowing activity.

## Setup instructions/ usage
### Default accounts

user account: <br/>
library card number: U123456 <br/>
password: Qwerty1! <br/>

administrator account: <br/>
library card number: A123456 <br/>
password: Qwerty1! <br/>
</br>

## Technologies used

- C#
- ASP .NET Core (MVC)
- Entity Framework Core
- Bootstrap


## Features
### Customer features

- Secure Login – Customers can log in with their library number and password. Invalid credentials return an error message.
- Book Search – Search by title or author, with results ranked by relevance (prefix matches shown first).
- Book Reservation – Reserve books to ensure availability. Reserved books are blocked for others until the due date.
- Account Management – View borrowed and reserved books along with their due dates. Returned books are automatically removed from the account.
- Password Management – Logged-in users can change their password, which immediately updates their login credentials.

### Employee features

- Customer Account Creation – Employees can create customer accounts with required details. Library card numbers can be entered manually or generated automatically. Default passwords are based on the customer’s date of birth.
- Employee Account Creation – Employees can create new employee accounts. Employee IDs are generated automatically.
- Book Management – Add new books by providing the author and title. Books become available immediately in the system.
- Borrowing & Returns – Record when books are borrowed or returned by customers, tracked using book ID and customer card number.
- Overdue Tracking – Access information about which customer borrowed a specific book and the due date for its return.
- User accounts management


## Screenshots
<h3>Customer:</h3>
<img width="1768" height="832" alt="image" src="https://github.com/user-attachments/assets/78455d37-a8c8-4955-bdc1-b185ee654bce" />
<img width="1790" height="599" alt="image" src="https://github.com/user-attachments/assets/7c6325ba-f045-4baf-b7a5-a954b2e31930" />

<img width="1784" height="600" alt="image" src="https://github.com/user-attachments/assets/734b0a0b-2f5c-4a0f-a742-292043cedc93" />
</br>
</br>
<img width="1833" height="896" alt="image" src="https://github.com/user-attachments/assets/dd395927-3e66-4638-a297-3e57e944e225" />
</br>
<h3>Employee:</h3>
<img width="1775" height="838" alt="image" src="https://github.com/user-attachments/assets/7747486a-a716-43d0-9c3c-6c56b3812455" />
<img width="1788" height="478" alt="image" src="https://github.com/user-attachments/assets/167b6e58-c148-49f1-bf1d-3b8b580c451c" />
</br>
</br>
<img width="1787" height="447" alt="image" src="https://github.com/user-attachments/assets/f6190ce6-d00d-4091-a043-990c08b8ebbc" />
