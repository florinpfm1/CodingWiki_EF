using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class Publisher
    {
        [Key]
        public int Publisher_Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Location { get; set; }

        //relations
        public virtual List<Book> Books { get; set; } //navigation prop 1-to-Many
                                              //from parent Publisher class we can extract all its children Books using this navigation prop
    }
}
