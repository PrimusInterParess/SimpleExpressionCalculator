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
    }
}
