using Core.Group;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Group
{
    public class GroupsMap : EntityTypeConfiguration<Groups>
    {
        public GroupsMap()
        {
            // Primary Key
            this.HasKey(t => t.GroupID);

            // Properties
            this.Property(t => t.Title)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CakeApp_Groups");
            this.Property(t => t.GroupID).HasColumnName("GroupID");
            this.Property(t => t.GroupName).HasColumnName("GroupName");
            this.Property(t => t.Title).HasColumnName("Title");
        }
    }
}
