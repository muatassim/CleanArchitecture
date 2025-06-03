using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleanArchitecture.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
namespace CleanArchitecture.Application.Helpers
{
    public class ClaimHelper(IHttpContextAccessor httpContextAccessor) : IClaimsHelper
    {
        IHttpContextAccessor HttpContextAccessor { get; } = httpContextAccessor;
        private readonly HttpContext _currentContext = httpContextAccessor.HttpContext!;
        private readonly ClaimsPrincipal _user = httpContextAccessor.HttpContext!.User;
        public UriBuilder? UriBuilder
        {
            get
            {
                if (_currentContext != null)
                {
                    return new UriBuilder
                    {
                        Scheme = _currentContext.Request.Scheme,
                        Host = _currentContext.Request.Host.ToString(),
                        Path = _currentContext.Request.Path.ToString(),
                        Query = _currentContext.Request.QueryString.ToString()
                    };
                }
                return null;
            }
        }
        /// <summary>
        /// Get Base 
        /// </summary>
        /// <returns></returns>
        public string GetBaseUrl()
        {
            if (UriBuilder != null)
                return $"{UriBuilder.Scheme}://{UriBuilder.Host.Replace("[", "").Replace("]", "")}";
            return string.Empty;
        }
        /// <summary>
        /// Check the Existence of Claim Type and Right Values
        /// eg: 
        /// 1. "Group","1",
        /// 2. "EmailAddress","maziz@gpo.gov"
        /// </summary>
        /// <param nam="claimType"></param>
        /// <param name="claimType"></param>
        /// <param name="claimValue"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public bool ContainClaimTypeAndValue(string claimType, string claimValue, IEnumerable<Claim> claims)
        {
            var exist = false;
            foreach (var claim in claims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (string.Compare(claim.Value, claimValue, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        exist = true;
                        break;
                    }
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase)
                    && string.Compare(claim.Value, claimValue, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
        public string GetClaimValue(ClaimsPrincipal principal, string claimType)
        {
            var claimVal = string.Empty;
            foreach (var claim in principal.Claims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    claimVal = claim.Value;
                    break;
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase))
                {
                    claimVal = claim.Value;
                    break;
                }
            }
            return claimVal;
        }
        /// <summary>
        /// Get Claim
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GetClaim(string claimType, IEnumerable<Claim> claims)
        {
            var claimVal = string.Empty;
            foreach (var claim in claims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    claimVal = claim.Value;
                    break;
                }
            }
            return claimVal;
        }
        public Claim? GetClaim(ClaimsPrincipal principal, string claimType)
        {
            Claim? rClaim = null;
            foreach (var claim in principal.Claims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    rClaim = claim;
                    break;
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase))
                {
                    rClaim = claim;
                    break;
                }
            }
            return rClaim;
        }
        public Claim? GetClaim(IEnumerable<Claim> claims, string claimType, string claimValue)
        {
            Claim? rClaim = null;
            foreach (var claim in claims)
            {
                var isClaimMatch = false;
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    isClaimMatch = true;
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase))
                {
                    isClaimMatch = true;
                }
                if (isClaimMatch && claim.Value.Equals(claimValue, StringComparison.CurrentCultureIgnoreCase))
                {
                    rClaim = claim;
                    break;
                }
            }
            return rClaim;
        }
        public Claim? GetClaim(ClaimsPrincipal principal, string claimType, string claimValue)
        {
            var claims = principal.Claims;
            return GetClaim(claims, claimType, claimValue);
        }
        public List<Claim> GetClaims(IEnumerable<Claim> allClaims, string claimType)
        {
            List<Claim> claims = [];
            foreach (var claim in allClaims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    claims.Add(claim);
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase))
                {
                    claims.Add(claim);
                }
            }
            return claims;
        }
        public List<Claim> GetClaims(ClaimsPrincipal principal, string claimType)
        {
            List<Claim> claims = [];
            foreach (var claim in principal.Claims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    claims.Add(claim);
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase))
                {
                    claims.Add(claim);
                }
            }
            return claims;
        }
        public bool IsInternetExplorer()
        {
            string? __userAgent = _currentContext.Request?.Headers.UserAgent;
            if (!string.IsNullOrEmpty(__userAgent) && (__userAgent.Contains("MSIE") || __userAgent.Contains("Trident")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Get Claims
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public List<string> GetClaimsByType(string claimType, IEnumerable<Claim> claims)
        {
            List<string> claimVal = [];
            foreach (var claim in claims)
            {
                if (string.Compare(claim.Type, claimType, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    claimVal.Add(claim.Value);
                }
                else if (claim.Type.EndsWith(claimType, StringComparison.CurrentCultureIgnoreCase))
                {
                    claimVal.Add(claim.Value);
                }
            }
            return claimVal;
        }
        /// <summary>
        /// Get Email 
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string GetEmail(IEnumerable<Claim> claims)
        {
            var emailClaim = claims.FirstOrDefault(x => x.Type.Contains("emailaddress"));
            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            emailClaim = claims.FirstOrDefault(x => x.Type.Contains("email"));
            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            emailClaim = claims.FirstOrDefault(x => x.Type.Contains("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"));
            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            emailClaim = claims.FirstOrDefault(x => x.Type.Contains("preferred__username"));
            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            return string.Empty;
        }
        public string GetClientId()
        {
            if (HttpContextAccessor?.HttpContext != null)
            {
                return GetClaim("client_id", HttpContextAccessor.HttpContext.User.Claims);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="claimType"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public async Task<List<string>> GetClaimsByTypeAsync(string claimType, IEnumerable<Claim> claims)
        {
            return await Task.Factory.StartNew(() => GetClaimsByType(claimType, claims));
        }
        public bool IsAdmin => _user.IsInRole("Admin");
        public string? GetUserId()
        {
            if (_user == null)
            {
                return null;
            }
            var claim = GetClaim("Id", _user.Claims);
            if (!string.IsNullOrEmpty(claim))
            {
                return claim;
            }
            claim = GetClaim("sub", _user.Claims);
            if (!string.IsNullOrEmpty(claim))
            {
                return claim;
            }
            claim = GetClaim("subject", _user.Claims);
            return claim;
        }
    }
}
