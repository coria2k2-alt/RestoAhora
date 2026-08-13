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
    public DbSet<Producto> Producto => Set<Producto>();
    public DbSet<CategoriaProducto> CategoriaProducto => Set<CategoriaProducto>();
    public DbSet<Pedidos> Pedidos => Set<Pedidos>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();

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

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(p => p.id);
            entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(p => p.Precio).HasColumnType("decimal(18,2)");

            entity.HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(produc => produc.CategoriaProductoID)
            .OnDelete(DeleteBehavior.Restrict);
    
        });

        modelBuilder.Entity<CategoriaProducto>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
        });


        modelBuilder.Entity<Pedidos>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.FechaHora).IsRequired();

            entity.Property(p => p.estado).IsRequired();

            entity.HasOne(p => p.Mesa)
                  .WithMany() 
                  .HasForeignKey(p => p.MesaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.Cantidad).IsRequired();

            entity.Property(d => d.PrecioUnitario)
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

            // Relación: Un Detalle pertenece a un Pedido
            entity.HasOne(d => d.Pedido)
                  .WithMany(p => p.Detalles)
                  .HasForeignKey(d => d.PedidoId)
                  .OnDelete(DeleteBehavior.Cascade); // Si borrás el pedido, borra sus detalles

            // Relación: Un Detalle pertenece a un Producto
            entity.HasOne(d => d.producto)
                  .WithMany()
                  .HasForeignKey(d => d.productoId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

