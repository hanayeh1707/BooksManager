using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace BookManager
{
    public class BookManager
    {
        public static List<BookModel> ListBooks()
        {
            string booksJson = Repository.Read();
            if (!string.IsNullOrEmpty(booksJson))
                return JsonConvert.DeserializeObject<List<BookModel>>(booksJson);
            return new List<BookModel>();
        }
        public static BookModel GetBookDetails(int bookId) => ListBooks().FirstOrDefault(b => b.Id == bookId);

        public static bool AddBook(BookModel BookModel)
        {
            List<BookModel> books = ListBooks();
            int lastIndex = books.OrderBy(b => b.Id).LastOrDefault()?.Id ?? 0;
            BookModel.Id = ++lastIndex;
            books.Add(BookModel);
            return Repository.Write(JsonConvert.SerializeObject(books));
        }
        public static bool UpdateBook(BookModel BookModel)
        {
            List<BookModel> books = ListBooks();
            BookModel editedBook = books.FirstOrDefault(b => b.Id == BookModel.Id);
            editedBook.Author = BookModel.Author;
            editedBook.Title = BookModel.Title;
            editedBook.Description = BookModel.Description;
            return Repository.Write(JsonConvert.SerializeObject(books));
        }
        public static bool RemoveBook(int bookId)
        {
            List<BookModel> books = ListBooks();
            BookModel deletedBook = books.FirstOrDefault(b => b.Id == bookId);
            books.Remove(deletedBook);
            return Repository.Write(JsonConvert.SerializeObject(books));
        }
        public static List<BookModel> Find(string searchKeyword)
        {
            List<BookModel> books = ListBooks();
            searchKeyword = searchKeyword.ToLower();
            return books.Where(b => b.Title.ToLower().Contains(searchKeyword)
            || b.Author.ToLower().Contains(searchKeyword)
            || b.Description.ToLower().Contains(searchKeyword)).ToList();
        }
    }
}
