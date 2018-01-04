using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MobileArenaAPI.Data;

namespace MobileArenaAPI.Controllers.Credentials
{
    public static class PostCredential
    {
        [FunctionName("PostCredential")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req, [Table("credentials", Connection = "")]ICollector<CredentialsModel> outTable, TraceWriter log)
        {
            dynamic data = await req.Content.ReadAsAsync<object>();
            string email = data?.email;
            string password = data?.password;


            if (email == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name in the request body");
            }

            outTable.Add(new CredentialsModel()
            {
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = email,
                Email = email,
                Password = HashPassword(password)

            });
            return req.CreateResponse(HttpStatusCode.Created);
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            RandomNumberGenerator.Create().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashbytes = new byte[36];
            Array.Copy(salt, 0, hashbytes, 0, 16);
            Array.Copy(hash, 0, hashbytes, 16, 20);
            return Convert.ToBase64String(hashbytes);
        }
    }
}
