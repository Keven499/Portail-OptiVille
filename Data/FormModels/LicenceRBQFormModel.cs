namespace Portail_OptiVille.Data.FormModels
{
    public class LicenceRBQFormModel
    {
        public string? NumeroLicence { get; set; }

        public string? StatutLicence { get; set; }

        public string? TypeLicence { get; set; }

        public Dictionary<string, bool> SousCategoSelected { get; set; } = new Dictionary<string, bool>();
    }
}
