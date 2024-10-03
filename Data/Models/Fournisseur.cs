using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Fournisseur
{
    public int IdFournisseur { get; set; }

    public string? Neq { get; set; }

    public string? AdresseCourriel { get; set; }

    public string? NomEntreprise { get; set; }

    public string? MotDePasse { get; set; }

    public string? NoCivique { get; set; }

    public string? Rue { get; set; }

    public string? Bureau { get; set; }

    public string? Ville { get; set; }

    public string? Province { get; set; }

    public string? CodePostal { get; set; }

    public string? CodeRegionAdministrative { get; set; }

    public string? RegionAdministrative { get; set; }

    public string? SiteInternet { get; set; }

    public string? NumeroTps { get; set; }

    public string? NumeroTvq { get; set; }

    public string? ConditionPaiement { get; set; }

    public string Devise { get; set; } = null!;

    public string? Details { get; set; }

    public string ModeCommunication { get; set; } = null!;

    public DateTime? DateCreation { get; set; }

    public DateTime? DateLastChanged { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Fichier> Fichiers { get; set; } = new List<Fichier>();

    public virtual ICollection<Historique> Historiques { get; set; } = new List<Historique>();

    public virtual ICollection<Licencerbq> Licencerbqs { get; set; } = new List<Licencerbq>();

    public virtual ICollection<Telephone> Telephones { get; set; } = new List<Telephone>();

    public virtual ICollection<Produitservice> CodeUnspscs { get; set; } = new List<Produitservice>();
}
