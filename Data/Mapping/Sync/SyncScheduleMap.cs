using Core.Sync;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Sync
{
    public class SyncScheduleMap : EntityTypeConfiguration<SyncSchedule>
    {
        public SyncScheduleMap()
        {
            // Primary Key
            this.HasKey(t => t.SyncID);

            // Properties
            this.Property(t => t.SyncName)
                .HasMaxLength(128);

            this.Property(t => t.Time)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("CakeApp_SyncSchedule");
            this.Property(t => t.SyncID).HasColumnName("SyncID");
            this.Property(t => t.SyncName).HasColumnName("SyncName");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.Time).HasColumnName("Time");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
