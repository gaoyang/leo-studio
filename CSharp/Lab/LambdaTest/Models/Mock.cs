using System.Collections.Generic;

namespace Lab.LambdaTest.Models
{
    public class Mock
    {
        public static IEnumerable<object> GetData()
        {
            yield return new User
            {
                Name = "张三",
                Age = 18,
                Parent = new User
                {
                    Name = "张三的爹",
                    Age = 45
                }
            };

            yield return new User
            {
                Name = "李四",
                Age = 18,
                Parent = new User
                {
                    Name = "李四的爹",
                    Age = 45
                }
            };

            yield return new User
            {
                Name = "王五",
                Age = 18,
                Parent = new User
                {
                    Name = "王五的爹",
                    Age = 45
                }
            };

        }
    }
}
