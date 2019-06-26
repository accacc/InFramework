using System;
using System.Collections.Generic;
using System.Text;


namespace IF.Core.Database
{
    public class SqlServerConnectionStringParser : IConnectionStringParser
    {
        public ConnectionStringModel Parser(string connectionString)
        {
            ConnectionStringModel model = new ConnectionStringModel();

            System.Data.SqlClient.SqlConnectionStringBuilder objSB1 = new System.Data.SqlClient.SqlConnectionStringBuilder(connectionString);

            StringBuilder sb = new StringBuilder();

            //sb.AppendLine("<b>Parsed SQL Connection String Parameters:</b>");
            model.DataSource = objSB1.DataSource;
            model.InitialCatalog = objSB1.InitialCatalog;
            model.IntegratedSecurity = objSB1.IntegratedSecurity;
            model.MinPoolSize = objSB1.MinPoolSize;
            model.MaxPoolSize = objSB1.MaxPoolSize;
            model.LoadBalanceTimeout = objSB1.LoadBalanceTimeout;

            return model;
        }
    }
}
