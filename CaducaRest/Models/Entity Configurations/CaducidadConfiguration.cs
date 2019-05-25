﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaducaRest.Models.EntityConfigurations
{
    public class CaducidadConfiguration : IEntityTypeConfiguration<Caducidad>
    {


        public void Configure(EntityTypeBuilder<Caducidad> builder)
        {
            builder.HasOne(c => c.Cliente)
                     .WithMany(cc => cc.Caducidades)
                     .OnDelete(DeleteBehavior.Restrict);

             builder.HasOne(e => e.Producto)
               .WithMany(c => c.Caducidades)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasIndex(e => new { e.ClienteId, e.ProductoId })
               .HasName("UI_ClienteProducto")
               .IsUnique();
        }
    }
}
