using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Research_Gate.Controllers
{
    public class EncryptPassword
    {

        public static string Encrypt(string password)
        {
            try
            {
                var encPass = System.Text.Encoding.UTF8.GetBytes(password);
                string encryptedPass = Convert.ToBase64String(encPass);

                return encryptedPass;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

    }
}