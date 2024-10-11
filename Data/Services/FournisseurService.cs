using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class FournisseurService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public FournisseurService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveFournisseurData(IdenticationFormModel identificationDto, LicenceRBQFormModel licenceRBQDto, FournisseurFormModel fournisseurDto)
        {
            var fournisseur = new Fournisseur
            {
                Neq = identificationDto.NEQ,
                NomEntreprise = identificationDto.NomEntreprise,
                AdresseCourriel = identificationDto.CourrielEntreprise,
                MotDePasse = identificationDto.MotDePasse,
                NoCivique = fournisseurDto.NoEntreprise,
                Rue = fournisseurDto.RueEntreprise,
                Bureau = fournisseurDto.BureauEntreprise,
                Ville = fournisseurDto.VilleEntreprise,
                Province = fournisseurDto.ProvinceEntreprise,
                CodePostal = fournisseurDto.CodePostalEntreprise,
                CodeRegionAdministrative = fournisseurDto.RegionAdmEntreprise?.Substring(fournisseurDto.RegionAdmEntreprise.IndexOf('(')
                                                                             + 1, fournisseurDto.RegionAdmEntreprise.IndexOf(')')
                                                                             - fournisseurDto.RegionAdmEntreprise.IndexOf('(') - 1),
                RegionAdministrative = fournisseurDto.RegionAdmEntreprise?.Substring(fournisseurDto.RegionAdmEntreprise.IndexOf(' ') + 1),
                SiteInternet = fournisseurDto.SiteWebEntreprise,
                DateCreation = DateTime.Now,
                // FAUT CHANGER DETAILS DE PLACE, IL DOIT ÊTRE DANS PRODUITSERVICE
            };

            _context.Fournisseurs.Add(fournisseur);
            await _context.SaveChangesAsync();
        }
    }
}
