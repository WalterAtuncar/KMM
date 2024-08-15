using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using WebApplication.Models;

namespace WebApplication
{
    public class CustomIdentity : IIdentity
    {
        public IIdentity Identity { get; set; }
        public UserModel Usuario { get; set; }

        public CustomIdentity(UserModel usuario)
        {
            Identity = new GenericIdentity(usuario.Email);
            Usuario = usuario;
        }

        public string AuthenticationType
        {
            get { return Identity.AuthenticationType; }
        }

        public bool IsAuthenticated
        {
            get { return Identity.IsAuthenticated; }
        }

        public string Name
        {
            get { return Identity.Name; }
        }
    }
}