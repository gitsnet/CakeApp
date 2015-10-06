using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Screensaver;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace Data.Mapping.Screensaver
{
    public class ScreensaversMap : EntityTypeConfiguration<Sreensavers>
    {
        public ScreensaversMap()
        {
            this.HasKey(t => t.ScreensaverID);

            this.Property(t => t.Name).IsRequired();

            this.Property(t => t.Title).IsRequired();

            this.ToTable("CakeApp_Screensavers");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Video).HasColumnName("Video");

        }
    }
}
