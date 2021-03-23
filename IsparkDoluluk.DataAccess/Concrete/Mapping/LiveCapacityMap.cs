using IsparkDoluluk.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.DataAccess.Concrete.Mapping
{
    public class LiveCapacityMap : IEntityTypeConfiguration<LiveCapacity>
    {
        public void Configure(EntityTypeBuilder<LiveCapacity> builder)
        {
            builder.HasKey(I => I.Id);
            builder.Property(I => I.Id).UseMySqlIdentityColumn();
        }
    }
}
