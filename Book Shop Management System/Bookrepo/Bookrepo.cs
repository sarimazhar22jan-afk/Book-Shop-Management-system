using System.IO;
using System.Collections.Generic;
using Book_Shop_Management.Models;

namespace Book_Shop_Management.Repositories
{
    public class BookRepo
    {
        private string path = "books.txt";

        public void SaveBookInFile(BookModel book)
        {
            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(book.ToStringFormat());
            writer.Flush();
            writer.Close();
        }

        public List<BookModel> GetBooksFromFile()
        {
            List<BookModel> books = new List<BookModel>();

            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path);
                string record;
                while ((record = reader.ReadLine()) != null)
                {
                    string[] data = record.Split(',');
                    if (data.Length >= 5)
                    {
                        int id = int.Parse(data[0]);
                        string title = data[1];
                        string author = data[2];
                        double price = double.Parse(data[3]);
                        int qty = int.Parse(data[4]);

                        BookModel book = new BookModel(id, title, author, price, qty);
                        books.Add(book);
                    }
                }
                reader.Close();
            }
            else
            {
                books.Add(new BookModel(101, "C++ Programming", "Bjarne Stroustrup", 1500, 10));
                books.Add(new BookModel(102, "Data Structures", "Seymour Lipschutz", 1200, 8));
                books.Add(new BookModel(103, "Computer Science", "Dennis Ritchie", 1800, 5));
                SaveAllBooksIntoFile(books);
            }

            return books;
        }

        public void SaveAllBooksIntoFile(List<BookModel> books)
        {
            File.WriteAllText(path, string.Empty);
            StreamWriter writer = new StreamWriter(path, true);
            foreach (var book in books)
            {
                writer.WriteLine(book.ToStringFormat());
            }
            writer.Flush();
            writer.Close();
        }
    }
}