using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class GestionUserService
    {
        private readonly A2024420517riGr1Eq6Context _context;

        public GestionUserService(A2024420517riGr1Eq6Context context)
        {
            _context = context;
        }

        public async Task SaveUser()
        {
            // EN FAIT JE CROIS QU'IL VA NOUS FALLOIR UNE SECONDE TABLE QUI DÉTERMINE JUSTE LE RÔLE DES EMPLOYÉS POUR NE PAS SUPPRIMER LES EMPLOYÉS DE LA TABLE, MAIS SEULEMENT LEUR RÔLE
        }

        public async Task DeleteUser(Employe employe)
        {
            var userToDelete = await _context.Employes.FindAsync(employe.Courriel);
            if (userToDelete != null)
            {
                _context.Employes.Remove(userToDelete);
                await _context.SaveChangesAsync(); 
            }
            else
            {
                throw new Exception("User not found");
            }
        }

        public async Task UpdateUser()
        {
            // EN FAIT JE CROIS QU'IL VA NOUS FALLOIR UNE SECONDE TABLE QUI DÉTERMINE JUSTE LE RÔLE DES EMPLOYÉS POUR NE PAS SUPPRIMER LES EMPLOYÉS DE LA TABLE, MAIS SEULEMENT LEUR RÔLE
        }
    }
}
