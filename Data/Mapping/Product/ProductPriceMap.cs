using Core.Product;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.Product
{
    public class ProductPriceMap : EntityTypeConfiguration<ProductPrice>
    {
       public ProductPriceMap()
       {
           this.HasKey(t => t.ProductPriceID);

           this.ToTable("CakeApp_ProductPrice");
           this.Property(t => t.ProductID).HasColumnName("ProductID");
           this.Property(t => t.GroupID).HasColumnName("GroupID");
           this.Property(t => t.InfoIDs).HasColumnName("InfoIDs");
           this.Property(t => t.SizeID).HasColumnName("SizeID");
           this.Property(t => t.Price).HasColumnName("Price");

           this.HasOptional(t => t.Group)
               .WithMany(t => t.ProductPrice)
               .HasForeignKey(d => d.GroupID);

           this.HasOptional(t => t.Product)
               .WithMany(t => t.ProductPrice)
               .HasForeignKey(t => t.ProductID);

           //this.HasOptional(t => t.Infos).WithMany(t => t.ProductPrice).HasForeignKey(t => t.InfoID);

           this.HasOptional(t => t.Sizes).WithMany(t => t.ProductPrice).HasForeignKey(t => t.SizeID);



       }
    }
}
