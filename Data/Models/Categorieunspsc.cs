using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Categorieunspsc
{
    public string CodeCategorie { get; set; } = null!;

    public string? Categorie { get; set; }

    public virtual ICollection<Produitservice> Produitservices { get; set; } = new List<Produitservice>();
}
