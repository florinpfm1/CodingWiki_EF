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
    public class FluentAuthorConfig : IEntityTypeConfiguration<Fluent_Author>
    {
        public void Configure(EntityTypeBuilder<Fluent_Author> modelBuilder)
        {
            modelBuilder.Property(u => u.FirstName).HasMaxLength(50); //we impose max length of string
            modelBuilder.Property(u => u.FirstName).IsRequired(); //we set the column as required
            modelBuilder.Property(u => u.LastName).IsRequired(); //we set the column as required
            modelBuilder.HasKey(u => u.Author_Id); //we set the PK
            modelBuilder.Ignore(u => u.FullName); //we don't map this prop to a table column in db
        }
    }
}
