using Core.Infos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.Infos
{
    public class InfoGroupsMap:EntityTypeConfiguration<InfoGroups>
    {
        public InfoGroupsMap()
        {
            // Primary Key
            this.HasKey(t => t.InfoGroupID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_InfoGroup");
            this.Property(t => t.InfoGroupID).HasColumnName("InfoGroupID");
            this.Property(t => t.InfoID).HasColumnName("InfoID");
            this.Property(t => t.GroupID).HasColumnName("GroupID");

            // Relationships
            this.HasOptional(t => t.Groups)
                .WithMany(t => t.InfoGroups)
                .HasForeignKey(d => d.GroupID);
            //this.HasOptional(t => t.CakeApp_Groups1)
            //    .WithMany(t => t.CakeApp_InfoGroup1)
            //    .HasForeignKey(d => d.GroupID);
            this.HasOptional(t => t.Info)
                .WithMany(t => t.InfoGroups)
                .HasForeignKey(d => d.InfoID);

        }
    }
}
