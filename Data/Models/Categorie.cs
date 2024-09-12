using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Categorie
{
    public string CodeSousCategorie { get; set; } = null!;

    public string? Nom { get; set; }

    public string? Categorie1 { get; set; }

    public virtual ICollection<Licencerbq> IdLicenceRbqs { get; set; } = new List<Licencerbq>();
}
