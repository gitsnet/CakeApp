using Core.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.Product
{
    class ProductCategoriesMap : EntityTypeConfiguration<ProductCategories>
    {
        public ProductCategoriesMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductCategoryID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_ProductCategories");
            this.Property(t => t.ProductCategoryID).HasColumnName("ProductCategoryID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.CategoryID).HasColumnName("CategoryID");

            // Relationships
            //this.HasOptional(t => t.Categories)
            //    .WithMany(t => t.ProductCategories)
            //    .HasForeignKey(d => d.CategoryID);
            this.HasOptional(t => t.Products)
                .WithMany(t => t.ProductCategories)
                .HasForeignKey(d => d.ProductID);

        }
    }
}
