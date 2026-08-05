using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    //CONSTRUCTOR
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    //CREA ENTIDADES
    public DbSet<Mesa> Mesas => Set<Mesa>();
    
    public DbSet<ReservaMesa> Reservas => Set<ReservaMesa>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Numero).IsRequired().HasMaxLength(10);

            entity.Property(m => m.Ubicacion).HasMaxLength(100);
        });

        modelBuilder.Entity<ReservaMesa>(entity =>
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.NombreCliente).IsRequired().HasMaxLength(100);
            entity.Property(r => r.EmailCliente).HasMaxLength(100);

            entity.Property(r => r.MontoSeña).HasColumnType("decimal(18,2)");

            entity.HasOne(r => r.Mesa)                      
                  .WithMany(m => m.Reservas)                
                  .HasForeignKey(r => r.MesaId)            
                  .OnDelete(DeleteBehavior.Restrict);        
        });
    }
}

