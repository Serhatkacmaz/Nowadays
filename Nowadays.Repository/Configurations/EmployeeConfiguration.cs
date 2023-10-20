using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nowadays.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Repository.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();

        builder.HasIndex(indexExpression: x => x.Name, name: "UK_Employee_Name").IsUnique();

        builder.HasOne(x => x.Company)
               .WithMany(x => x.Employees)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
