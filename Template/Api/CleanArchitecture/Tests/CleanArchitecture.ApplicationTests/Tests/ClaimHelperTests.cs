using System.Security.Claims;
using CleanArchitecture.Application.Helpers;
using CleanArchitecture.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
namespace CleanArchitecture.ApplicationTests.Tests
{
    [TestClass]
    public class ClaimHelperTests
    {
        static ILogger<ClaimHelperTests> _logger;
        static Guid userId;
        static Guid clientId;
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _logger = (ILogger<ClaimHelperTests>)
                   KernelMapper.ServiceProvider.GetService(typeof(ILogger<ClaimHelperTests>));
            _logger.LogInformation("{ctx}", context.TestRunDirectory);
            userId = Guid.NewGuid();
            clientId = Guid.NewGuid();
        }
        [DataTestMethod]
        public void GetEmail_Test()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            var claims = GetClaimsPrincipal().Claims;
            Assert.IsTrue(applicationHelper.GetEmail(claims).Equals("myemail@gpo.gov"));
        }
        [DataTestMethod]
        public void GetClientId_Test()
        {
            ClaimHelper applicationHelper = new
                (GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            var ClientId = Guid.Parse(applicationHelper.GetClientId());
            Assert.IsTrue(ClientId.Equals(clientId));
        }
        [DataTestMethod]
        public void Is_IE_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            bool success = applicationHelper.IsInternetExplorer();
            Assert.IsTrue(success);
        }
        [DataTestMethod]
        public void Is_Admin_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            bool success = applicationHelper.IsAdmin;
            Assert.IsTrue(success);
        }
        [DataTestMethod]
        public void ContainClaimTypeAndValue_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            bool success = applicationHelper.ContainClaimTypeAndValue("emailaddress", "myemail@gpo.gov", GetClaimsPrincipal().Claims);
            Assert.IsTrue(success);
        }
        [DataTestMethod]
        public void GetBaseUrl_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            string url = applicationHelper.GetBaseUrl();
            Assert.IsNotNull(url);
        }
        [DataTestMethod]
        public void GetClaimValue_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            string email = applicationHelper.GetClaimValue(GetClaimsPrincipal(), "emailaddress");
            Assert.IsTrue(email.Equals("myemail@gpo.gov", StringComparison.CurrentCultureIgnoreCase));
            string claim = applicationHelper.GetClaim("emailaddress", GetClaimsPrincipal().Claims);
            Assert.IsTrue(claim.Equals("myemail@gpo.gov", StringComparison.CurrentCultureIgnoreCase));
        }
        [DataTestMethod]
        public void GetClaim_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            Claim claim = applicationHelper.GetClaim(GetClaimsPrincipal(), "emailaddress");
            Assert.IsTrue(claim != null);
        }
        [DataTestMethod]
        public void GetClaims_TEST()
        {
            ClaimHelper applicationHelper = new(GetIHttpContextAccessor());
            Assert.IsInstanceOfType<IClaimsHelper>(applicationHelper);
            List<Claim> claim = applicationHelper.GetClaims(GetClaimsPrincipal(), ClaimTypes.Role.ToString());
            Assert.IsTrue(claim != null);
            List<Claim> claims = applicationHelper.GetClaims(GetClaimsPrincipal().Claims, ClaimTypes.Role.ToString());
            Assert.IsTrue(claims != null);
            List<string> claimByTypes = applicationHelper.GetClaimsByType(ClaimTypes.Role.ToString(), GetClaimsPrincipal().Claims);
            Assert.IsTrue(claimByTypes != null);
        }
        static IHttpContextAccessor GetIHttpContextAccessor()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var headers = new Mock<IHeaderDictionary>();
            headers.Setup(h => h.UserAgent).Returns("MSIE");
            mockHttpContextAccessor.Setup(req => req.HttpContext.Request.Scheme).Returns("https");
            mockHttpContextAccessor.Setup(req => req.HttpContext.Request.Host).Returns(new HostString("//localhost:8080"));
            mockHttpContextAccessor.Setup(req => req.HttpContext.Request.Path).Returns("/index.html");
            mockHttpContextAccessor.Setup(req => req.HttpContext.Request.QueryString).Returns(new QueryString("?funfactor=100"));
            mockHttpContextAccessor.Setup(req => req.HttpContext.Request.Headers).Returns(headers.Object);
            mockHttpContextAccessor.Setup(o => o.HttpContext.User).Returns(GetClaimsPrincipal());
            return mockHttpContextAccessor.Object;
        }
        static ClaimsPrincipal GetClaimsPrincipal()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new(ClaimTypes.Name, "example name"),
                new(ClaimTypes.Email, "myemail@gpo.gov"),
                new("emailaddress","myemail@gpo.gov"),
                new(ClaimTypes.NameIdentifier, "Muatassim"),
                new("client_id",clientId.ToString()),
                new("Id", userId.ToString()),
                new("sub", userId.ToString()),
                new("subject", userId.ToString()),
                new(ClaimTypes.Role, "Admin"),
                new(ClaimTypes.Role, "PowerUser"),
                new("preferred_username","Muatassim Aziz"),
                new("custom-claim", "example claim value"),
            ], "mock"));
            return user;
        }
    }
}
