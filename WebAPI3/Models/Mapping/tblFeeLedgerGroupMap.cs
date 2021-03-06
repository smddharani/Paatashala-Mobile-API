using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebAPI3.Models.Mapping
{
    public class tblFeeLedgerGroupMap : EntityTypeConfiguration<tblFeeLedgerGroup>
    {
        public tblFeeLedgerGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LedgerCode)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblFeeLedgerGroup");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.LedgerCode).HasColumnName("LedgerCode");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
            this.Property(t => t.isBilling).HasColumnName("isBilling");

            // Relationships
            this.HasRequired(t => t.tblOrg)
                .WithMany(t => t.tblFeeLedgerGroups)
                .HasForeignKey(d => d.OrgId);

        }
    }
}
