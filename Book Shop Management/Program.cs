using System;
using Book_Shop_Management.Models;
using Book_Shop_Management.Services;

namespace Book_Shop_Management
{
    public class Program
    {
        static BookService service = new BookService();

        static void Main(string[] args)
        {
            int choice;
            string username, password;

            do
            {
                Console.Clear();
                Console.WriteLine("\n_________BOOK STORE MANAGEMENT SYSTEM__________");
                Console.WriteLine("1. Admin Login");
                Console.WriteLine("2. Customer");
                Console.WriteLine("3. Exit");
                Console.Write("Enter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice)) continue;

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Username: ");
                        username = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        password = Console.ReadLine();

                        if (username == "admin" && password == "1234")
                        {
                            Console.WriteLine("Login successful!");
                            Console.ReadKey();
                            AdminPanel();
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password!");
                            Console.ReadKey();
                        }
                        break;

                    case 2:
                        CustomerPanel();
                        break;

                    case 3:
                        Console.WriteLine("Thank you for using Book Store System!\n");
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadKey();
                        break;
                }

            } while (choice != 3);
        }

        static void DisplayBooks()
        {
            var books = service.GetAllBooks();
            if (books.Count == 0)
            {
                Console.WriteLine("\nNo books available.\n");
                return;
            }

            Console.WriteLine("\n-------------BOOK LIST-------------\n");
            Console.WriteLine(string.Format("{0,-8} {1,-22} {2,-20} {3,-10} {4}", "ID", "Title", "Author", "Price", "Stock"));

            foreach (var b in books)
            {
                Console.WriteLine(string.Format("{0,-8} {1,-22} {2,-20} {3,-10} {4}", b.GetId(), b.GetTitle(), b.GetAuthor(), b.GetPrice(), b.GetQuantity()));
            }
        }

        static void AdminPanel()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("\n_______ADMIN PANEL_________");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. View All Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Delete Book");
                Console.WriteLine("5. Logout");
                Console.Write("Enter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice)) continue;

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.Write("Enter Book ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Enter Book Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Author Name: ");
                        string author = Console.ReadLine();

                        Console.Write("Enter Price: ");
                        double price = double.Parse(Console.ReadLine());

                        Console.Write("Enter Quantity: ");
                        int quantity = int.Parse(Console.ReadLine());

                        if (price < 0 || quantity < 0)
                        {
                            Console.WriteLine("Invalid price or quantity!");
                        }
                        else
                        {
                            BookModel newBook = new BookModel(id, title, author, price, quantity);
                            bool added = service.AddBook(newBook);
                            if (added)
                                Console.WriteLine("Book added successfully!");
                            else
                                Console.WriteLine("Book ID already exists!");
                        }
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        DisplayBooks();
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        Console.Write("Enter Book ID to search: ");
                        int searchId = int.Parse(Console.ReadLine());
                        BookModel found = service.SearchBook(searchId);

                        if (found != null)
                        {
                            Console.WriteLine("\nBook Found!");
                            Console.WriteLine("ID: " + found.GetId());
                            Console.WriteLine("Title: " + found.GetTitle());
                            Console.WriteLine("Author: " + found.GetAuthor());
                            Console.WriteLine("Price: " + found.GetPrice());
                            Console.WriteLine("Quantity: " + found.GetQuantity());
                        }
                        else
                        {
                            Console.WriteLine("Book not found!");
                        }
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Clear();
                        Console.Write("Enter Book ID to delete: ");
                        int delId = int.Parse(Console.ReadLine());
                        bool deleted = service.DeleteBook(delId);

                        if (deleted)
                            Console.WriteLine("Book deleted successfully!");
                        else
                            Console.WriteLine("Book not found!");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("Admin logged out.");
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadKey();
                        break;
                }

            } while (choice != 5);
        }

        static void CustomerPanel()
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("\n________CUSTOMER PANEL_______");
                Console.WriteLine("1. View Books");
                Console.WriteLine("2. Search Book");
                Console.WriteLine("3. Purchase Book");
                Console.WriteLine("4. Back to Main Menu");
                Console.Write("Enter Choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice)) continue;

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        DisplayBooks();
                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        Console.Write("Enter Book ID to search: ");
                        int searchId = int.Parse(Console.ReadLine());
                        BookModel found = service.SearchBook(searchId);

                        if (found != null)
                        {
                            Console.WriteLine("\nBook Found!");
                            Console.WriteLine("ID: " + found.GetId());
                            Console.WriteLine("Title: " + found.GetTitle());
                            Console.WriteLine("Author: " + found.GetAuthor());
                            Console.WriteLine("Price: " + found.GetPrice());
                            Console.WriteLine("Quantity: " + found.GetQuantity());
                        }
                        else
                        {
                            Console.WriteLine("Book not found!");
                        }
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        DisplayBooks();
                        Console.Write("\nEnter Book ID to purchase: ");
                        int pId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Quantity: ");
                        int qty = int.Parse(Console.ReadLine());

                        double totalBill;
                        string result = service.PurchaseBook(pId, qty, out totalBill);

                        if (result == "SUCCESS")
                        {
                            BookModel b = service.SearchBook(pId);
                            Console.WriteLine("\n======= PURCHASE RECEIPT =======");
                            Console.WriteLine("Book: " + b.GetTitle());
                            Console.WriteLine("Quantity: " + qty);
                            Console.WriteLine("Price per book: $" + b.GetPrice());
                            Console.WriteLine("Total Bill: $" + totalBill);
                            Console.WriteLine("Purchase successful!");
                        }
                        else
                        {
                            Console.WriteLine(result);
                        }
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.WriteLine("Returning to main menu.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice!");
                        Console.ReadKey();
                        break;
                }

            } while (choice != 4);
        }
    }
}