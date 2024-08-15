using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace WebApplication
{
    public class CustomPrincipal : IPrincipal
    {
        private readonly CustomIdentity CustomIdentity;
        public CustomPrincipal(CustomIdentity _customIdentity)
        {
            CustomIdentity = _customIdentity;
        }
        public IIdentity Identity
        {
            get { return CustomIdentity; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}