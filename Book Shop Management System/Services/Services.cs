using System.Collections.Generic;
using Book_Shop_Management.Models;
using Book_Shop_Management.Repositories;

namespace Book_Shop_Management.Services
{
    public class BookService
    {
        private BookRepo repo = new BookRepo();

        public List<BookModel> GetAllBooks()
        {
            return repo.GetBooksFromFile();
        }

        public bool AddBook(BookModel book)
        {
            List<BookModel> books = repo.GetBooksFromFile();

            foreach (var b in books)
            {
                if (b.GetId() == book.GetId())
                {
                    return false;
                }
            }

            books.Add(book);
            repo.SaveAllBooksIntoFile(books);
            return true;
        }

        public BookModel SearchBook(int id)
        {
            List<BookModel> books = repo.GetBooksFromFile();
            foreach (var b in books)
            {
                if (b.GetId() == id)
                {
                    return b;
                }
            }
            return null;
        }

        public bool DeleteBook(int id)
        {
            List<BookModel> books = repo.GetBooksFromFile();
            bool found = false;

            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].GetId() == id)
                {
                    books.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (found)
            {
                repo.SaveAllBooksIntoFile(books);
            }
            return found;
        }

        public string PurchaseBook(int id, int qty, out double totalBill)
        {
            totalBill = 0;
            List<BookModel> books = repo.GetBooksFromFile();

            foreach (var b in books)
            {
                if (b.GetId() == id)
                {
                    if (qty <= 0) return "Invalid quantity!";
                    if (qty > b.GetQuantity()) return "Not enough stock available!";

                    totalBill = b.GetPrice() * qty;
                    b.SetQuantity(b.GetQuantity() - qty);

                    repo.SaveAllBooksIntoFile(books);
                    return "SUCCESS";
                }
            }
            return "Book not found!";
        }
    }
}