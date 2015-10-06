using Core.Category;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Category
{
    public class CategoryImagesMap : EntityTypeConfiguration<CategoryImages>
    {
        public CategoryImagesMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryImageID);

            // Properties
            this.Property(t => t.Title)
                .IsRequired();

            this.Property(t => t.ImageName)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CakeApp_CategoryImages");
            this.Property(t => t.CategoryImageID).HasColumnName("CategoryImageID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.ImageAlt).HasColumnName("ImageAlt");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");
            // Relationships
           
            //this.HasOptional(t => t.Categories)
            //    .WithMany(t => t.CategoryImages)
            //    .HasForeignKey(d => d.CategoryID);

        }
    }
}
