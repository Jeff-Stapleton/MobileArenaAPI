using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MobileArenaAPI.Data;

namespace MobileArenaAPI
{
    public static class GetUsers
    {
        [FunctionName("GetUsers")]
        public static HttpResponseMessage Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")]HttpRequestMessage req, [Table("users", Connection = "")]IQueryable<UserModel> inTable, TraceWriter log)
        {
            var query = from person in inTable select person;
            foreach (UserModel person in query)
            {
                log.Info($"Name:{person.Name}");
            }
            return req.CreateResponse(HttpStatusCode.OK, inTable.ToList());
        }
    }
}
