using Core.Infos;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Data.Mapping.Infos
{
    public class InfoMap : EntityTypeConfiguration<Info>
    {
        public InfoMap()
        {
            // Primary Key
            this.HasKey(t => t.InfoID);

            // Properties
            this.Property(t => t.InfoName)
                .HasMaxLength(128);

            this.Property(t => t.Title)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("CakeApp_Info");
            this.Property(t => t.InfoID).HasColumnName("InfoID");
            this.Property(t => t.InfoName).HasColumnName("InfoName");
            this.Property(t => t.Title).HasColumnName("Title");
            
            this.Property(t => t.Priority).HasColumnName("Priority");
            this.Property(t => t.Status).HasColumnName("Status");

            // Relationships
            //this.HasOptional(t => t.Groups)
            //    .WithMany(t => t.Info)
            //    .HasForeignKey(d => d.GroupID);

        }
    }
}
