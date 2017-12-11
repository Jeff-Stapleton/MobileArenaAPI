using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MobileArenaAPI.Data;

namespace MobileArenaAPI
{
    public static class PostUser
    {
        [FunctionName("PostUser")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")]HttpRequestMessage req, [Table("users", Connection = "")]ICollector<UserModel> userTable, [Table("collections", Connection = "")]ICollector<CollectionModel> collectionTable, TraceWriter log)
        {
            dynamic data = await req.Content.ReadAsAsync<object>();

            if (data?.email == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name in the request body");
            }

            var newCollection = new CollectionModel();
            collectionTable.Add(newCollection);
            userTable.Add(new UserModel()
            {
                PartitionKey = "Functions",
                RowKey = Guid.NewGuid().ToString(),
                Name = data?.name,
                Email = data?.email,
                Deck = new DeckModel
                {
                    Collection = newCollection.Id
                        
                }
            });
            return req.CreateResponse(HttpStatusCode.Created);
        }
    }
}
