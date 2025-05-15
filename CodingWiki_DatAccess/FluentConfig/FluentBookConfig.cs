using CodingWiki_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingWiki_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Fluent_Book>
    {
        public void Configure(EntityTypeBuilder<Fluent_Book> modelBuilder)
        {
            modelBuilder.Property(u => u.ISBN).HasMaxLength(50); //we impose max length of string
            modelBuilder.Property(u => u.ISBN).IsRequired(); //we set the column as required
            modelBuilder.HasKey(u => u.BookId); //we set the PK
            modelBuilder.Ignore(u => u.PriceRange); //we don't map this prop to a table column in db
            modelBuilder.HasOne(u => u.Publisher).WithMany(u => u.Books)
                .HasForeignKey(u => u.Publisher_Id); //we set the relation 1-to-Many with Publisher
        }
    }
}
