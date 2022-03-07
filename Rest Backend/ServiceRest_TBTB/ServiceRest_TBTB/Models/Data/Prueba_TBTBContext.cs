using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace ServiceRest_TBTB.Models.Data
{
    public partial class Prueba_TBTBContext : DbContext
    {
        private IConfiguration _configuration;
        private readonly string _connectionString = "";

        public Prueba_TBTBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration["BaseDeDatos"];
        }

        public Prueba_TBTBContext(DbContextOptions<Prueba_TBTBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Medico> Medicos { get; set; }
        public virtual DbSet<MedicoEspecialidad> MedicoEspecialidads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Medico>(entity =>
            {
                entity.ToTable("Medico", "Transaccion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Creacion)
                    .HasColumnType("datetime")
                    .HasColumnName("creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.NumeroDocumento)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("numeroDocumento");
            });

            modelBuilder.Entity<MedicoEspecialidad>(entity =>
            {
                entity.ToTable("MedicoEspecialidad", "Transaccion");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Actualizacion)
                    .HasColumnType("datetime")
                    .HasColumnName("actualizacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Creacion)
                    .HasColumnType("datetime")
                    .HasColumnName("creacion")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdEspecialidad).HasColumnName("idEspecialidad");

                entity.Property(e => e.IdMedico).HasColumnName("idMedico");

                entity.HasOne(d => d.IdMedicoNavigation)
                    .WithMany(p => p.MedicoEspecialidads)
                    .HasForeignKey(d => d.IdMedico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MedicoEspecialidad_Medico");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
