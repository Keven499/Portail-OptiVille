using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Categorierbq
{
    public string CodeSousCategorie { get; set; } = null!;

    public string? Nom { get; set; }

    public string? NomCategorie { get; set; }

    public virtual ICollection<Licencerbq> IdLicenceRbqs { get; set; } = new List<Licencerbq>();
}
