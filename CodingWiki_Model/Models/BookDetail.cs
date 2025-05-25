using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class BookDetail
    {
        [Key]
        public int BookDetail_Id { get; set; }
        [Required]
        public int NumberOfChapters { get; set; }
        public int NumberOfPages { get; set; }
        public string Weight { get; set; }

        //relations
        [ForeignKey("Book")]
        public int Book_Id { get; set; } //foreign key
        public virtual Book Book { get; set; } //navigation prop 1-to-1
                                       //from child BookDetail class we can extract its only parent Book using this navigation prop
    }
}
