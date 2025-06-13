using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Domain.Products.Entities;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;

public partial class NegocioDbContext : DbContext
{
    public NegocioDbContext()
    {
    }

    public NegocioDbContext(DbContextOptions<NegocioDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Clientes> Clientes { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }

    public virtual DbSet<IngresosProducto> IngresosProducto { get; set; }

    public virtual DbSet<Logs> Logs { get; set; }

    public virtual DbSet<MovimientosInventario> MovimientosInventario { get; set; }

    public virtual DbSet<Negocios> Negocios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedores> Proveedores { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    public virtual DbSet<Ventas> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=smartstockai_db;Username=robertoflores;Password=302630");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categorias_pkey");

            entity.ToTable("categorias");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Clientes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clientes_pkey");

            entity.ToTable("clientes");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("detalle_venta_pkey");

            entity.ToTable("detalle_venta");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DescuentoAplicado).HasColumnName("descuento_aplicado");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario).HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("detalle_venta_id_producto_fkey");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("detalle_venta_id_venta_fkey");
        });

        modelBuilder.Entity<IngresosProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ingresos_producto_pkey");

            entity.ToTable("ingresos_producto");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ingreso");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.PrecioCompra).HasColumnName("precio_compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.IngresosProducto)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("ingresos_producto_id_producto_fkey");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.IngresosProducto)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("ingresos_producto_id_proveedor_fkey");
        });

        modelBuilder.Entity<Logs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_pkey");

            entity.ToTable("logs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Accion).HasColumnName("accion");
            entity.Property(e => e.Fecha)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");
            entity.Property(e => e.TablaAfectada)
                .HasMaxLength(100)
                .HasColumnName("tabla_afectada");
            entity.Property(e => e.Usuario)
                .HasMaxLength(20)
                .HasColumnName("usuario");

            entity.HasOne(d => d.UsuarioNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.Usuario)
                .HasConstraintName("logs_usuario_fkey");
        });

        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("movimientos_inventario_pkey");

            entity.ToTable("movimientos_inventario");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaMovimiento)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_movimiento");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Observacion).HasColumnName("observacion");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(50)
                .HasColumnName("tipo_movimiento");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientosInventario)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("movimientos_inventario_id_producto_fkey");
        });

        modelBuilder.Entity<Negocios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("negocios_pkey");

            entity.ToTable("negocios");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.LogoUrl).HasColumnName("logo_url");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(150)
                .HasColumnName("nombre_comercial");
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .HasColumnName("ruc");
            entity.Property(e => e.UsuarioAdmin)
                .HasMaxLength(20)
                .HasColumnName("usuario_admin");

            entity.HasOne(d => d.UsuarioAdminNavigation).WithMany(p => p.Negocios)
                .HasForeignKey(d => d.UsuarioAdmin)
                .HasConstraintName("negocios_usuario_admin_fkey");
        });

        modelBuilder.Entity<Productos>(entity =>
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

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("productos_id_categoria_fkey");
        });

        modelBuilder.Entity<Proveedores>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("proveedores_pkey");

            entity.ToTable("proveedores");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.NombreEmpresa)
                .HasMaxLength(150)
                .HasColumnName("nombre_empresa");
            entity.Property(e => e.Ruc)
                .HasMaxLength(20)
                .HasColumnName("ruc");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.Property(e => e.Dni)
                .HasMaxLength(20)
                .HasColumnName("dni");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .HasColumnName("apellido");
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .HasColumnName("celular");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion).HasColumnName("direccion");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(150)
                .HasColumnName("razon_social");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("usuarios_id_rol_fkey");
        });

        modelBuilder.Entity<Ventas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ventas_pkey");

            entity.ToTable("ventas");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_venta");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(20)
                .HasColumnName("id_usuario");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(50)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.TotalVenta).HasColumnName("total_venta");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Ventas)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("ventas_id_usuario_fkey");
        });
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Producto>()
            .HasOne(p => p.Categoria)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.IdCategoria);
        
        modelBuilder.Ignore<SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities.Categorias>();
        modelBuilder.Ignore<SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio.Entities.Productos>();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
