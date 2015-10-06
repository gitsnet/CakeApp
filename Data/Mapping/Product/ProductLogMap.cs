using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Product;

namespace Data.Mapping.Product
{
    public class ProductLogMap:EntityTypeConfiguration<ProductLog>
    {
        public ProductLogMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductLogID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_ProductLog");
            this.Property(t => t.ProductLogID).HasColumnName("ProductLogID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.IsAdded).HasColumnName("IsAdded");
            this.Property(t => t.IsUpdated).HasColumnName("IsUpdated");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            this.Property(t => t.DateOfModification).HasColumnName("DateOfModification");
            // Relationships
            // Relationships
            this.HasOptional(t => t.Products)
                .WithMany(t => t.ProductLog)
                .HasForeignKey(d => d.ProductID);
           

        }
    }
}
