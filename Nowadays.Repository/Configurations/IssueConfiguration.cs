using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nowadays.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nowadays.Repository.Configurations;

public class IssueConfiguration : IEntityTypeConfiguration<Issue>
{
    public void Configure(EntityTypeBuilder<Issue> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();

        builder.HasIndex(indexExpression: x => x.Name, name: "UK_Issue_Name").IsUnique();

        builder.HasOne(x => x.Company)
               .WithMany(x => x.Issues)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Project)
               .WithMany(x => x.Issues)
               .HasForeignKey(x => x.ProjectId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
