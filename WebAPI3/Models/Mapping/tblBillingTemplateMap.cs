using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebAPI3.Models.Mapping
{
    public class tblBillingTemplateMap : EntityTypeConfiguration<tblBillingTemplate>
    {
        public tblBillingTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TemplateName)
                .HasMaxLength(50);

            this.Property(t => t.Heading)
                .HasMaxLength(50);

            this.Property(t => t.AddressLine1)
                .HasMaxLength(500);

            this.Property(t => t.AddressLine2)
                .HasMaxLength(500);

            this.Property(t => t.City)
                .HasMaxLength(50);

            this.Property(t => t.State)
                .HasMaxLength(50);

            this.Property(t => t.Zip)
                .HasMaxLength(50);

            this.Property(t => t.TermsAndConditions)
                .HasMaxLength(500);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tblBillingTemplate");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TemplateName).HasColumnName("TemplateName");
            this.Property(t => t.Heading).HasColumnName("Heading");
            this.Property(t => t.AddressLine1).HasColumnName("AddressLine1");
            this.Property(t => t.AddressLine2).HasColumnName("AddressLine2");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.State).HasColumnName("State");
            this.Property(t => t.Zip).HasColumnName("Zip");
            this.Property(t => t.TermsAndConditions).HasColumnName("TermsAndConditions");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.OrgPhoto).HasColumnName("OrgPhoto");
            this.Property(t => t.OrgId).HasColumnName("OrgId");
        }
    }
}
