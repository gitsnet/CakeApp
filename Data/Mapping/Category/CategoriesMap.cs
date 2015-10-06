using Core.Category;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Category
{
    public class CategoriesMap : EntityTypeConfiguration<Categories>
    {
        public CategoriesMap()
        {
            // Primary Key
            this.HasKey(t => t.CategoryID);

            // Properties
            this.Property(t => t.CategoryName)
                .IsRequired();

            this.Property(t => t.Title)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("CakeApp_Categories");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");
            this.Property(t => t.CategoryName).HasColumnName("CategoryName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TagID).HasColumnName("TagID");          
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.AddedBy).HasColumnName("AddedBy");
            this.Property(t => t.CategoryImageName).HasColumnName("CategoryImageName");

            // Relationships
            //this.HasOptional(t => t.Tags)
            //    .WithMany(t => t.Categories)
            //    .HasForeignKey(d => d.TagID);
            //this.HasOptional(t => t.Users)
            //    .WithMany(t => t.Categories)
            //    .HasForeignKey(d => d.AddedBy);
            //this.HasOptional(t => t.CategoryImages)
            //  .WithMany(t => t.Categories)
            //  .HasForeignKey(d => d.CategoryImageID);

            // Relationships
            this.HasOptional(t => t.CategoryImages)
                .WithMany(t => t.Categories)
                .HasForeignKey(d => d.CategoryImageID);
            this.HasOptional(t => t.Tags)
                .WithMany(t => t.Categories)
                .HasForeignKey(d => d.TagID);
            
         

        }
    }
}
