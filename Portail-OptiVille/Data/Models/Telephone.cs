﻿using System;
using System.Collections.Generic;

namespace Portail_OptiVille.Data.Models;

public partial class Telephone
{
    public int IdTelephone { get; set; }

    public string Type { get; set; } = null!;

    public string? Telephone1 { get; set; }

    public string? Poste { get; set; }

    public int? IdFournisseur { get; set; }

    public virtual Fournisseur? IdFournisseurNavigation { get; set; }
}
