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
              new Category { Id = Guid.NewGuid(), Value = "Politics" },
              new Category { Id = Guid.NewGuid(), Value = "Economics" },
              new Category { Id = Guid.NewGuid(), Value = "India" },
              new Category { Id = Guid.NewGuid(), Value = "World" },
              new Category { Id = Guid.NewGuid(), Value = "Sports" },
              new Category { Id = Guid.NewGuid(), Value = "Random" },
              new Category { Id = Guid.NewGuid(), Value = "Entertainment" },
              new Category { Id = Guid.NewGuid(), Value = "Good Life" },
              new Category { Id = Guid.NewGuid(), Value = "Fashion And Style" },
              new Category { Id = Guid.NewGuid(), Value = "Writing" },
              new Category { Id = Guid.NewGuid(), Value = "Computers" },
              new Category { Id = Guid.NewGuid(), Value = "Philosophy" }
            );
        }
    }
}