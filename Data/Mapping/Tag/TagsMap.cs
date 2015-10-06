using Core.Tag;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Tag
{
    public class TagsMap : EntityTypeConfiguration<Tags>
    {
        public TagsMap()
        {
            // Primary Key
            this.HasKey(t => t.TagID);

            // Properties
            this.Property(t => t.TagName)
                .IsRequired()
                .HasMaxLength(128);

            this.Property(t => t.Title)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CakeApp_Tags");
            this.Property(t => t.TagID).HasColumnName("TagID");
            this.Property(t => t.TagName).HasColumnName("TagName");
            this.Property(t => t.Title).HasColumnName("Title");
        }
    }
}
