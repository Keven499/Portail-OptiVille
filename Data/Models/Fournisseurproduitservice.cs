using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Fournisseurproduitservice
{
    public int IdFournisseur { get; set; }

    public string IdProduitService { get; set; } = null!;

    public string? DetailSpecification { get; set; }

    public virtual Fournisseur IdFournisseurNavigation { get; set; } = null!;

    public virtual Produitservice IdProduitServiceNavigation { get; set; } = null!;
}
