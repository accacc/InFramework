//using IF.Core.Common;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using IF.HealthChecks.Checks;
//using System.Threading;
//using IF.Core.RabbitMQ;
//using Elasticsearch.Net;
//using Nest;
//using IF.HealthChecks.ElasticSearch;
//using System.Linq;
//using IF.Elasticsearch.Model;
//using IF.Elasticsearch.Repository;

//namespace IF.HealthChecks.ElasticSearch
//{

//    public static class HealthCheckBuilderElasticSearchCrudExtensions
//    {
//        public static HealthCheckBuilder AddElasticSearchCrudCheck(this HealthCheckBuilder builder, string name, string host)
//        {
//            Guard.ArgumentNotNull(nameof(builder), builder);

//            return AddElasticSearchCrudCheck(builder, name, host, builder.DefaultCacheDuration);
//        }

//        public static HealthCheckBuilder AddElasticSearchCrudCheck(this HealthCheckBuilder builder, string name, string host, TimeSpan cacheDuration)
//        {
//            builder.AddCheck($"ElasticSearchCrudCheck({name})", () =>
//            {
//                try
//                {
//                    ElasticRepository client = new ElasticRepository("healthchecktestlog");

//                    client.DeleteIndex();

//                    var now = DateTime.Now;
//                    HealthCheckTestLog doc = new HealthCheckTestLog();
//                    doc.TestPropertyDatetime = now;
//                    doc.TestPropertyDecimal = 12.5M;
//                    doc.TestPropertyInteger = 8;
//                    doc.TestPropertyString = "this is a string";

//                    client.Save(doc);

//                    Thread.Sleep(2000);

//                    var createDocument = client.Get<HealthCheckTestLog>(doc.Id);

//                    if (createDocument.Id != doc.Id
//                        || createDocument.TestPropertyDatetime != doc.TestPropertyDatetime
//                        || createDocument.TestPropertyDecimal != doc.TestPropertyDecimal
//                        || createDocument.TestPropertyInteger != doc.TestPropertyInteger
//                        || createDocument.TestPropertyString != doc.TestPropertyString)
//                    {
//                        throw new Exception("Create");
//                    }


//                    doc.TestPropertyInteger = 1;

//                    client.Update(doc);

//                    Thread.Sleep(2000);

//                    var updatedDocument = client.Get<HealthCheckTestLog>(doc.Id);


//                    if (updatedDocument.Id != doc.Id || updatedDocument.TestPropertyInteger != 1)
//                    {
//                        throw new Exception("Update");
//                    }


                    

           

//                    client.Delete<HealthCheckTestLog>(doc.Id);


//                    Thread.Sleep(2000);

//                    var deletedDocuments = client.SearchById<HealthCheckTestLog>(doc.Id);

//                    if (deletedDocuments.Count() > 0)
//                    {
//                        throw new Exception("Delete");
//                    }

//                    return HealthCheckResult.Healthy($"ElasticSearchCrudCheck({name}): Healthy");

//                }
//                catch (Exception ex)
//                {

//                    return HealthCheckResult.Unhealthy($"ElasticSearchCrudCheck({name}): Exception during check: {ex.Message}");
//                }
//            }, cacheDuration);

//            return builder;
//        }
//    }
//}
