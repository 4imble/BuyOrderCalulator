using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;

namespace BuyOrderCalc.Web.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("{code}")]
        public TokenResponse Get(string code)
        {
            var clientId = "760234712446402560";
            var secret = "MBuAhtzBgI4fIos0wqatQreSm68j9TPy";
            var redirect = "http://localhost:5000/login";
            var endpoint = $"grant_type=authorization_code&client_id={clientId}&client_secret={secret}&code={code}&redirect_uri={redirect}";
            var client = new RestClient("https://discord.com/api/oauth2/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", endpoint, ParameterType.RequestBody);
            var response = client.Execute<TokenResponse>(request);
            return response.Data;
        }

        [Authorize]
        [HttpGet()]
        [Route("GetUser")]
        public void GetUser()
        {
            var currentUser = HttpContext.User;
        }


        public class TokenResponse
        {
            // {"access_token": "x", "expires_in": 604800, "refresh_token": "xx", "scope": "identify", "token_type": "Bearer"}

            public string access_token { get; set; }
            public string expires_in { get; set; }
            public string refresh_token { get; set; }
            public string scope { get; set; }
            public string token_type { get; set; }
        }
    }
}
