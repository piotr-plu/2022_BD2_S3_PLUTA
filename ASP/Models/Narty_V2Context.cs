using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Narciarze_v_2.Models
{
    public partial class Narty_V2Context : DbContext
    {

        public Narty_V2Context(DbContextOptions<Narty_V2Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Bilety> Bileties { get; set; } = null!;
        public virtual DbSet<CenaBilety> CenaBileties { get; set; } = null!;
        public virtual DbSet<CenaKarnety> CenaKarneties { get; set; } = null!;
        public virtual DbSet<Cennik> Cenniks { get; set; } = null!;
        public virtual DbSet<Harmonogram> Harmonograms { get; set; } = null!;
        public virtual DbSet<Karnety> Karneties { get; set; } = null!;
        public virtual DbSet<Klient> Klients { get; set; } = null!;
        public virtual DbSet<Stoki> Stokis { get; set; } = null!;
        public virtual DbSet<Wyciagi> Wyciagis { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bilety>(entity =>
            {
                entity.ToTable("Bilety");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdCennik).HasColumnName("ID_Cennik");

                entity.Property(e => e.IdKlient).HasColumnName("ID_Klient");

                entity.Property(e => e.IdWyciag).HasColumnName("ID_Wyciag");

                entity.Property(e => e.IloscZjazdow).HasColumnName("Ilosc_zjazdow");

                entity.HasOne(d => d.IdCennikNavigation)
                    .WithMany(p => p.Bileties)
                    .HasForeignKey(d => d.IdCennik)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bilety_to_Cennik");

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.Bileties)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bilety_to_Klient");

                entity.HasOne(d => d.IdWyciagNavigation)
                    .WithMany(p => p.Bileties)
                    .HasForeignKey(d => d.IdWyciag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bilety_to_Wyciag");
            });

            modelBuilder.Entity<CenaBilety>(entity =>
            {
                entity.ToTable("Cena_bilety");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CenaPrzejazd)
                    .HasColumnType("money")
                    .HasColumnName("Cena_przejazd");

                entity.Property(e => e.IdWyciag).HasColumnName("ID_Wyciag");

                entity.HasOne(d => d.IdWyciagNavigation)
                    .WithMany(p => p.CenaBileties)
                    .HasForeignKey(d => d.IdWyciag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cena_bilety_to_Wyciag");
            });

            modelBuilder.Entity<CenaKarnety>(entity =>
            {
                entity.ToTable("Cena_karnety");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cena).HasColumnType("money");

                entity.Property(e => e.IdStok).HasColumnName("ID_Stok");

                entity.HasOne(d => d.IdStokNavigation)
                    .WithMany(p => p.CenaKarneties)
                    .HasForeignKey(d => d.IdStok)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cena_karnety_to_Stok");
            });

            modelBuilder.Entity<Cennik>(entity =>
            {
                entity.ToTable("Cennik");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataRozp)
                    .HasColumnType("date")
                    .HasColumnName("Data_rozp");

                entity.Property(e => e.DataZak)
                    .HasColumnType("date")
                    .HasColumnName("Data_zak");

                entity.Property(e => e.IdCenaBilet).HasColumnName("ID_Cena_bilet");

                entity.Property(e => e.IdCenaKarnet).HasColumnName("ID_Cena_karnet");

                entity.HasOne(d => d.IdCenaBiletNavigation)
                    .WithMany(p => p.Cenniks)
                    .HasForeignKey(d => d.IdCenaBilet)
                    .HasConstraintName("FK_Cennik_to_Cena_Bilety");

                entity.HasOne(d => d.IdCenaKarnetNavigation)
                    .WithMany(p => p.Cenniks)
                    .HasForeignKey(d => d.IdCenaKarnet)
                    .HasConstraintName("FK_Cennik_to_Cena_Karnety");
            });

            modelBuilder.Entity<Harmonogram>(entity =>
            {
                entity.ToTable("Harmonogram");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataRozp)
                    .HasColumnType("date")
                    .HasColumnName("Data_rozp");

                entity.Property(e => e.DataZak)
                    .HasColumnType("date")
                    .HasColumnName("Data_zak");

                entity.Property(e => e.IdWyciagi).HasColumnName("ID_Wyciagi");

                entity.HasOne(d => d.IdWyciagiNavigation)
                    .WithMany(p => p.Harmonograms)
                    .HasForeignKey(d => d.IdWyciagi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Harmonogram_to_Wyciagi");
            });

            modelBuilder.Entity<Karnety>(entity =>
            {
                entity.ToTable("Karnety");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CzasTrwania).HasColumnName("Czas_trwania");

                entity.Property(e => e.DataAktywacji)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Data_aktywacji");

                entity.Property(e => e.IdCennika).HasColumnName("ID_Cennika");

                entity.Property(e => e.IdKlient).HasColumnName("ID_Klient");

                entity.Property(e => e.IdStok).HasColumnName("ID_Stok");

                entity.Property(e => e.Nazwa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCennikaNavigation)
                    .WithMany(p => p.Karneties)
                    .HasForeignKey(d => d.IdCennika)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Karnety_to_Cennik");

                entity.HasOne(d => d.IdKlientNavigation)
                    .WithMany(p => p.Karneties)
                    .HasForeignKey(d => d.IdKlient)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Karnety_to_Klient");
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.ToTable("Klient");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMail");

                entity.Property(e => e.Haslo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Imie)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Stoki>(entity =>
            {
                entity.ToTable("Stoki");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nazwa)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Wyciagi>(entity =>
            {
                entity.ToTable("Wyciagi");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.IdHarmonogram).HasColumnName("ID_Harmonogram");

                entity.Property(e => e.IdStok).HasColumnName("ID_Stok");

                entity.Property(e => e.Nazwa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WysBezwzgl).HasColumnName("Wys_bezwzgl");

                entity.HasOne(d => d.IdStokNavigation)
                    .WithMany(p => p.Wyciagis)
                    .HasForeignKey(d => d.IdStok)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wyciagi_to_Stoki");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
