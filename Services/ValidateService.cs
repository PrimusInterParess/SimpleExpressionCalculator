using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class ValidateService : IValidateService
    {
        private readonly HashSet<char> elements = new HashSet<char> { '+', '-', '*', '/', '(', ')' };

        public bool IsInputValid(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            if (input.Length < 2)
            {
                return false;
            }

            if (ContainsCharacters(input))
            {
                return false;
            }

            if (AreParenthesisBalanced(input) == false) 
            {
                return false;
            }

            return true;
        }

        private bool ContainsCharacters(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (this.elements.Contains(input[i]) == false && char.IsNumber(input[i]) == false)
                {
                    return true;
                }
            }

            return false;
        }

        private bool AreParenthesisBalanced(string input)
        {
            var parenthesis = "";

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(' || input[i] == ')')
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

                if (ch == '(')
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
