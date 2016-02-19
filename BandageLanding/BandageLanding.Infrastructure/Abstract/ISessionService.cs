using BandageLanding.Infrastructure.Models;

namespace BandageLanding.Infrastructure.Abstract
{
  public interface ISessionService
  {
    Session CreateSession(string userName);
    ApplicationUser GetUser(string token);
    bool? ValidateToken(string token);
  }
}
