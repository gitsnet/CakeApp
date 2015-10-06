using Core.Size;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Mapping.Size
{
    public class SizeGroupMap : EntityTypeConfiguration<SizeGroup>
    {
        public SizeGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.SizeGroupID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_SizeGroup");
            this.Property(t => t.SizeGroupID).HasColumnName("SizeGroupID");
            this.Property(t => t.SizeID).HasColumnName("SizeID");
            this.Property(t => t.GroupID).HasColumnName("GroupID");

            // Relationships
            this.HasOptional(t => t.Groups)
                .WithMany(t => t.SizeGroup)
                .HasForeignKey(d => d.GroupID);
            this.HasOptional(t => t.Sizes)
                .WithMany(t => t.SizeGroup)
                .HasForeignKey(d => d.SizeID);

        }
    }
}
