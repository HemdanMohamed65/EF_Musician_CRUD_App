using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EF_Musician.Models;

public partial class MusicianContext : DbContext
{
    public MusicianContext()
    {
    }

    public MusicianContext(DbContextOptions<MusicianContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<Musician> Musicians { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=HEMDAN65;Initial Catalog=Musician;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.Property(e => e.Album_ID).ValueGeneratedNever();

            entity.HasOne(d => d.Producer).WithMany(p => p.Albums).HasConstraintName("FK_Albums_Musicians");
        });

        modelBuilder.Entity<Musician>(entity =>
        {
            entity.Property(e => e.Musician_ID).ValueGeneratedNever();

            entity.HasMany(d => d.Instrument_Names).WithMany(p => p.Musicians)
                .UsingEntity<Dictionary<string, object>>(
                    "Play",
                    r => r.HasOne<Instrument>().WithMany()
                        .HasForeignKey("Instrument_Name")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Plays_Instruments"),
                    l => l.HasOne<Musician>().WithMany()
                        .HasForeignKey("Musician_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Plays_Musicians"),
                    j =>
                    {
                        j.HasKey("Musician_ID", "Instrument_Name");
                        j.ToTable("Plays");
                        j.IndexerProperty<string>("Instrument_Name")
                            .HasMaxLength(50)
                            .IsUnicode(false);
                    });

            entity.HasMany(d => d.Songs).WithMany(p => p.Musicians)
                .UsingEntity<Dictionary<string, object>>(
                    "Perform",
                    r => r.HasOne<Song>().WithMany()
                        .HasForeignKey("Song_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Performs_Songs"),
                    l => l.HasOne<Musician>().WithMany()
                        .HasForeignKey("Musician_ID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Performs_Musicians"),
                    j =>
                    {
                        j.HasKey("Musician_ID", "Song_ID");
                        j.ToTable("Performs");
                    });
        });

        modelBuilder.Entity<Song>(entity =>
        {
            entity.Property(e => e.Song_ID).ValueGeneratedNever();

            entity.HasOne(d => d.Album).WithMany(p => p.Songs).HasConstraintName("FK_Songs_Albums");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
