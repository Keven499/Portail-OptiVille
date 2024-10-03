using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Licencerbq
{
    public string IdLicenceRbq { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public string? Type { get; set; }

    public string? TravauxPermis { get; set; }

    public int? IdFournisseur { get; set; }

    public virtual Fournisseur? IdFournisseurNavigation { get; set; }

    public virtual ICollection<Categorierbq> CodeSousCategories { get; set; } = new List<Categorierbq>();
}
