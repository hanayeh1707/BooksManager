using System;
using System.Collections.Generic;

namespace BookManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Options:
            {
                Console.WriteLine("==== Book Manager ====");
                Console.WriteLine("1) View all books \n2) Add a book \n3) Edit a book \n4) Search for a book \n5) Save and exit");
                Console.Write("Choose [1-5]:");
                string option = Console.ReadLine();
                if (option != "")
                {
                    SelectOption(option);
                    goto Options;
                }
            } 
            Console.ReadKey();
        }
        /// <summary>
        /// Handles the user selected option from [Book manager] options 
        /// </summary>
        /// <param name="option"></param>
        static void SelectOption(string option)
        {
            switch (option)
            {
                case "1":
                    ViewBooks();
                    break;
                case "2":
                    AddBook();
                    break; 
                case "3":
                    EditBook();
                    break;
                case "4":
                    FindBooks();
                    break;
                case "5":
                    Console.WriteLine("Library saved");
                    Console.ReadKey();
                    break; 
                default:
                    break;
            }
        }
        /// <summary>
        /// Lists all the saved books book id & book title, with endline option to select book id to view its details
        /// </summary>
        static void ViewBooks()
        {
            Console.WriteLine("==== View Books ====");
            ListBooks();
            Console.WriteLine("To view details enter the book ID, to return press<Enter>.");
            ShowDetails();
        }
        /// <summary>
        /// Asks the user for a book id to display its details
        /// </summary>
        static void ShowDetails()
        {
            while (true)
            {
                Console.Write("Book ID: ");
                int.TryParse(Console.ReadLine(), out int bookId);
                if (bookId == 0)
                    return;
                BookModel book = BookManager.GetBookDetails(bookId);
                if (book == null)
                {
                    Console.WriteLine("No book with this Id was found");
                    continue; 
                } 
                Console.WriteLine($"ID: {book.Id} \n" +
                    $"Title: {book.Title}\n" +
                    $"Author: {book.Author} \n" +
                    $"Description: {book.Description}");
            }
        }
        /// <summary>
        /// Asks user for a new book details to be saved.
        /// </summary>
        static void AddBook()
        {
            BookModel bookModel = new BookModel();
            Console.WriteLine(" ==== Add a Book ====");
            Console.WriteLine("Please enter the following information:");
            Console.Write("Title:");
            bookModel.Title = Console.ReadLine();
            Console.Write("Author:");
            bookModel.Author = Console.ReadLine();
            Console.Write("Description:");
            bookModel.Description = Console.ReadLine();

            if (BookManager.AddBook(bookModel))
                Console.WriteLine($"Book [{bookModel.Id}] Saved"); 
        }
        /// <summary>
        /// Allows user to select a book by id from the book list to edit its details.
        /// </summary>
        static void EditBook() {
            Console.WriteLine("==== Edit a Book ====");
            ListBooks();

            EditBook:
            {
                Console.WriteLine("Enter the book ID of the book you want to edit; to return press <Enter>.");
                Console.Write("Book ID: ");
                int.TryParse(Console.ReadLine(), out int bookId);
                if (bookId != 0)
                {
                    BookModel bookModel = BookManager.GetBookDetails(bookId);
                    Console.WriteLine("Input the following information. To leave a field unchanged, hit <Enter>");
                    Console.Write("Title:");
                    string Title = Console.ReadLine();
                    if (!string.IsNullOrEmpty(Title))
                        bookModel.Title = Title;
                    Console.Write("Author:");
                    string Author = Console.ReadLine();
                    if (!string.IsNullOrEmpty(Author))
                        bookModel.Author = Author;
                    Console.Write("Description:");
                    string Description = Console.ReadLine();
                    if (!string.IsNullOrEmpty(Description))
                        bookModel.Description = Description;
                    Console.WriteLine("Book saved.");
                    goto EditBook;
                }
            }
        }
        /// <summary>
        /// Allows user to enter search keywords to seach for the books that contain such keywords in their details
        /// </summary>
        static void FindBooks()
        {
            Console.WriteLine("==== Search ====\n" +
                "Type in one or more keywords to search for");
            Console.Write("Search:");
            string keywords=Console.ReadLine();

            Console.WriteLine("The following books matched your query. Enter the book ID to see more details, or <Enter> to return.");
            ListBooks(keywords);
            ShowDetails();
        }
        /// <summary>
        ///  Fetch books and list them
        /// </summary>
        /// <param name="searchText">optional.pass it to filter the list </param>
        static void ListBooks(string searchText = "")
        {
            List<BookModel> books = new List<BookModel>();
            if (string.IsNullOrEmpty(searchText))
                books = BookManager.ListBooks();
            else
                books = BookManager.Find(searchText);
            if (books?.Count > 0)
            {
                books.ForEach(b =>
                {
                    Console.WriteLine($"[{b.Id}] {b.Title}");
                });
            }
            else
                Console.WriteLine("No books were found");
        }

    }
}
