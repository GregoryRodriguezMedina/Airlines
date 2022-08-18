using AirLines.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLines.Infrastructure.Data.map
{
    internal class AirPortMap
    {
        public void Map(EntityTypeBuilder<AirPort> builder)
        {
            
            builder.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);            
        }

    }
}
