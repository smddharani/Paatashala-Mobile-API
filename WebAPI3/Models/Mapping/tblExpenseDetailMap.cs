using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebAPI3.Models.Mapping
{
    public class tblExpenseDetailMap : EntityTypeConfiguration<tblExpenseDetail>
    {
        public tblExpenseDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblExpenseDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ExpenseName).HasColumnName("ExpenseName");
            this.Property(t => t.Category).HasColumnName("Category");
            this.Property(t => t.Value).HasColumnName("Value");
            this.Property(t => t.Date).HasColumnName("Date");
            this.Property(t => t.OrgId).HasColumnName("OrgId");

            // Relationships
            this.HasOptional(t => t.tblExpenseCategory)
                .WithMany(t => t.tblExpenseDetails)
                .HasForeignKey(d => d.Category);
            this.HasOptional(t => t.tblOrg)
                .WithMany(t => t.tblExpenseDetails)
                .HasForeignKey(d => d.OrgId);

        }
    }
}
