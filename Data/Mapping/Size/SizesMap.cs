using Core.Size;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Size
{
    public class SizesMap : EntityTypeConfiguration<Sizes>
    {
        public SizesMap()
        {
            // Primary Key
            this.HasKey(t => t.SizeID);

            // Properties
            this.Property(t => t.Size)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Title)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CakeApp_Sizes");
            this.Property(t => t.SizeID).HasColumnName("SizeID");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Price).HasColumnName("Price");
        }
    }
}
