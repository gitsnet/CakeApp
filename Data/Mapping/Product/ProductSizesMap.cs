using Core.Product;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Product
{
    public class ProductSizesMap : EntityTypeConfiguration<ProductSizes>
    {
        public ProductSizesMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductSizeID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_ProductSizes");
            this.Property(t => t.ProductSizeID).HasColumnName("ProductSizeID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.SizeID).HasColumnName("SizeID");

            // Relationships
            this.HasOptional(t => t.Products)
                .WithMany(t => t.ProductSizes)
                .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.Sizes)
                .WithMany(t => t.ProductSizes)
                .HasForeignKey(d => d.SizeID);

        }
    }
}
