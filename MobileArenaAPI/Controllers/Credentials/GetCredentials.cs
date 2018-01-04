using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using MobileArenaAPI.Data;

namespace MobileArenaAPI.Controllers.Credentials
{
    public static class GetCredentials
    {
        [FunctionName("GetCredentials")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequestMessage req, [Table("credentials", Connection = "")]CloudTable inTable, TraceWriter log)
        {
            dynamic data = req.Content.ReadAsAsync<object>();
            var email = data?.email;
            var password = data?.password;

            TableOperation updateOperation = TableOperation.Retrieve<CredentialsModel>(null, null, null);
            TableResult result = inTable.Execute(updateOperation);

            return req.CreateResponse(HttpStatusCode.OK, result.Result);
        }

        public static bool VerifyPassword(string inputPassword, string storedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(storedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(inputPassword, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
