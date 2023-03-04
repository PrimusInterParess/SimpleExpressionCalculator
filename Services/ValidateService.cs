using MathExpressionSolver.Services.Contracts;
using System.Linq;

namespace MathExpressionSolver.Services
{
    public class ValidateService : IValidateService
    {
        private readonly HashSet<char> elements = new HashSet<char> { '+', '-', '*', '/', '(', ')' };
        private readonly HashSet<char> operators = new HashSet<char> { '+', '-', '*', '/' };
        private readonly HashSet<char> notAllowedBeforeOpenBrackets = new HashSet<char> { '*', '/' ,'+'};
        private const char OpenBracket = '(';
        private const char ClosedBracket = ')';
        private const char Minus = '-';

        public bool ZeroNumber(double number)
        {
            return number == 0;
        }

        public void IsInputValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new InvalidOperationException("Input data is null or whitespace");
            }

            if (input.Length <= 2)
            {
                throw new InvalidOperationException("Expression length is invalid");
            }

            if (StartsWithNegativeSigh(input))
            {
                throw new InvalidOperationException("Invalid expression. Cannot start with negative sign");
            }

            if (EndsWithOperator(input))
            {
                throw new InvalidOperationException("Invalid expression. Cannot end with an operator");
            }

            if (NotValidExpression(input))
            {
                throw new InvalidOperationException("Invalid expression. Contains incorrect characters");
            }

            if (AreParenthesisBalanced(input) == false)
            {
                throw new InvalidOperationException("Invalid expression. Parenthesis are unbalanced");
            }
        }

        private bool NotValidExpression(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (this.elements.Contains(input[i]) == false && char.IsNumber(input[i]) == false)
                {
                    return true;
                }

                if (input[i] == OpenBracket)
                {
                    if (notAllowedBeforeOpenBrackets.Contains(input[i + 1]))
                    {
                        return true;
                    }
                }

                if (input[i] == ClosedBracket)
                {
                    if (operators.Contains(input[i - 1]))
                    {
                        return true;
                    }
                }

                if (operators.Contains(input[i]) && 
                    operators.Contains(input[i+1]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool EndsWithOperator(string input)
        {
            if (operators.Contains(input[input.Length - 1]))
            {
                return true;
            }

            return false;
        }

        private bool StartsWithNegativeSigh(string input)
        {
            return input[0] == Minus;
        }

        private bool AreParenthesisBalanced(string input)
        {
            var parenthesis = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == OpenBracket || input[i] == ClosedBracket)
                {
                    parenthesis += input[i];
                }
            }

            Dictionary<char, char> pairsParentheses = new Dictionary<char, char>()
            {
                { '(',')'}
            };

            Stack<char> parenthesesChars = new Stack<char>();


            for (int i = 0; i < parenthesis.Length; i++)
            {
                char ch = parenthesis[i];

                if (ch == OpenBracket)
                {
                    parenthesesChars.Push(ch);

                }
                else if (parenthesesChars.Count == 0)
                {
                    return false;
                }
                else
                {
                    char lastBracket = parenthesesChars.Pop();

                    char expected = pairsParentheses[lastBracket];

                    if (ch != expected)
                    {
                        return false;
                    }

                }
            }

            return parenthesesChars.Count == 0 ? true : false;
        }
    }
}
