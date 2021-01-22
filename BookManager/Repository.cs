
namespace BookManager
{
    class Repository
    {
   
        const string BOOK_FILE_PATH = "books.json";
        public static bool Write(string data)
        {
            try
            {
                System.IO.File.WriteAllText(BOOK_FILE_PATH, data);
                return true; 
            }
            catch
            { }
            return false; 
           
        }
        public static string Read()
        {
            try
            {
                return System.IO.File.ReadAllText(BOOK_FILE_PATH);
            }
            catch { }
            return null;
        }

    }
    }
