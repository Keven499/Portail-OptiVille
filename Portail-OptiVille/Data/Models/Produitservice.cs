using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Produitservice
{
    public string CodeUnspsc { get; set; } = null!;

    public string Nature { get; set; } = null!;

    public string? CodeCategorie { get; set; }

    public string? Categorie { get; set; }

    public string? Description { get; set; }

    public string? Details { get; set; }

    public virtual ICollection<Fournisseur> IdFournisseurs { get; set; } = new List<Fournisseur>();
}
