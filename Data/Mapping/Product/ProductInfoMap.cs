using Core.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.Product
{
    public class ProductInfoMap : EntityTypeConfiguration<ProductInfo>
    {
        public ProductInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductInfoID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_ProductInfo");
            this.Property(t => t.ProductInfoID).HasColumnName("ProductInfoID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.InfoID).HasColumnName("InfoID");

            // Relationships
            this.HasOptional(t => t.Info)
                .WithMany(t => t.ProductInfo)
                .HasForeignKey(d => d.InfoID);
            this.HasOptional(t => t.Products)
                .WithMany(t => t.ProductInfo)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
