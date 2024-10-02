namespace Portail_OptiVille.Data.FormModels
{
    /*
        CECI EST UN TEST. IL A ÉTÉ CONCLUANT ET JE VAIS CONTINUER TOUT À L'HEURE, SI VOUS VOYEZ CE MESSAGE, N'EFFACEZ PAS CETTE PAGE S'IL VOUS PLAÎT
    */
    public class LicenceRBQFormModel
    {
        public string NumeroLicence { get; set; }

        public string StatutLicence { get; set; }

        public string TypeLicence { get; set; }

        public Dictionary<string, bool> SousCategoSelected { get; set; } = new Dictionary<string, bool>();
    }
}
