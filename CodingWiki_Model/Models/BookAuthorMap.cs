﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_Model.Models
{
    public class BookAuthorMap
    {
        [ForeignKey("Book")]
        public int Book_Id { get; set; }
        [ForeignKey("Author")]
        public int Author_Id { get; set; }
        /*
         * e.g. of other props like "CreatedDateTime" for the record in db
         * which we can add in this intermediary table because we create it manually !!!
        */


        //relations
        public virtual Book Book { get; set; } //navigation prop Many-to-Many
        public virtual Author Author { get; set; } //navigation prop Many-to-Many
    }
}
