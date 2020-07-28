using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Lab.LambdaTest.Models;

namespace Lab.LambdaTest
{
    public class Main
    {
        static readonly List<object> _data = new List<object>(Mock.GetData());

        public static void Run()
        {
            // 序列化Lambda
            var testData = new[]
            {
                1,
                2,
                3,
                7,
                8
            };

            var res = testData.Where(o => o > 3).ToList();

            Expression<Func<int, bool>> expression = o => o > 3;
            var parameter = Expression.Parameter(typeof(int[]), "_o");
            var lambdaExpression = DynamicExpressionParser.ParseLambda(new[]
            {
                parameter
            }, typeof(IEnumerable<int>), $"_o.Where({expression})");
            var sss = lambdaExpression.Compile().DynamicInvoke(testData) as IEnumerable<int>;
            var res2 = sss.ToList();

            // 动态组装Lambda
            var query = new Query().Equal("Parent.Name", "张三的爹").And().Equal("Name", "张三");
            var type = typeof(User);

            // o=>
            var parameterExpr = Expression.Parameter(type, "o");
            // Func<User,bool>
            var funcType = Expression.GetFuncType(type, typeof(bool));
            Expression expr = null;
            foreach (var queryItem in query.Queries)
            {
                // o=>Parent.Name
                MemberExpression propertyExpr = null;
                foreach (var field in queryItem.Field.Split('.'))
                    if (propertyExpr == null)
                        propertyExpr = Expression.PropertyOrField(parameterExpr, field);
                    else
                        propertyExpr = Expression.PropertyOrField(propertyExpr, field);
                // 值
                var valueExpr = Expression.Constant(queryItem.Value);
                // 运算符
                BinaryExpression binaryExpr = null;
                switch (queryItem.Operation)
                {
                    case QueryOperation.Equal:
                        // =
                        binaryExpr = Expression.Equal(propertyExpr, valueExpr);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (expr == null)
                {
                    // o=>Parent.Name=="张三的爹"
                    expr = binaryExpr;
                    continue;
                }

                switch (queryItem.Connector)
                {
                    // o=>Parent.Name=="张三的爹"&&Name=="张三"
                    case QueryConnector.And:
                        expr = Expression.And(expr, binaryExpr);
                        break;
                    // o=>Parent.Name=="张三的爹"||Name=="张三"
                    case QueryConnector.Or:
                        expr = Expression.Or(expr, binaryExpr);
                        break;
                    default:
                        expr = binaryExpr;
                        break;
                }
            }

            // Lambda<Func<User,bool>> = o=>Parent.Name=="张三的爹"&&Name=="张三"
            var lambda = Expression.Lambda(funcType, expr, parameterExpr);
            //把 List<object> 转为 List<User>
            var listType = typeof(List<>).MakeGenericType(type);
            var listInstance = Activator.CreateInstance(listType);
            var list = listInstance as IList;
            foreach (var item in _data)
                list.Add(item);
            // list.FirstOrDefault(o=>Parent.Name=="张三的爹"&&Name=="张三")
            var firstOrDefault = Expression.Call(typeof(Enumerable), nameof(Enumerable.FirstOrDefault), new[]
            {
                type
            }, Expression.Constant(list), lambda);
            // 执行表达式
            var result = Expression.Lambda(firstOrDefault).Compile().DynamicInvoke();

            Console.WriteLine("LambdaTest end");
            Console.ReadLine();
        }
    }
}