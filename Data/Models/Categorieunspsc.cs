using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Categorieunspsc
{
    public int CategoUnsid { get; set; }

    public string? CodeCategorie { get; set; }

    public string? Categorie { get; set; }

    public virtual ICollection<Produitservice> Produitservices { get; set; } = new List<Produitservice>();
}
