using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace TWCodeAnalysis
{
    public class Book
    {
     
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }

        public Book() : this(string.Empty, string.Empty, string.Empty, string.Empty){ }

        public Book(string title, string genre, string author, string isbn)
        {
            this.Title = title; 
            this.Genre = genre;
            this.Author = author;
            this.ISBN = isbn;
        }
    }
}
