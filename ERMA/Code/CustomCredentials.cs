using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Security;


namespace ERMA.Code
{
    public class CustomCredentials : ClientCredentials
    {
        public CustomCredentials() { }
        protected CustomCredentials(CustomCredentials cc) : base(cc) { }
        public override System.IdentityModel.Selectors.SecurityTokenManager CreateSecurityTokenManager() { return new CustomSecurityTokenManager(this); }
        protected override ClientCredentials CloneCore() { return new CustomCredentials(this); }
    }
    internal class CustomSecurityTokenManager : ClientCredentialsSecurityTokenManager
    {
        public CustomSecurityTokenManager(CustomCredentials cred) : base(cred) { }
        public override System.IdentityModel.Selectors.SecurityTokenSerializer CreateSecurityTokenSerializer(System.IdentityModel.Selectors.SecurityTokenVersion version)
        {


        
        return new CustomTokenSerializer(System.ServiceModel.Security.SecurityVersion.WSSecurity11);
        }
    }

    internal class CustomTokenSerializer : WSSecurityTokenSerializer
    {
        public CustomTokenSerializer(SecurityVersion sv) : base(sv)
        {
        }

        protected override void WriteTokenCore(System.Xml.XmlWriter writer, System.IdentityModel.Tokens.SecurityToken token)
        {
            System.IdentityModel.Tokens.UserNameSecurityToken unToken = (System.IdentityModel.Tokens.UserNameSecurityToken)token;
            writer.WriteRaw(@"<o:UsernameToken u:Id=""" + token.Id + @"""> <o:Username>" + unToken.UserName + @"</o:Username> <o:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"">" + unToken.Password + @"</o:Password> </o:UsernameToken> ");
        }
    }
}
