using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.FormModels;
using Portail_OptiVille.Data.Models;

namespace Portail_OptiVille.Data.Services
{
    public class ContactsService
    {
        private readonly A2024420517riGr1Eq6Context _context;
        private readonly ContactHosterFormModel _form;

        public ContactsService(A2024420517riGr1Eq6Context context, ContactHosterFormModel form)
        {
            _context = context;
            _form = form;
        }

        public async Task SaveContactsData()
        {
            var lastFournisseurId = await _context.Fournisseurs.MaxAsync(f => (int?)f.IdFournisseur);
            var contacts = new List<Contact>();

            foreach (var contactFromList in _form.ContactList)
            {
                var contact = new Contact
                {
                    Prenom = contactFromList.Prenom,
                    Nom = contactFromList.Nom,
                    Fonction = contactFromList.Fonction,
                    AdresseCourriel = contactFromList.AdresseCourriel,
                    Fournisseur = lastFournisseurId 
                };

                contacts.Add(contact);
            }

            try
            {
                await _context.Contacts.AddRangeAsync(contacts);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Une erreur est survenue lors de la sauvegarde des contacts.", ex);
            }
        }
    }
}