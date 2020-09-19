using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntitiesConfiguration
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
              new Category { Id = Guid.NewGuid(), Value = "Politics", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Economics", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "India", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "World", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Sports", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Random", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Entertainment", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Good Life", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Fashion And Style", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Writing", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Computers", IsActive= true },
              new Category { Id = Guid.NewGuid(), Value = "Philosophy", IsActive= true }
            );
        }
    }
}