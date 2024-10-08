using Microsoft.EntityFrameworkCore;
using Portail_OptiVille.Data.Models;

public interface IUserService
{
    Task<Employe> GetUserByUsernameAsync(string username);
}

public class UserService : IUserService
{
    private readonly A2024420517riGr1Eq6Context _dbContext;

    public UserService(A2024420517riGr1Eq6Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Employe> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Employes.SingleOrDefaultAsync(u => u.Courriel == username);
    }
}
