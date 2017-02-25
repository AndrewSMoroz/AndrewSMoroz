using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.Services
{

    public class UserContext : IUserContext
    {

        private readonly IHttpContextAccessor _contextAccessor;

        //--------------------------------------------------------------------------------------------------------------
        public UserContext (IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        //--------------------------------------------------------------------------------------------------------------
        public string UserName
        {
            get
            {

                string userName = "Unknown";

                if (_contextAccessor.HttpContext != null)
                {
                    if (_contextAccessor.HttpContext.User != null)
                    {
                        var identity = _contextAccessor.HttpContext.User.Identity;
                        if (identity != null && identity.IsAuthenticated)
                        {
                            userName = identity.Name;
                        }
                    }
                }

                return userName;

            }

        }

    }

}
