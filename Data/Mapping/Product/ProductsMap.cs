using Core.Product;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Product
{
    public class ProductsMap : EntityTypeConfiguration<Products>
    {
        public ProductsMap()
        {
            // Primary Key
            this.HasKey(t => t.ProductID);

            // Properties
            this.Property(t => t.ProductName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CakeApp_Products");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.ProductName).HasColumnName("ProductName");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.TagID).HasColumnName("TagID");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.AdditionalInfo).HasColumnName("AdditionalInfo");
            this.Property(t => t.Instructions).HasColumnName("Instructions");
            this.Property(t => t.AddedBy).HasColumnName("AddedBy");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.statusAdditionalInfo).HasColumnName("statusAdditionalInfo");
            this.Property(t => t.statusInstructions).HasColumnName("statusInstructions");
            this.Property(t => t.ProductImageName).HasColumnName("ProductImageName");

            // Relationships
            this.HasOptional(t => t.Groups)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.GroupID);
            this.HasOptional(t => t.Tags)
                .WithMany(t => t.Products)
                .HasForeignKey(d => d.TagID);
            //this.HasOptional(t => t.Users)
            //    .WithMany(t => t.Products)
            //    .HasForeignKey(d => d.AddedBy);
            //this.HasOptional(t => t.Groups)
            //    .WithMany(t => t.Products)
            //    .HasForeignKey(d => d.GroupID);
            //this.HasOptional(t => t.Tags)
            //    .WithMany(t => t.Products)
            //    .HasForeignKey(d => d.TagID);

        }
    }
}
