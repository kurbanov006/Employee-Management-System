using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeConfig : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(255);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(255);

        builder.HasAlternateKey(x => x.Email);
        builder.Property(x => x.Email).HasMaxLength(255);
        builder.HasAlternateKey(x => x.Phone);
        builder.Property(x=>x.Phone).HasMaxLength(13);

        builder.Property(x => x.Position).IsRequired();
        builder.Property(x => x.Salary).IsRequired();
        builder.Property(x => x.Address).IsRequired().HasMaxLength(100);
        builder.Property(x => x.City).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Country).IsRequired().HasMaxLength(150);
    }
}