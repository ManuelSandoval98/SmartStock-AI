using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Common.Interfaces;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Domain.Products.Entities;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;

public partial class NegocioDbContext : DbContext, INegocioDbContext
{
    public NegocioDbContext()
    {
        
    }

    public NegocioDbContext(DbContextOptions<NegocioDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    
    // 👉 Implementación de los métodos de la interfaz
    public IQueryable<T> Get<T>() where T : class
        => Set<T>().AsQueryable();

    public async Task AddAsync<T>(T entity) where T : class
        => await Set<T>().AddAsync(entity);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => base.SaveChangesAsync(cancellationToken);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=smartstockai_db;Username=robertoflores;Password=302630");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("productos_pkey");

            entity.ToTable("productos");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CodProducto)
                .HasMaxLength(50)
                .HasColumnName("cod_producto");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.FechaEgreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_egreso");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdCategoria).HasColumnName("id_categoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioCompra).HasColumnName("precio_compra");
            entity.Property(e => e.PrecioDescuento).HasColumnName("precio_descuento");
            entity.Property(e => e.PrecioVenta).HasColumnName("precio_venta");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.Umbral).HasColumnName("umbral");

            entity.HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(p => p.IdCategoria)
                .HasConstraintName("productos_id_categoria_fkey");
        });
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.IdCategoria);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
