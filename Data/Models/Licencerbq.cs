using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Licencerbq
{
    public string IdLicenceRbq { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public string? Type { get; set; }

    public string? TravauxPermis { get; set; }

    public virtual ICollection<Categorie> CodeSousCategories { get; set; } = new List<Categorie>();

    public virtual ICollection<Fournisseur> IdFournisseurs { get; set; } = new List<Fournisseur>();
}
