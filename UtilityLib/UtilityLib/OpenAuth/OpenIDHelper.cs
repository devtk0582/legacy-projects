using DotNetOpenAuth.OpenId.RelyingParty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UtilityLib.OpenAuth
{
    public class OpenIDHelper
    {
        public string GetOpenIDResponse()
        {
            var result = string.Empty;
            var openid = new OpenIdRelyingParty();
            var response = openid.GetResponse();
            if (response != null)
            {
                switch (response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        result = StringToGUID(response.ClaimedIdentifier.ToString()).ToString();
                        break;
                    case AuthenticationStatus.Canceled:
                        result = "Canceled at Provider";
                        break;
                    case AuthenticationStatus.Failed:
                        result = response.Exception.Message;
                        break;
                }

            }

            return result;
        }

        public void RedirectToProvider(string providerUri)
        {
            using (OpenIdRelyingParty openid = new OpenIdRelyingParty())
            {
                var request = openid.CreateRequest(providerUri);
                request.RedirectToProvider();
            }
        }

        private Guid StringToGUID(string value) 
        { 
            var md5Hasher = MD5.Create(); 
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value)); 
            return new Guid(data); 
        }
    }
}
