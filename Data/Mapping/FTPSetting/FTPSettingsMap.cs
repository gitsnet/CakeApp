using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Core.Product;
using Core.FTPSetting;

namespace Data.Mapping.FTPSetting
{
    public partial class FTPSettingsMap : EntityTypeConfiguration<FTPSettings>
    {
        public FTPSettingsMap()
        {
            // Primary Key
            this.HasKey(t => t.FTPSettingsID);

            // Properties
            this.Property(t => t.HostName)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.FTPPassword)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.FolderName)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("CakeApp_FTPSettings");
            this.Property(t => t.FTPSettingsID).HasColumnName("FTPSettingsID");
            this.Property(t => t.HostName).HasColumnName("HostName");
            this.Property(t => t.FTPPassword).HasColumnName("FTPPassword");
            this.Property(t => t.FolderName).HasColumnName("FolderName");

            
        }
    }
}
