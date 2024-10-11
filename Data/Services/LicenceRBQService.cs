using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class LicenceRBQService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public LicenceRBQService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveLicenceRBQData(LicenceRBQFormModel licenceRBQDto)
        {
            var licenceRBQ = new Licencerbq
            {
                Statut = licenceRBQDto.StatutLicence,
                Type = licenceRBQDto.TypeLicence,
                // FAUT RÉFLÉCHIR À COMMENT ON FAIT POUR AJOUTER DANS LES AUTRES TABLES AVEC CLEFS ÉTRANGÈRES ETC
            };

            _context.Licencerbqs.Add(licenceRBQ);
            await _context.SaveChangesAsync();
        }
    }
}
