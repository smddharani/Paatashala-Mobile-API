using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebAPI3.Models.Mapping
{
    public class tblChallan_InvoiceDetailMap : EntityTypeConfiguration<tblChallan_InvoiceDetail>
    {
        public tblChallan_InvoiceDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ItemName)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblChallan_InvoiceDetail");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.ItemCode).HasColumnName("ItemCode");
            this.Property(t => t.ItemName).HasColumnName("ItemName");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Unit).HasColumnName("Unit");
            this.Property(t => t.RatePerUnit).HasColumnName("RatePerUnit");
            this.Property(t => t.DiscountAmount).HasColumnName("DiscountAmount");
            this.Property(t => t.Amount).HasColumnName("Amount");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
        }
    }
}
