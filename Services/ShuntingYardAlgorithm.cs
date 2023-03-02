using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class ShuntingYardAlgorithm:IShuntingYardAlgorithm
    {

        private readonly HashSet<char> highPriority = new HashSet<char> { '*', '/' };
        private readonly HashSet<char> lowPrority = new HashSet<char> { '+', '-' };

        public Queue<string> ShuntingYardAlgorithmResult(string input)
        {
            var operators = new Stack<char>();

            var output = new Queue<string>();

            for (int i = 0; i < input.Length; i++)
            {
                var expressionElement = input[i];

                if (char.IsNumber(expressionElement))
                {
                    var operand = expressionElement.ToString();
                    var startIndex = i + 1;

                    while (startIndex < input.Length - 1 &&
                           char.IsNumber(input[startIndex]))
                    {
                        operand += input[startIndex];
                        startIndex++;
                        i++;
                    }

                    output.Enqueue(operand);
                }
                else
                {
                    if (expressionElement == ')')
                    {
                        var currentElement = operators.Pop();

                        while (currentElement != '(')
                        {
                            output.Enqueue(currentElement.ToString());
                            currentElement = operators.Pop();
                        }
                    }
                    else
                    {
                        var topStackOperand = ' ';

                        if (operators.Count != 0)
                        {
                            topStackOperand = operators.Peek();
                        }

                        if (highPriority.Contains(topStackOperand) &&
                            lowPrority.Contains(expressionElement))
                        {
                            output.Enqueue(operators.Pop().ToString());
                        }
                        else if (topStackOperand == '*' && expressionElement == '/')
                        {
                            output.Enqueue(operators.Pop().ToString());

                        }

                        operators.Push(expressionElement);
                    }

                }
            }

            while (operators.Count != 0)
            {
                output.Enqueue(operators.Pop().ToString());
            }

            return output;
        }
    }
}
