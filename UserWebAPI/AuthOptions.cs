using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserWebAPI
{
    public class AuthOptions
    {
        public const string ISSUER = "AuthServer";
        public const string AUDIENCE = "AuthClient"; 
        const string KEY = "sdnhjfioIgjkvJvd123d"; 
        
        public const int LIFETIME = 1; // in munuts

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
