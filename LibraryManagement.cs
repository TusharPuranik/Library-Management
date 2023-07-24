using System;
using System.Collections.Generic;

class Book
{
    public int ID { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsAvailable { get; set; }

    public Book(int id, string title, string author)
    {
        ID = id;
        Title = title;
        Author = author;
        IsAvailable = true;
    }
}

class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(int bookID)
    {
        books.RemoveAll(book => book.ID == bookID);
    }

    public Book SearchBookByTitle(string title)
    {
        return books.Find(book => book.Title.ToLower() == title.ToLower());
    }

    public Book SearchBookByAuthor(string author)
    {
        return books.Find(book => book.Author.ToLower() == author.ToLower());
    }
}

class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public List<Book> BorrowedBooks { get; set; }

    public User(int id, string name)
    {
        ID = id;
        Name = name;
        BorrowedBooks = new List<Book>();
    }

    public bool BorrowBook(Book book)
    {
        if (book.IsAvailable)
        {
            book.IsAvailable = false;
            BorrowedBooks.Add(book);
            return true;
        }
        return false;
    }

    public bool ReturnBook(Book book)
    {
        if (BorrowedBooks.Contains(book))
        {
            book.IsAvailable = true;
            BorrowedBooks.Remove(book);
            return true;
        }
        return false;
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();
        List<User> users = new List<User>();

        Console.WriteLine("Welcome to the Library Management System");

        while (true)
        {
            Console.WriteLine("\n1. Add Book");
            Console.WriteLine("2. Add User");
            Console.WriteLine("3. Search Book by Title");
            Console.WriteLine("4. Search Book by Author");
            Console.WriteLine("5. Borrow Book");
            Console.WriteLine("6. Return Book");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter book ID: ");
                    int bookID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter book title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter book author: ");
                    string author = Console.ReadLine();
                    Book newBook = new Book(bookID, title, author);
                    library.AddBook(newBook);
                    Console.WriteLine("Book added successfully!");
                    break;

                case 2:
                    Console.Write("Enter user ID: ");
                    int userID = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Enter user name: ");
                    string userName = Console.ReadLine();
                    User newUser = new User(userID, userName);
                    users.Add(newUser);
                    Console.WriteLine("User added successfully!");
                    break;

                case 3:
                    Console.Write("Enter book title: ");
                    title = Console.ReadLine();
                    Book foundBookByTitle = library.SearchBookByTitle(title);
                    if (foundBookByTitle != null)
                        Console.WriteLine("Book found: " + foundBookByTitle.Title + " by " + foundBookByTitle.Author);
                    else
                        Console.WriteLine("Book not found!");
                    break;

                case 4:
                    Console.Write("Enter author name: ");
                    author = Console.ReadLine();
                    Book foundBookByAuthor = library.SearchBookByAuthor(author);
                    if (foundBookByAuthor != null)
                        Console.WriteLine("Book found: " + foundBookByAuthor.Title + " by " + foundBookByAuthor.Author);
                    else
                        Console.WriteLine("Book not found!");
                    break;

                case 5:
                    Console.Write("Enter user ID: ");
                    userID = Convert.ToInt32(Console.ReadLine());
                    User userToBorrow = users.Find(user => user.ID == userID);
                    if (userToBorrow != null)
                    {
                        Console.Write("Enter book ID to borrow: ");
                        int bookIDToBorrow = Convert.ToInt32(Console.ReadLine());
                        Book bookToBorrow = library.SearchBookByTitle(title);
                        if (bookToBorrow != null)
                        {
                            if (userToBorrow.BorrowBook(bookToBorrow))
                                Console.WriteLine("Book borrowed successfully!");
                            else
                                Console.WriteLine("Book is not available for borrowing.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found!");
                    }
                    break;

                case 6:
                    Console.Write("Enter user ID: ");
                    userID = Convert.ToInt32(Console.ReadLine());
                    User userToReturn = users.Find(user => user.ID == userID);
                    if (userToReturn != null)
                    {
                        Console.Write("Enter book ID to return: ");
                        int bookIDToReturn = Convert.ToInt32(Console.ReadLine());
                        Book bookToReturn = library.SearchBookByTitle(title);
                        if (bookToReturn != null)
                        {
                            if (userToReturn.ReturnBook(bookToReturn))
                                Console.WriteLine("Book returned successfully!");
                            else
                                Console.WriteLine("Book is not borrowed by the user.");
                        }
                        else
                        {
                            Console.WriteLine("Book not found!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("User not found!");
                    }
                    break;

                case 7:
                    Console.WriteLine("Thank you for using the Library Management System.");
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
