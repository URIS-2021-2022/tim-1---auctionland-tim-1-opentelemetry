using PublicBiddingMicroservice.Models;

namespace PublicBiddingMicroservice.Helpers
{
    public interface IAuthenticationHelper
    {
        public bool AuthenticatePrincipal(Principal principal);

        public string GenerateJwt(Principal principal);
    }
}