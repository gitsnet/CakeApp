using Core.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.User
{
    public partial class UserRoleMap : EntityTypeConfiguration<Role>
    {
        public UserRoleMap()
        {
            //this.ToTable("CakeApp_Roles");
            //this.HasKey(ur => ur.RoleID);

            this.HasKey(t => t.RoleID);

            // Properties
            this.Property(t => t.RoleName)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("CakeApp_Roles");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.RoleName).HasColumnName("RoleName");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
