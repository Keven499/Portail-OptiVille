using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Portail_OptiVille.Data.Models;

public partial class A2024420517riGr1Eq6Context : DbContext
{
    public A2024420517riGr1Eq6Context()
    {
    }

    public A2024420517riGr1Eq6Context(DbContextOptions<A2024420517riGr1Eq6Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorie> Categories { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Fichier> Fichiers { get; set; }

    public virtual DbSet<Fournisseur> Fournisseurs { get; set; }

    public virtual DbSet<Historique> Historiques { get; set; }

    public virtual DbSet<Licencerbq> Licencerbqs { get; set; }

    public virtual DbSet<Produitservice> Produitservices { get; set; }

    public virtual DbSet<Telephone> Telephones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=cours.cegep3r.info;database=a2024_420517ri_gr1-eq6;user=2263519;password=2263519", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.18-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Categorie>(entity =>
        {
            entity.HasKey(e => e.CodeSousCategorie).HasName("PRIMARY");

            entity.ToTable("categorie");

            entity.Property(e => e.CodeSousCategorie)
                .HasMaxLength(10)
                .HasColumnName("codeSousCategorie");
            entity.Property(e => e.Nom)
                .HasMaxLength(150)
                .HasColumnName("nom");
            entity.Property(e => e.NomCategorie)
                .HasMaxLength(50)
                .HasColumnName("nomCategorie");

            entity.HasMany(d => d.IdLicenceRbqs).WithMany(p => p.CodeSousCategories)
                .UsingEntity<Dictionary<string, object>>(
                    "Licencerbqcategorie",
                    r => r.HasOne<Licencerbq>().WithMany()
                        .HasForeignKey("IdLicenceRbq")
                        .HasConstraintName("licencerbqcategorie_ibfk_2"),
                    l => l.HasOne<Categorie>().WithMany()
                        .HasForeignKey("CodeSousCategorie")
                        .HasConstraintName("licencerbqcategorie_ibfk_1"),
                    j =>
                    {
                        j.HasKey("CodeSousCategorie", "IdLicenceRbq")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("licencerbqcategorie");
                        j.HasIndex(new[] { "IdLicenceRbq" }, "idLicenceRBQ");
                        j.IndexerProperty<string>("CodeSousCategorie")
                            .HasMaxLength(10)
                            .HasColumnName("codeSousCategorie");
                        j.IndexerProperty<string>("IdLicenceRbq")
                            .HasMaxLength(12)
                            .HasColumnName("idLicenceRBQ");
                    });
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.IdContact).HasName("PRIMARY");

            entity.ToTable("contact");

            entity.HasIndex(e => e.IdFournisseur, "idFournisseur");

            entity.Property(e => e.IdContact)
                .HasColumnType("int(11)")
                .HasColumnName("idContact");
            entity.Property(e => e.AdresseCourriel)
                .HasMaxLength(64)
                .HasColumnName("adresseCourriel");
            entity.Property(e => e.Fonction)
                .HasMaxLength(32)
                .HasColumnName("fonction");
            entity.Property(e => e.IdFournisseur)
                .HasColumnType("int(11)")
                .HasColumnName("idFournisseur");
            entity.Property(e => e.Nom)
                .HasMaxLength(32)
                .HasColumnName("nom");
            entity.Property(e => e.Poste)
                .HasMaxLength(6)
                .HasColumnName("poste");
            entity.Property(e => e.Prenom)
                .HasMaxLength(32)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasMaxLength(10)
                .HasColumnName("telephone");
            entity.Property(e => e.TypeTelephone)
                .HasColumnType("enum('Bureau','Télécopieur','Cellulaire')")
                .HasColumnName("typeTelephone");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("contact_ibfk_1");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Courriel).HasName("PRIMARY");

            entity.ToTable("employe");

            entity.Property(e => e.Courriel)
                .HasMaxLength(64)
                .HasColumnName("courriel");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(256)
                .HasColumnName("motDePasse");
            entity.Property(e => e.Role)
                .HasColumnType("enum('Administrateur','Commis','Responsable')")
                .HasColumnName("role");
        });

        modelBuilder.Entity<Fichier>(entity =>
        {
            entity.HasKey(e => e.IdFichier).HasName("PRIMARY");

            entity.ToTable("fichier");

            entity.HasIndex(e => e.IdFournisseur, "idFournisseur");

            entity.Property(e => e.IdFichier)
                .HasColumnType("int(11)")
                .HasColumnName("idFichier");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.IdFournisseur)
                .HasColumnType("int(11)")
                .HasColumnName("idFournisseur");
            entity.Property(e => e.Nom)
                .HasMaxLength(32)
                .HasColumnName("nom");
            entity.Property(e => e.Taille)
                .HasColumnType("int(11)")
                .HasColumnName("taille");
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .HasColumnName("type");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Fichiers)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fichier_ibfk_1");
        });

        modelBuilder.Entity<Fournisseur>(entity =>
        {
            entity.HasKey(e => e.IdFournisseur).HasName("PRIMARY");

            entity.ToTable("fournisseur");

            entity.HasIndex(e => e.Neq, "neq").IsUnique();

            entity.Property(e => e.IdFournisseur)
                .HasColumnType("int(11)")
                .HasColumnName("idFournisseur");
            entity.Property(e => e.AdresseCourriel)
                .HasMaxLength(64)
                .HasColumnName("adresseCourriel");
            entity.Property(e => e.Bureau)
                .HasMaxLength(8)
                .HasColumnName("bureau");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(6)
                .HasColumnName("codePostal");
            entity.Property(e => e.CodeRegionAdministrative)
                .HasMaxLength(2)
                .HasColumnName("codeRegionAdministrative");
            entity.Property(e => e.ConditionPaiement)
                .HasMaxLength(100)
                .HasColumnName("conditionPaiement");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateCreation");
            entity.Property(e => e.DateLastChanged)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("dateLastChanged");
            entity.Property(e => e.Devise)
                .HasColumnType("enum('CAD','USD')")
                .HasColumnName("devise");
            entity.Property(e => e.ModeCommunication)
                .HasColumnType("enum('Courriel','Courrier régulier')")
                .HasColumnName("modeCommunication");
            entity.Property(e => e.MotDePasse)
                .HasMaxLength(256)
                .HasColumnName("motDePasse");
            entity.Property(e => e.Neq)
                .HasMaxLength(10)
                .HasColumnName("neq");
            entity.Property(e => e.NoCivique)
                .HasMaxLength(8)
                .HasColumnName("noCivique");
            entity.Property(e => e.NomEntreprise)
                .HasMaxLength(64)
                .HasColumnName("nomEntreprise");
            entity.Property(e => e.NumeroTps)
                .HasMaxLength(15)
                .HasColumnName("numeroTps");
            entity.Property(e => e.NumeroTvq)
                .HasMaxLength(15)
                .HasColumnName("numeroTvq");
            entity.Property(e => e.Province)
                .HasDefaultValueSql("'Québec'")
                .HasColumnType("enum('Ontario','Québec','Nouvelle-Écosse','Nouveau-Brunswick','Manitoba','Colombie-Britannique','Île-du-Prince-Édouard','Saskatchewan','Alberta','Terre-Neuve-et-Labrador','Territoires du Nord-Ouest','Yukon','Nunavut')")
                .HasColumnName("province");
            entity.Property(e => e.RegionAdministrative)
                .HasMaxLength(50)
                .HasColumnName("regionAdministrative");
            entity.Property(e => e.Rue)
                .HasMaxLength(64)
                .HasColumnName("rue");
            entity.Property(e => e.SiteInternet)
                .HasMaxLength(64)
                .HasColumnName("siteInternet");
            entity.Property(e => e.Ville)
                .HasMaxLength(64)
                .HasColumnName("ville");

            entity.HasMany(d => d.CodeUnspscs).WithMany(p => p.IdFournisseurs)
                .UsingEntity<Dictionary<string, object>>(
                    "Fournisseurproduitservice",
                    r => r.HasOne<Produitservice>().WithMany()
                        .HasForeignKey("CodeUnspsc")
                        .HasConstraintName("fournisseurproduitservice_ibfk_2"),
                    l => l.HasOne<Fournisseur>().WithMany()
                        .HasForeignKey("IdFournisseur")
                        .HasConstraintName("fournisseurproduitservice_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdFournisseur", "CodeUnspsc")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("fournisseurproduitservice");
                        j.HasIndex(new[] { "CodeUnspsc" }, "codeUNSPSC");
                        j.IndexerProperty<int>("IdFournisseur")
                            .HasColumnType("int(11)")
                            .HasColumnName("idFournisseur");
                        j.IndexerProperty<string>("CodeUnspsc")
                            .HasMaxLength(8)
                            .HasColumnName("codeUNSPSC");
                    });

            entity.HasMany(d => d.IdLicenceRbqs).WithMany(p => p.IdFournisseurs)
                .UsingEntity<Dictionary<string, object>>(
                    "Fournisseurlicencerbq",
                    r => r.HasOne<Licencerbq>().WithMany()
                        .HasForeignKey("IdLicenceRbq")
                        .HasConstraintName("fournisseurlicencerbq_ibfk_2"),
                    l => l.HasOne<Fournisseur>().WithMany()
                        .HasForeignKey("IdFournisseur")
                        .HasConstraintName("fournisseurlicencerbq_ibfk_1"),
                    j =>
                    {
                        j.HasKey("IdFournisseur", "IdLicenceRbq")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("fournisseurlicencerbq");
                        j.HasIndex(new[] { "IdLicenceRbq" }, "idLicenceRBQ");
                        j.IndexerProperty<int>("IdFournisseur")
                            .HasColumnType("int(11)")
                            .HasColumnName("idFournisseur");
                        j.IndexerProperty<string>("IdLicenceRbq")
                            .HasMaxLength(12)
                            .HasColumnName("idLicenceRBQ");
                    });
        });

        modelBuilder.Entity<Historique>(entity =>
        {
            entity.HasKey(e => e.IdHistorique).HasName("PRIMARY");

            entity.ToTable("historique");

            entity.HasIndex(e => e.IdFournisseur, "idFournisseur");

            entity.Property(e => e.IdHistorique)
                .HasColumnType("int(11)")
                .HasColumnName("idHistorique");
            entity.Property(e => e.DateEtatChanged)
                .HasColumnType("datetime")
                .HasColumnName("dateEtatChanged");
            entity.Property(e => e.EtatDemande)
                .HasColumnType("enum('Accepté','Refusé','En attente','À reviser')")
                .HasColumnName("etatDemande");
            entity.Property(e => e.IdFournisseur)
                .HasColumnType("int(11)")
                .HasColumnName("idFournisseur");
            entity.Property(e => e.RaisonRefus)
                .HasMaxLength(500)
                .HasColumnName("raisonRefus");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Historiques)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("historique_ibfk_1");
        });

        modelBuilder.Entity<Licencerbq>(entity =>
        {
            entity.HasKey(e => e.IdLicenceRbq).HasName("PRIMARY");

            entity.ToTable("licencerbq");

            entity.Property(e => e.IdLicenceRbq)
                .HasMaxLength(10)
                .HasColumnName("idLicenceRBQ");
            entity.Property(e => e.Statut)
                .HasColumnType("enum('Valide','Valide avec restriction','Non valide')")
                .HasColumnName("statut");
            entity.Property(e => e.TravauxPermis)
                .HasMaxLength(255)
                .HasColumnName("travauxPermis");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Produitservice>(entity =>
        {
            entity.HasKey(e => e.CodeUnspsc).HasName("PRIMARY");

            entity.ToTable("produitservice");

            entity.Property(e => e.CodeUnspsc)
                .HasMaxLength(8)
                .HasColumnName("codeUNSPSC");
            entity.Property(e => e.Categorie)
                .HasMaxLength(50)
                .HasColumnName("categorie");
            entity.Property(e => e.CodeCategorie)
                .HasMaxLength(10)
                .HasColumnName("codeCategorie");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Details)
                .HasMaxLength(500)
                .HasColumnName("details");
            entity.Property(e => e.Nature)
                .HasColumnType("enum('Approvisionnement','Services','Travaux de construction','Autres natures de contrat')")
                .HasColumnName("nature");
        });

        modelBuilder.Entity<Telephone>(entity =>
        {
            entity.HasKey(e => e.IdTelephone).HasName("PRIMARY");

            entity.ToTable("telephone");

            entity.HasIndex(e => e.IdFournisseur, "idFournisseur");

            entity.Property(e => e.IdTelephone)
                .HasColumnType("int(11)")
                .HasColumnName("idTelephone");
            entity.Property(e => e.IdFournisseur)
                .HasColumnType("int(11)")
                .HasColumnName("idFournisseur");
            entity.Property(e => e.NumTelephone)
                .HasMaxLength(10)
                .HasColumnName("numTelephone");
            entity.Property(e => e.Poste)
                .HasMaxLength(6)
                .HasColumnName("poste");
            entity.Property(e => e.Type)
                .HasColumnType("enum('Bureau','Télécopieur','Cellulaire')")
                .HasColumnName("type");

            entity.HasOne(d => d.IdFournisseurNavigation).WithMany(p => p.Telephones)
                .HasForeignKey(d => d.IdFournisseur)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("telephone_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
