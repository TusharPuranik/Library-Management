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
    private List<Book> books = new List<Book>();
    private List<User> users = new List<User>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(int bookId)
    {
        Book bookToRemove = books.Find(book => book.ID == bookId);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
        }
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }

    public Book SearchBookByTitle(string title)
    {
        return books.Find(book => book.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public List<Book> SearchBookByAuthor(string author)
    {
        return books.FindAll(book => book.Author.Equals(author, StringComparison.OrdinalIgnoreCase));
    }

    public void AddUser(User user)
    {
        users.Add(user);
    }

    public List<User> GetAllUsers()
    {
        return users;
    }

    public User SearchUserById(int id)
    {
        return users.Find(user => user.ID == id);
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

        Book book1 = new Book(1, "book2", "author1");
        Book book2 = new Book(2, "book2", "author2");
        Book book3 = new Book(3, "book3", "author3");

        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);

        User user1 = new User(1, "Tushar");
        User user2 = new User(2, "Pooja");

        library.AddUser(user1);
        library.AddUser(user2);

        Console.WriteLine("Welcome to the Library System!");

        while (true)
        {
            Console.WriteLine("\nEnter '1' to search for a book by title");
            Console.WriteLine("Enter '2' to search for books by author");
            Console.WriteLine("Enter '3' to view all books");
            Console.WriteLine("Enter '4' to view all users");
            Console.WriteLine("Enter '5' to add a new book");
            Console.WriteLine("Enter '6' to add a new user");
            Console.WriteLine("Enter '7' to exit");
            Console.Write("Your choice: ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.Write("Enter the title of the book: ");
                string title = Console.ReadLine();
                Book foundBook = library.SearchBookByTitle(title);
                if (foundBook != null)
                {
                    Console.WriteLine($"Book Found: {foundBook.Title} by {foundBook.Author}");
                    if (foundBook.IsAvailable)
                    {
                        Console.Write("Do you want to borrow this book? (yes/no): ");
                        string borrowChoice = Console.ReadLine();
                        if (borrowChoice.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.Write("Enter your user ID: ");
                            int userId = int.Parse(Console.ReadLine());
                            User user = library.SearchUserById(userId);
                            if (user != null)
                            {
                                if (user.BorrowBook(foundBook))
                                {
                                    Console.WriteLine($"Book '{foundBook.Title}' borrowed successfully by {user.Name}");
                                }
                                else
                                {
                                    Console.WriteLine("The book is not available for borrowing.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("User not found. Please check the user ID.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("The book is already borrowed.");
                    }
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else if (input == "2")
            {
                Console.Write("Enter the author's name: ");
                string author = Console.ReadLine();
                List<Book> foundBooks = library.SearchBookByAuthor(author);
                if (foundBooks.Count > 0)
                {
                    Console.WriteLine("Books Found:");
                    foreach (Book book in foundBooks)
                    {
                        Console.WriteLine($"- {book.Title} by {book.Author}");
                    }
                }
                else
                {
                    Console.WriteLine("No books found for the author.");
                }
            }
            else if (input == "3")
            {
                List<Book> allBooks = library.GetAllBooks();
                Console.WriteLine("All Books:");
                foreach (Book book in allBooks)
                {
                    Console.WriteLine($"- {book.Title} by {book.Author}");
                }
            }
            else if (input == "4")
            {
                List<User> allUsers = library.GetAllUsers();
                Console.WriteLine("All Users:");
                foreach (User user in allUsers)
                {
                    Console.WriteLine($"- {user.Name} (ID: {user.ID})");
                }
            }
            else if (input == "5")
            {
                Console.Write("Enter the book ID: ");
                int bookId = int.Parse(Console.ReadLine());
                Console.Write("Enter the book title: ");
                string title = Console.ReadLine();
                Console.Write("Enter the author's name: ");
                string author = Console.ReadLine();

                Book newBook = new Book(bookId, title, author);
                library.AddBook(newBook);

                Console.WriteLine("Book added successfully!");
            }
            else if (input == "6")
            {
                Console.Write("Enter the user ID: ");
                int userId = int.Parse(Console.ReadLine());
                Console.Write("Enter the user's name: ");
                string name = Console.ReadLine();

                User newUser = new User(userId, name);
                library.AddUser(newUser);

                Console.WriteLine("User added successfully!");
            }
            else if (input == "7")
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}
