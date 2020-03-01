//using Elasticsearch.Net;
//using IF.Core.Elasticsearch;
//using IF.Elasticsearch.Model;
//using IF.Elasticsearch.Repository;
//using Microsoft.Extensions.Options;
//using Nest;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace IF.Elasticsearch
//{
//    public class ElasticSearchLogProvider<T> : IElasticSearchLogProvider<T> where T : class,IElasticSearchLog
//    {
//        private readonly string defaultAlias;
//        private string defaultIndex;
//        private static Field TimestampField = new Field("timestamp");
//        private readonly ElasticSearchLogSettings _options;
//        private readonly IElasticSearchConnectionStrategy elasticSearchConnectionStrategy;

//        private ElasticClient _elasticClient { get; }

//        public ElasticSearchLogProvider(ElasticSearchLogSettings options, IElasticSearchConnectionStrategy elasticSearchConnectionStrategy)
//        {
//            _options = options ?? throw new ArgumentNullException(nameof(options));

//            this.elasticSearchConnectionStrategy = elasticSearchConnectionStrategy ?? throw new ArgumentNullException(nameof(elasticSearchConnectionStrategy));

//            if(String.IsNullOrWhiteSpace(options.Host))
//            {
//                throw new ArgumentNullException(nameof(options.Host));
//            }

//            defaultAlias = options.Alias;

//            defaultIndex = $"{defaultAlias}-{DateTime.UtcNow.ToString("yyyy-MM-dd")}";

//            if (_options.IndexPerMonth)
//            {
//                defaultIndex = $"{defaultAlias}-{DateTime.UtcNow.ToString("yyyy-MM")}";
//            }

//            _elasticClient = this.elasticSearchConnectionStrategy.GetConnection(options.Host, defaultIndex);

//        }

//        public void AddLog(T log)
//        {
//            CheckIndex();

//            log.UniqueId = Guid.NewGuid();

//            var indexRequest = new IndexRequest<T>(log);

//            var response = _elasticClient.Index(indexRequest);

//            if (!response.IsValid)
//            {
//                throw new ElasticsearchClientException("Add auditlog disaster!");
//            }
//        }

//        public async Task AddLogAsync(T log)
//        {
//            CheckIndex();

//            log.UniqueId = Guid.NewGuid();

//            var indexRequest = new IndexRequest<T>(log);

//            var response = await _elasticClient.IndexAsync(indexRequest);

//            if (!response.IsValid)
//            {
//                throw new ElasticsearchClientException("Add auditlog disaster!");
//            }
//        }

//        public void CheckIndex()
//        {
//            var response = this._elasticClient.IndexExists(this.defaultIndex);

//            if (!response.Exists)
//            {
//                this._elasticClient.CreateIndex(this.defaultIndex, index =>
//                   index.Mappings(ms =>
//                       ms.Map<T>(x => x.AutoMap())));
//            }
//        }

//        public void DeleteIndex()
//        {
//            if (_elasticClient.IndexExists(defaultIndex).Exists)
//            {
//                var result = _elasticClient.DeleteIndex(defaultIndex);

//                if (!result.IsValid)
//                {
//                    throw new Exception(result.OriginalException.Message);
//                }
//            }
//        }

//        public long Count(string filter = "*")
//        {
//            CheckIndex();

//            EnsureAlias();

//            var searchRequest = new SearchRequest<T>(Indices.Parse(defaultAlias))
//            {
//                Size = 0,
//                Query = new QueryContainer(
//                    new SimpleQueryStringQuery
//                    {
//                        Query = filter
//                    }
//                ),
//                Sort = new List<ISort>
//                    {
//                        new SortField { Field = TimestampField, Order = SortOrder.Descending }
//                    }
//            };

//            var searchResponse = _elasticClient.Search<T>(searchRequest);

//            return searchResponse.Total;
//        }

//        public IEnumerable<T> QueryLogs(string filter = "*", ElasticsearchLogPaging paging = null) 
//        {
//            var from = 0;
//            var size = 10;

//            CheckIndex();

//            EnsureAlias();

//            if (paging != null)
//            {
//                from = paging.Skip;
//                size = paging.Size;
//                if (size > 1000)
//                {
//                    // max limit 1000 items
//                    size = 1000;
//                }
//            }

