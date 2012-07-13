using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace Consumer.Controllers {

    public class SigninController : Controller {

        public ActionResult Index() {

            var openId = new OpenIdRelyingParty();
            IAuthenticationResponse response = openId.GetResponse();

            // if this is a roundtrip and we got something back from the provider...
            if (response != null) {
                switch (response.Status) {
                    case AuthenticationStatus.Authenticated: {
                            FormsAuthentication.RedirectFromLoginPage(response.ClaimedIdentifier, false);
                            break;
                        }
                    case AuthenticationStatus.Canceled: {
                            ModelState.AddModelError("loginIdentifier", "Login was cancelled at the provider");
                            break;
                        }
                    case AuthenticationStatus.Failed: {
                            ModelState.AddModelError("loginIdentifier", "Login failed at the provider");
                            break;
                        }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(string loginIdentifier) {

            if (!Identifier.IsValid(loginIdentifier)) {
                ModelState.AddModelError("loginIdentifier", "The specified loginIdentifier is invalid");
                return View();
            }

            var openId = new OpenIdRelyingParty();
            IAuthenticationRequest request = openId.CreateRequest(Identifier.Parse(loginIdentifier));

            // request some additional claims
            request.AddExtension(new ClaimsRequest {
                BirthDate = DemandLevel.Require,
                Email = DemandLevel.Require,
                FullName = DemandLevel.Require,
            });

            return request.RedirectingResponse.AsActionResult();
        }
    }
}
