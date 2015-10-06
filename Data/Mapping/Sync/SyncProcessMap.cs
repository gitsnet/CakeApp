using Core.Sync;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Sync
{
    public class SyncProcessMap : EntityTypeConfiguration<SyncProcess>
    {
        public SyncProcessMap()
        {
            // Primary Key
            this.HasKey(t => t.SyncProcessID);

            // Properties
            // Table & Column Mappings
            this.ToTable("CakeApp_SyncProcess");
            this.Property(t => t.SyncProcessID).HasColumnName("SyncProcessID");
            this.Property(t => t.SyncID).HasColumnName("SyncID");
            this.Property(t => t.ProductID).HasColumnName("ProductID");
            this.Property(t => t.SyncStatus).HasColumnName("SyncStatus");

            // Relationships
            //this.HasOptional(t => t.Products)
            //    .WithMany(t => t.SyncProcess)
            //    .HasForeignKey(d => d.ProductID);
            this.HasOptional(t => t.SyncSchedule)
                .WithMany(t => t.SyncProcess)
                .HasForeignKey(d => d.SyncID); 

        }
    }
}
