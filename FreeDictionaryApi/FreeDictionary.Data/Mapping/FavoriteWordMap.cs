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
    public class FavoriteWordMap : IEntityTypeConfiguration<FavoriteWord>
    {
        public void Configure(EntityTypeBuilder<FavoriteWord> builder)
        {
            builder.ToTable("TbFavoriteWord");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Word)
                .IsRequired()
                .HasColumnName("Word")
                .HasColumnType("VARCHAR(256)");

            builder.Property(x => x.UserId)
                .IsRequired()
                .HasColumnName("UserId")
                .HasColumnType("UNIQUEIDENTIFIER");

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
