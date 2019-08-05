using IF.Core.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IF.Persistence
{
    public interface IDbContext
    {
        //int ExecuteInsertQuery<T>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new();

        object ExecuteScalar<T>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new();

        //object ExecuteFunction<T>(Action<Builder<T>> builderAction, T parameters, string functionName) where T : class, new();


        DataTable GetDatatableBySql<T>(Action<ParameterBuilder<T>> builderAction, T parameters, string sql) where T : class, new();

        int ExecuteNonQuery<T>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new();

        int ExecuteNonQuery(ParameterBuilder builder, string storedProcedure);

        DataTable GetDatatable<T>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new();

        DataTable GetDatatable(ParameterBuilder builder, string storedProcedure);

        DataSet GetDataset(ParameterBuilder parameterBuilder, string storedProcedure);

        DataSet GetDataset<T>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new();

        List<K> GetList<T, K>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new() where K : class, new();

        List<T> GetList<T>(ParameterBuilder builder, string storedProcedure) where T : class, new();

        K Get<T,K>(Action<ParameterBuilder<T>> builderAction, T parameters, string storedProcedure) where T : class, new() where K : class, new();


        T Get<T>(ParameterBuilder builder, string storedProcedure) where T : class, new();
        

        void Transaction(Action action, TransactionScopeOption option = TransactionScopeOption.Required); //where T : class

    }
}
