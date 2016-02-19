using BandageLanding.Infrastructure.Models;
using Supermortal.Common.PCL.Abstract;

namespace BandageLanding.Infrastructure.Abstract
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        ApplicationUser FindByUserName(string userName);
    }
}
