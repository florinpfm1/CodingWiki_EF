using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Fluent_Author
    {
        //[Key]
        public int Author_Id { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string FirstName { get; set; }
        //[Required]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Location { get; set; }
        //[NotMapped]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //relations


        //public List<Fluent_Book> Books { get; set; } //navigation prop Many-to-Many for Skip


        public virtual List<Fluent_BookAuthorMap> BookAuthorMap { get; set; } //navigation prop Many-to-Many for Manual
    }
}
