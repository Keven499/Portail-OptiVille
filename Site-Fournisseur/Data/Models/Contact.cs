using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Contact
{
    public int IdContact { get; set; }

    public string? Prenom { get; set; }

    public string? Nom { get; set; }

    public string? Fonction { get; set; }

    public string? AdresseCourriel { get; set; }

    public string TypeTelephone { get; set; } = null!;

    public string? Telephone { get; set; }

    public string? Poste { get; set; }

    public int? IdFournisseur { get; set; }

    public virtual Fournisseur? IdFournisseurNavigation { get; set; }
}
