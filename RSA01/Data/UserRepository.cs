using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSA01.Data
{
    public class UserRepository
    {
        public bool ValidateUser(string username, string password)
        {
            using (EntitiesRecibo db = new EntitiesRecibo())
            {
                var result = from u in db.SAG01_USUARIO where (u.USUARIO.ToUpper() == username.ToUpper()) select u;

                if (result.Count() != 0)
                {
                    var dbuser = result.First();

                    if (SEG01_DO.Encriptor.validarPassword(password, dbuser.PASSWORD))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
        }
    }
}