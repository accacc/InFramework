namespace IF.Elasticsearch.Model
{
    public class ElasticSearchLogSettings
    {
        public bool IndexPerMonth { get; set; }

        public int AmountOfPreviousIndicesUsedInAlias { get; set; }

        public string Alias { get; set; }

        public string Host { get; set; }

        public void UseSettings(bool indexPerMonth, int amountOfPreviousIndicesUsedInAlias,string alias,string Host)
        {
            IndexPerMonth = indexPerMonth;
            AmountOfPreviousIndicesUsedInAlias = amountOfPreviousIndicesUsedInAlias;
            this.Alias = alias;
            this.Host = Host;
        }
    }
}
