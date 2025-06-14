using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;
using NegocioLoginTracking = SmartStock_AI.Domain.Authentication.Entities.NegocioLoginTracking;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;

public partial class AdminDbContext : DbContext
{
    public AdminDbContext()
    {
    }

    public AdminDbContext(DbContextOptions<AdminDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Adminlogs> Adminlogs { get; set; }

    public virtual DbSet<Domain.Authentication.Entities.NegocioLoginTracking> NegocioLoginTracking { get; set; }

    public virtual DbSet<Domain.Authentication.Entities.Negocio> Negocios { get; set; }

    public virtual DbSet<Sesiones> Sesiones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=smartstock_admin;Username=robertoflores;Password=302630");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Adminlogs>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("adminlogs_pkey");

            entity.ToTable("adminlogs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Accion).HasColumnName("accion");
            entity.Property(e => e.CorreoAdmin)
                .HasMaxLength(150)
                .HasColumnName("correo_admin");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha");

            entity.HasOne(d => d.CorreoAdminNavigation).WithMany(p => p.Adminlogs)
                .HasPrincipalKey(p => p.CorreoAdmin)
                .HasForeignKey(d => d.CorreoAdmin)
                .HasConstraintName("adminlogs_correo_admin_fkey");
        });

        modelBuilder.Entity<NegocioLoginTracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("negocio_login_tracking_pkey");

            entity.ToTable("negocio_login_tracking");

            entity.HasIndex(e => e.CorreoAdmin, "negocio_login_tracking_correo_admin_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BloqueadoHasta)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("bloqueado_hasta");
            entity.Property(e => e.CorreoAdmin)
                .HasMaxLength(150)
                .HasColumnName("correo_admin");
            entity.Property(e => e.IntentosFallidos)
                .HasDefaultValue(0)
                .HasColumnName("intentos_fallidos");
        });

        modelBuilder.Entity<Negocios>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("negocios_pkey");

            entity.ToTable("negocios");

            entity.HasIndex(e => e.CorreoAdmin, "negocios_correo_admin_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CorreoAdmin)
                .HasMaxLength(150)
                .HasColumnName("correo_admin");
            entity.Property(e => e.DbAsociada)
                .HasMaxLength(100)
                .HasColumnName("db_asociada");
            entity.Property(e => e.Estado)
                .HasDefaultValue(true)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(150)
                .HasColumnName("nombre_comercial");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
        });

        modelBuilder.Entity<Sesiones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sesiones_pkey");

            entity.ToTable("sesiones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CorreoAdmin)
                .HasMaxLength(150)
                .HasColumnName("correo_admin");
            entity.Property(e => e.FechaExpiracion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_expiracion");
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.Token).HasColumnName("token");

            entity.HasOne(d => d.CorreoAdminNavigation).WithMany(p => p.Sesiones)
                .HasPrincipalKey(p => p.CorreoAdmin)
                .HasForeignKey(d => d.CorreoAdmin)
                .HasConstraintName("sesiones_correo_admin_fkey");
        });
        
        modelBuilder.Ignore<SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities.Negocios>();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
