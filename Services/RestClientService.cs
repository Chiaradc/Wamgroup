using System.Collections.Generic;
using System.Text.Json;
using Elogic.Wamgroup.Sito2023.Caching.Interfaces;
using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;
using Progress.Sitefinity.RestSdk.Filters;

namespace Elogic.Wamgroup.Sito2023.NetCore.Services
{
   public class RestClientService
   {
      private readonly IRestClient restClient;
      private readonly ICacheManager cacheManager;

      public RestClientService(IRestClient restClient, ICacheManager cacheManager)
      {
         this.restClient = restClient;
         this.cacheManager = cacheManager;
      }


      public async Task Init(RequestArgs args)
      {
         await restClient.Init(args);
      }

      public async Task<T> GetItem<T>(string id, List<string>? fields = null, string? type = null) where T : class
      {
         var joinFields = fields != null ? string.Join(",", fields) : string.Empty;
         T value = await cacheManager.Cache(() =>
            {
               var args = new GetItemArgs
               {
                  Id = id,
               };
               if (fields != null)
               {
                  args.Fields = fields;
               }
               if (type != null)
               {
                  args.Type = type;
               }  
               return restClient.GetItem<T>(args).ConfigureAwait(true);

            }, $"RESTCLIENTSERVICE|GETITEM|{typeof(T).FullName}|{id.ToString()}|{joinFields}");

         

         return value;
      }

      public async Task<IList<T>> GetItems<T>(List<string>? fields = null, int take = 0, string? url = null, string?  parentId = null, int skip = 0) where T : class
      {
         var joinFields = fields != null ? string.Join(",", fields) : string.Empty;
         var data = await cacheManager.Cache(() =>
         {
            var args = new GetAllArgs();
            if (fields != null)
            {
               args.Fields = fields;
            }
            if (take > 0)
            {
               args.Take = take;
            }
            if (skip > 0)
            {
               args.Skip = skip;
            }

            if (!string.IsNullOrEmpty(url))
            {
               args.Filter = new FilterClause
               {
                  FieldName = "ItemDefaultUrl",
                  Operator = FilterClause.Operators.Equal,
                  FieldValue = url

               };
            }

            if (!string.IsNullOrEmpty(parentId))
             {
               args.Filter = new FilterClause
               {
                  FieldName = "ParentId",
                  Operator = FilterClause.Operators.Equal,
                  FieldValue = parentId
               };
            }

            return restClient.GetItems<T>(args).ConfigureAwait(true);

         }, $"RESTCLIENTSERVICE|GETITEMS|{typeof(T).FullName}|{joinFields}|{skip}|{take}|{url}|{parentId}");
         IList<T> value = data.Items;

         return value;
      }

      public async Task<IList<T>> GetItems<T>(GetAllArgs args) where T : class
      {
         var argsJson = JsonSerializer.Serialize(args);
         var data = await cacheManager.Cache(() =>
         {
            
         
            return restClient.GetItems<T>(args).ConfigureAwait(true);

         }, $"RESTCLIENTSERVICE|GETITEMS|{typeof(T).FullName}|{argsJson}");
         IList<T> value = data.Items;

         return value;
      }

      public async Task<CollectionResponse<T>> GetResponseItems<T>(GetAllArgs args) where T : class
      {
         var argsJson = JsonSerializer.Serialize(args);
         var data = await cacheManager.Cache(() =>
         {


            return restClient.GetItems<T>(args).ConfigureAwait(true);

         }, $"RESTCLIENTSERVICE|GETITEMS|{typeof(T).FullName}|{argsJson}");
         

         return data;
      }

      
   }
}
