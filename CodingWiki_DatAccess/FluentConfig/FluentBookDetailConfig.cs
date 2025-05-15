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
    public class FluentBookDetailConfig : IEntityTypeConfiguration<Fluent_BookDetail>
    {
        public void Configure(EntityTypeBuilder<Fluent_BookDetail> modelBuilder)
        {
            //name of table
            modelBuilder.ToTable("Fluent_BookDetails"); //we change the name of table to Fluent_BookDetails
            //name of columns
            modelBuilder.Property(u => u.NumberOfChapters).HasColumnName("NoOfChapters"); //we change the name of a column to NoOfChapters
            //primary key
            modelBuilder.HasKey(u => u.BookDetail_Id); //we set the PK as BookDetail_Id
            //other validations
            modelBuilder.Property(u => u.NumberOfPages).IsRequired(); //we set the column as required
            //relations
            //note that since we setup the relation 1-to-1 in Fluent_BookDetail -->> we don't need to also set it up in Fluent_Book
            modelBuilder.HasOne(b => b.Book).WithOne(b => b.BookDetail)
                .HasForeignKey<Fluent_BookDetail>(u => u.Book_Id); //we set the relation 1-to-1 with Book
        }
    }
}
