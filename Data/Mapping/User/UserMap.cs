using Core.User;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.User
{
    public partial class UserMap : EntityTypeConfiguration<Users>
    {
        public UserMap()
        {
            //this.ToTable("CakeApp_Users");
            //this.HasKey(u => u.UserID);

            this.HasKey(t => t.UserID);

            // Properties
            this.Property(t => t.Username)
                .HasMaxLength(250);

            this.Property(t => t.Password)
                .HasMaxLength(250);

            this.Property(t => t.Email)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("CakeApp_Users");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Username).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.RoleID).HasColumnName("RoleID");
            this.Property(t => t.Status).HasColumnName("Status");

             //Relationships
            //this.HasOptional(t => t.Role)
            //    .WithMany(t => t.Users)
            //    .HasForeignKey(d => d.RoleID);
        }
    }
}
