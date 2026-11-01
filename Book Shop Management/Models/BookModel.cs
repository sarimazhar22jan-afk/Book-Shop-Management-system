using System;

namespace Book_Shop_Management.Models
{
    public class BookModel
    {
        private int id;
        private string title;
        private string author;
        private double price;
        private int quantity;

        public BookModel(int id, string title, string author, double price, int quantity)
        {
            this.id = id;
            this.title = title;
            this.author = author;
            this.price = price;
            this.quantity = quantity;
        }

        public int GetId() { return id; }
        public void SetId(int id) { this.id = id; }

        public string GetTitle() { return title; }
        public void SetTitle(string title) { this.title = title; }

        public string GetAuthor() { return author; }
        public void SetAuthor(string author) { this.author = author; }

        public double GetPrice() { return price; }
        public void SetPrice(double price) { this.price = price; }

        public int GetQuantity() { return quantity; }
        public void SetQuantity(int quantity) { this.quantity = quantity; }

        public string ToStringFormat()
        {
            return id + "," + title + "," + author + "," + price + "," + quantity;
        }
    }
}