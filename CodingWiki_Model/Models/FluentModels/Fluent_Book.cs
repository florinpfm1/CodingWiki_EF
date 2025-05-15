using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Fluent_Book
    {
        //[Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        //[MaxLength(20)]
        //[Required]
        public string ISBN { get; set; }
        public decimal Price { get; set; }
        //[NotMapped]
        public string PriceRange { get; set; }

        //relations
        public Fluent_BookDetail BookDetail { get; set; } //navigation prop 1-to-1
                                                   //from parent Book class we can extract its only child BookDetail using this navigation prop

        //[ForeignKey("Publisher")]
        public int Publisher_Id { get; set; } //foreign key
        public Fluent_Publisher Publisher { get; set; } //navigation prop 1-to-Many
                                                        //from child Book we can extract its only parent Publisher using this navigation prop

        //public List<Fluent_Author> Authors { get; set; } //navigation prop Many-to-Many for Skip


        public List<Fluent_BookAuthorMap> BookAuthorMap { get; set; } //navigation prop Many-to-Many with Manual

    }
}
