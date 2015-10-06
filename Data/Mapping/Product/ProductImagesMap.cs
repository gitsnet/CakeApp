using Core.Product;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Product
{
    public class ProductImagesMap : EntityTypeConfiguration<ProductImages>
    {
        public ProductImagesMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductImageID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_ProductImages");
            this.Property(t => t.ProductImageID).HasColumnName("ProductImageID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ImageName).HasColumnName("ImageName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.ImageAlt).HasColumnName("ImageAlt");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            this.HasOptional(t => t.Products)
                .WithMany(t => t.ProductImages)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
