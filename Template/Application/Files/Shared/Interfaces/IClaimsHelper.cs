using System.Security.Claims;
namespace CleanArchitecture.Shared.Interfaces
{
    public interface IClaimsHelper
    {
        /// <summary>
        /// Contain Claim Type And Value
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        bool ContainClaimTypeAndValue(string claimType, string claimValue, IEnumerable<Claim> claims);
        /// <summary>
        /// Get Base Url 
        /// </summary>
        /// <returns></returns>
        string GetBaseUrl();
        /// <summary>
        /// Get Claim 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        string GetClaimValue(ClaimsPrincipal principal, string claimType);
        /// <summary>
        /// Get Claim 
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        Claim? GetClaim(ClaimsPrincipal principal, string claimType);
        /// <summary>
        /// Get Claim 
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GetClaim(string claimType, IEnumerable<Claim> claims);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="allClaims"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        List<Claim> GetClaims(IEnumerable<Claim> allClaims, string claimType);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        List<Claim> GetClaims(ClaimsPrincipal principal, string claimType);
        /// <summary>
        /// Get Claim 
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        Claim? GetClaim(IEnumerable<Claim> claims, string claimType, string claimValue);
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="principal"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        Claim? GetClaim(ClaimsPrincipal principal, string claimType, string claimValue);
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        List<string> GetClaimsByType(string claimType, IEnumerable<Claim> claims);
        /// <summary>
        /// Get Email 
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        string GetEmail(IEnumerable<Claim> claims);
        /// <summary>
        /// GetClientId
        /// </summary>
        /// <returns></returns>
        string GetClientId();
        /// <summary>
        /// GetUserId
        /// </summary>
        /// <returns></returns>
        string? GetUserId();
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        Task<List<string>> GetClaimsByTypeAsync(string claimType, IEnumerable<Claim> claims);
        /// <summary>
        /// Is Admin
        /// </summary>
        bool IsAdmin { get; }
        /// <summary>
        /// Is Internet Explorer
        /// </summary>
        /// <returns></returns>
        bool IsInternetExplorer();
    }
}
