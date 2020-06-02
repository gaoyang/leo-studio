using System.Collections.Generic;
using System.Linq;

namespace Lab.LambdaTest
{
    public class Query
    {
        internal Query()
        {
            Queries = new List<Query>();
        }

        internal QueryOperation Operation { get; set; }
        internal QueryConnector Connector { get; set; }

        internal string Field { get; set; }

        internal string DbField { get; set; }

        internal object Value { get; set; }

        internal List<Query> Queries { get; set; }

        public Query Equal(string field, object value)
        {
            Equal(field, field, value);
            return this;
        }

        public Query Equal(string field, string dbField, object value)
        {
            var query = Queries.LastOrDefault();
            if (query == null)
            {
                Queries.Add(new Query
                {
                    Operation = QueryOperation.Equal,
                    Field = field,
                    DbField = dbField,
                    Value = value
                });
            }
            else
            {
                query.Operation = QueryOperation.Equal;
                query.Field = field;
                query.DbField = dbField;
                query.Value = value;
            }
            return this;
        }


        public Query And()
        {
            Queries.Add(new Query
            {
                Connector = QueryConnector.And
            });
            return this;
        }

        public Query Or()
        {
            Queries.Add(new Query
            {
                Connector = QueryConnector.Or
            });
            return this;
        }

    }

    internal enum QueryOperation
    {
        None,
        Equal
    }

    internal enum QueryConnector
    {
        None,
        And,
        Or
    }
}