//            var searchRequest = new SearchRequest<T>(Indices.Parse(defaultAlias))
//            {
//                Size = size,
//                From = from,
//                Query = new QueryContainer(
//                    new SimpleQueryStringQuery
//                    {
//                        Query = filter
//                    }
//                ),
//                Sort = new List<ISort>
//                    {
//                        new SortField { Field = TimestampField, Order = SortOrder.Descending }
//                    }
//            };

//            var searchResponse = _elasticClient.Search<T>(searchRequest);

//            return searchResponse.Documents;
//        }

//        private void CreateAliasForAllIndices()
//        {
//            var response = _elasticClient.AliasExists(new AliasExistsRequest(new Names(new List<string> { defaultAlias })));
//            if (!response.IsValid)
//            {
//                throw response.OriginalException;
//            }

//            if (response.Exists)
//            {
//                _elasticClient.DeleteAlias(new DeleteAliasRequest(Indices.Parse($"{defaultAlias}-*"), defaultAlias));
//            }

//            var responseCreateIndex = _elasticClient.PutAlias(new PutAliasRequest(Indices.Parse($"{defaultAlias}-*"), defaultAlias));
//            if (!responseCreateIndex.IsValid)
//            {
//                throw responseCreateIndex.OriginalException;
//            }
//        }

//        private void CreateAlias()
//        {
//            if (_options.AmountOfPreviousIndicesUsedInAlias > 0)
//            {
//                CreateAliasForLastNIndices(_options.AmountOfPreviousIndicesUsedInAlias);
//            }
//            else
//            {
//                CreateAliasForAllIndices();
//            }
//        }

//        private void CreateAliasForLastNIndices(int amount)
//        {
//            var responseCatIndices = _elasticClient.CatIndices(new CatIndicesRequest(Indices.Parse($"{defaultAlias}-*")));
//            var records = responseCatIndices.Records.ToList();
//            List<string> indicesToAddToAlias = new List<string>();
//            for(int i = amount;i>0;i--)
//            {
//                if (_options.IndexPerMonth)
//                {
//                    var indexName = $"{defaultAlias}-{DateTime.UtcNow.AddMonths(-i + 1).ToString("yyyy-MM")}";
//                    if(records.Exists(t => t.Index == indexName))
//                    {
//                        indicesToAddToAlias.Add(indexName);
//                    }
//                }
//                else
//                {
//                    var indexName = $"{defaultAlias}-{DateTime.UtcNow.AddDays(-i + 1).ToString("yyyy-MM-dd")}";                   
//                    if (records.Exists(t => t.Index == indexName))
//                    {
//                        indicesToAddToAlias.Add(indexName);
//                    }
//                }
//            }

//            var response = _elasticClient.AliasExists(new AliasExistsRequest(new Names(new List<string> { defaultAlias })));
//            if (!response.IsValid)
//            {
//                throw response.OriginalException;
//            }

//            if (response.Exists)
//            {
//                _elasticClient.DeleteAlias(new DeleteAliasRequest(Indices.Parse($"{defaultAlias}-*"), defaultAlias));
//            }

//            Indices multipleIndicesFromStringArray = indicesToAddToAlias.ToArray();
//            var responseCreateIndex = _elasticClient.PutAlias(new PutAliasRequest(multipleIndicesFromStringArray, defaultAlias));
//            if (!responseCreateIndex.IsValid)
//            {
//                throw responseCreateIndex.OriginalException;
//            }
//        }

//        private static DateTime aliasUpdated = DateTime.UtcNow.AddYears(-50);

//        private void EnsureAlias()
//        {
//            if (_options.IndexPerMonth)
//            {
//                if (aliasUpdated.Date < DateTime.UtcNow.AddMonths(-1).Date)
//                {
//                    aliasUpdated = DateTime.UtcNow;
//                    CreateAlias();
//                }
//            }
//            else
//            {
//                if (aliasUpdated.Date < DateTime.UtcNow.AddDays(-1).Date)
//                {
//                    aliasUpdated = DateTime.UtcNow;
//                    CreateAlias();
//                }
//            }           
//        }
//    }
//}
