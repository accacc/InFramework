using Derin.Core.Convert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Derin.Core.Data
{
    public class ParameterFactory
    {
        Parameter parameter;

        public ParameterFactory(string Name)
        {
            parameter = new Parameter(Name);
        }

        public ParameterFactory Direction(ParamDirection direction)
        {
            this.parameter.Direction = direction;
            return this;
        }
        public ParameterFactory Type(Type type)
        {
            this.parameter.Type = type;
            return this;
        }

        public ParameterFactory Size(int size)
        {
            this.parameter.Size = size;
            return this;
        }

        
        public ParameterFactory Value(object value)
        {
            this.parameter.Value = value;
            return this;
        }

        public ParameterFactory ValueToStringArray(IEnumerable<string> value,string seperator=",")
        {
            if (value != null && value.Any())
            {
                this.parameter.Value = String.Join(seperator, value.Select(a => String.Format("'{0}'", a)));
            }
            else
            {
                this.parameter.Value = String.Empty;
            }

            return this;
        }


        public Parameter GetParameter()
        {
            return this.parameter;
        }

    }

    public class ParameterBuilder
    {
        public List<Parameter> Parameters { get; private set; }

        public ParameterBuilder()
        {
            this.Parameters = new List<Parameter>();
        }
        public ParameterFactory Add(string name)
        {

            ParameterFactory parameterBuilder = new ParameterFactory(name);
            parameterBuilder.Direction(ParamDirection.Input);
            this.Parameters.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

        public  ParameterFactory Add<T>(string name)
        {

            ParameterFactory parameterBuilder = new ParameterFactory(name);
            parameterBuilder.Direction(ParamDirection.Input);
            parameterBuilder.Type(typeof(T));
            this.Parameters.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

     

        public T GetParameterValue<T>(string name)
        {
            var parameter = this.Parameters.Where(p => p.Name == name).SingleOrDefault();

            if (parameter == null) throw new System.Exception(String.Format("{0} parametre bulunamadı.", parameter.Name));

            return ConvertHelper.ConvertValue<T>(parameter.Value);
        }

    }

    public class ParameterBuilder<T> where T : class
    {

        public List<Parameter> Parameters { get; private set; }

        public ParameterBuilder()
        {
            this.Parameters = new List<Parameter>();
        }
        public ParameterFactory Add<TProperty>(Expression<Func<T, TProperty>> param)
        {
            MemberExpression propertyBody = param.Body as MemberExpression;

            ParameterFactory parameterBuilder = new ParameterFactory(propertyBody.Member.Name);
            parameterBuilder.Direction(ParamDirection.Input).Type(propertyBody.Type);
            this.Parameters.Add(parameterBuilder.GetParameter());
            return parameterBuilder;
        }

       

    }
}
