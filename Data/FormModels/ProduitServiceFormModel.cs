namespace Portail_OptiVille.Data.FormModels
{
    public class ProduitServiceFormModel
    {
        public string? Message { get; set; }
        public Dictionary<string, bool> SousProduitSelected { get; set; } = new Dictionary<string, bool>();

        public string? CategrorieUNSPSC { get; set; }
    }
}
