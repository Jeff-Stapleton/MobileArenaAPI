using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Table;
using MobileArenaAPI.Data;

namespace MobileArenaAPI
{
    public static class PutUser
    {
        [FunctionName("PutUser")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "put")]UserModel user, [Table("users", Connection = "")]CloudTable outTable, TraceWriter log)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("A non-empty Name must be specified.")
                };
            };

            log.Info($"PersonName={user.Name}");

            TableOperation updateOperation = TableOperation.InsertOrReplace(user);
            TableResult result = outTable.Execute(updateOperation);
            return new HttpResponseMessage((HttpStatusCode)result.HttpStatusCode);
        }
    }
}
