using MathExpressionSolver.Services.Contracts;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MathExpressionSolver.Services
{
    public class StringService:IStringService
    {
        public string RemoveWhiteSpace(string data)
        {
            return Regex.Replace(data, @"\s", "");

        }

        public Queue<char> GetExpressionQ(string data)
        { 
            var result = new Queue<char>();
            
            for (int i = 0; i < data.Length; i++)
            {
                result.Enqueue(data[i]);
            }

            return result;
        }
    }
}
