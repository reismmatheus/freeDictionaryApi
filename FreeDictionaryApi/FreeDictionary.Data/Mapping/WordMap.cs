using FreeDictionary.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeDictionary.Data.Mapping
{
    public class WordMap : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.ToTable("TbWord");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR(256)");

            builder.Property(x => x.CreatedIn)
              .IsRequired()
              .HasColumnName("CreatedIn")
              .HasColumnType("DATETIME");

            builder.Property(x => x.UpdatedIn)
              .HasColumnName("UpdatedIn")
              .HasColumnType("DATETIME");

            builder.Property(x => x.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("IsDeleted")
                .HasColumnType("BIT");
        }
    }
}
