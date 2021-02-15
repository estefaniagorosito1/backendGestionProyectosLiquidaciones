using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendGestionProyectosLiquidaciones.Model
{
    public partial class TpSeminarioContext : DbContext
    {
        public TpSeminarioContext()
        {
        }

        public TpSeminarioContext(DbContextOptions<TpSeminarioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EmpleadoProyecto> EmpleadoProyecto { get; set; }
        public virtual DbSet<EscalaAntiguedad> EscalaAntiguedad { get; set; }
        public virtual DbSet<EscalaHoras> EscalaHoras { get; set; }
        public virtual DbSet<EscalaPerfiles> EscalaPerfiles { get; set; }
        public virtual DbSet<HoraTrabajada> HoraTrabajada { get; set; }
        public virtual DbSet<Liquidacion> Liquidacion { get; set; }
        public virtual DbSet<Localidad> Localidad { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<PerfilEmpleado> PerfilEmpleado { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Tarea> Tarea { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=TpSeminario;Trusted_Connection=True;");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Idcliente);

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.ApellidoCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DireccionCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCliente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TelefonoCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocalidadClienteNavigation)
                    .WithMany(p => p.Cliente)
                    .HasForeignKey(d => d.LocalidadCliente)
                    .HasConstraintName("FK_Cliente_Localidad");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.Idempleado);

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.Property(e => e.ApellidoEmpleado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaIngresoEmpleado).HasColumnType("datetime");

                entity.Property(e => e.NombreEmpleado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocalidadNavigation)
                    .WithMany(p => p.Empleado)
                    .HasForeignKey(d => d.Localidad)
                    .HasConstraintName("FK_Empleado_Localidad");
            });

            modelBuilder.Entity<EmpleadoProyecto>(entity =>
            {
                entity.HasKey(e => new { e.Idproyecto, e.Idempleado });

                entity.Property(e => e.Idproyecto).HasColumnName("IDProyecto");

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.HasOne(d => d.IdempleadoNavigation)
                    .WithMany(p => p.EmpleadoProyecto)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpleadoProyecto_Empleado");

                entity.HasOne(d => d.IdproyectoNavigation)
                    .WithMany(p => p.EmpleadoProyecto)
                    .HasForeignKey(d => d.Idproyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpleadoProyecto_Proyecto");
            });

            modelBuilder.Entity<EscalaAntiguedad>(entity =>
            {
                entity.HasKey(e => e.IdescalaAntiguedad);

                entity.Property(e => e.IdescalaAntiguedad).HasColumnName("IDEscalaAntiguedad");
            });

            modelBuilder.Entity<EscalaHoras>(entity =>
            {
                entity.HasKey(e => e.IdescalaHoras);

                entity.Property(e => e.IdescalaHoras).HasColumnName("IDEscalaHoras");
            });

            modelBuilder.Entity<EscalaPerfiles>(entity =>
            {
                entity.HasKey(e => e.IdescalaPerfil);

                entity.Property(e => e.IdescalaPerfil).HasColumnName("IDEscalaPerfil");
            });

            modelBuilder.Entity<HoraTrabajada>(entity =>
            {
                entity.HasKey(e => new { e.IdhoraTrabajada, e.Idproyecto, e.Idtarea });

                entity.Property(e => e.IdhoraTrabajada)
                    .HasColumnName("IDHoraTrabajada")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Idproyecto).HasColumnName("IDProyecto");

                entity.Property(e => e.Idtarea).HasColumnName("IDTarea");

                entity.Property(e => e.EstadoHoraTrabajada)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaHoraTrabajada).HasColumnType("datetime");

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.HoraTrabajada)
                    .HasForeignKey(d => new { d.Idempleado, d.Idperfil })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HoraTrabajada_PerfilEmpleado");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.HoraTrabajada)
                    .HasForeignKey(d => new { d.Idtarea, d.Idproyecto })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HT_ProyectoTarea");
            });

            modelBuilder.Entity<Liquidacion>(entity =>
            {
                entity.HasKey(e => e.CodLiquidacion);

                entity.Property(e => e.Estado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaLiquidacion).HasColumnType("datetime");

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.Property(e => e.IdescalaAntiguedad).HasColumnName("IDEscalaAntiguedad");

                entity.Property(e => e.IdescalaHoras).HasColumnName("IDEscalaHoras");

                entity.Property(e => e.IdescalaPerfil).HasColumnName("IDEscalaPerfil");

                entity.HasOne(d => d.IdempleadoNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmpleadoLiquidacion");

                entity.HasOne(d => d.IdescalaAntiguedadNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdescalaAntiguedad)
                    .HasConstraintName("FK_LiquidacionEscalaAnt");

                entity.HasOne(d => d.IdescalaHorasNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdescalaHoras)
                    .HasConstraintName("FK_LiquidacionEscalaHoras");

                entity.HasOne(d => d.IdescalaPerfilNavigation)
                    .WithMany(p => p.Liquidacion)
                    .HasForeignKey(d => d.IdescalaPerfil)
                    .HasConstraintName("FK_LiquidacionEscalaPerfil");
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.Idlocalidad);

                entity.Property(e => e.Idlocalidad)
                    .HasColumnName("IDLocalidad")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Idprovincia).HasColumnName("IDProvincia");

                entity.HasOne(d => d.IdprovinciaNavigation)
                    .WithMany(p => p.Localidad)
                    .HasForeignKey(d => d.Idprovincia)
                    .HasConstraintName("FK_ProvinciaLocalidad");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.Idperfil);

                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");

                entity.Property(e => e.NombrePerfil)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PerfilEmpleado>(entity =>
            {
                entity.HasKey(e => new { e.Idempleado, e.Idperfil });

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");

                entity.HasOne(d => d.IdempleadoNavigation)
                    .WithMany(p => p.PerfilEmpleado)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PerfilEmpleado_Empleado");

                entity.HasOne(d => d.IdperfilNavigation)
                    .WithMany(p => p.PerfilEmpleado)
                    .HasForeignKey(d => d.Idperfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PerfilEmpleado_IdPerfil");
            });

            modelBuilder.Entity<Provincia>(entity =>
            {
                entity.HasKey(e => e.Idprovincia);

                entity.Property(e => e.Idprovincia).HasColumnName("IDProvincia");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.Idproyecto);

                entity.Property(e => e.Idproyecto).HasColumnName("IDProyecto");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoProyecto)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FechaFinProyecto).HasColumnType("datetime");

                entity.Property(e => e.FechaInicioProyecto).HasColumnType("datetime");

                entity.Property(e => e.Idcliente).HasColumnName("IDCliente");

                entity.Property(e => e.NombreProyecto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdclienteNavigation)
                    .WithMany(p => p.Proyecto)
                    .HasForeignKey(d => d.Idcliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proyecto_Cliente");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.Idrol);

                entity.Property(e => e.Idrol).HasColumnName("IDRol");

                entity.Property(e => e.DescripcionRol)
                    .IsRequired()
                    .HasColumnName("descripcionRol")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tarea>(entity =>
            {
                entity.HasKey(e => new { e.Idtarea, e.Idproyecto });

                entity.Property(e => e.Idtarea)
                    .HasColumnName("IDTarea")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Idproyecto).HasColumnName("IDProyecto");

                entity.Property(e => e.DescripcionTarea)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.finalizada)
                      .HasMaxLength(50)
                      .IsUnicode(false);

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.Property(e => e.Idperfil).HasColumnName("IDPerfil");

                entity.HasOne(d => d.IdproyectoNavigation)
                    .WithMany(p => p.Tarea)
                    .HasForeignKey(d => d.Idproyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proyecto");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Tarea)
                    .HasForeignKey(d => new { d.Idempleado, d.Idperfil })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tarea_EmpleadoPerfil");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Idusuario);

                entity.Property(e => e.Idusuario).HasColumnName("IDUsuario");

                entity.Property(e => e.Idempleado).HasColumnName("IDEmpleado");

                entity.Property(e => e.Idrol).HasColumnName("IDRol");

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUsuario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdempleadoNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Idempleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdEmpleado");

                entity.HasOne(d => d.IdrolNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.Idrol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_IdRol");
            });
        }
    }
}
