using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IKEA.DAL.Common.Enums;
using IKEA.DAL.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.DAL.persistance.Data.Configurations.EmployeeConfigurations
{
    public class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(e => e.Address).HasColumnType("varchar(100)").IsRequired();
            builder.Property(e => e.Salary).HasColumnType("decimal(8,2)").IsRequired();
            builder.Property(e => e.Gender).HasConversion
                (
                  (gender)=>gender.ToString(),
                  (gender) => (Gender)Enum.Parse(typeof(Gender), gender)
                );
            builder.Property(e => e.EmployeeType).HasConversion
                (
                  (Type) => Type.ToString(),
                  (Type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), Type)
                );
            builder.Property(D => D.CreatedOn).HasDefaultValueSql("Getdate()");
            builder.Property(D => D.LastModifiedOn).HasComputedColumnSql("Getdate()");

        }
    }
}
