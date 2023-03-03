using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class ShuntingYardAlgorithm : IShuntingYardAlgorithm
    {

        private readonly HashSet<char> highPriority = new HashSet<char> { '*', '/' };
        private readonly HashSet<char> lowPrority = new HashSet<char> { '+', '-' };
        private const char Minus = '-';
        private const char Addition = '+';
        private const char CloseBracket = ')';
        private const char OpenBracket = '(';

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

                    while (startIndex <= input.Length - 1 &&
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
                    if (expressionElement == OpenBracket)
                    {
                        if (operators.Count != 0)
                        {
                            var token = operators.Peek();

                            if (input[i + 1] == Minus &&
                                token == Minus)
                            {
                                operators.Pop();
                                operators.Push(expressionElement);
                                operators.Push(Addition);
                                i++;

                                if (input[i + 2] == Addition)
                                {
                                    output.Enqueue(input[i + 1].ToString());
                                    operators.Push(Minus);
                                    i += 2;
                                }

                                if (input[i + 2] == Minus)
                                {
                                    output.Enqueue(input[i + 1].ToString());
                                    operators.Push(Addition);
                                    i += 2;
                                }
                            }
                            else
                            {
                                operators.Push(expressionElement);
                            }
                        }
                        else
                        {
                            operators.Push(expressionElement);

                        }
                    }
                    else if (expressionElement == CloseBracket)
                    {
                        var currentElement = operators.Pop();

                        while (currentElement != OpenBracket)
                        {
                            output.Enqueue(currentElement.ToString());
                            if (operators.Count > 0)
                            {
                                currentElement = operators.Pop();

                            }
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
                        else if (highPriority.Contains(topStackOperand) &&
                                 highPriority.Contains(expressionElement))
                        {
                            output.Enqueue(operators.Pop().ToString());
                        }
                        else if (lowPrority.Contains(topStackOperand) &&
                                lowPrority.Contains(expressionElement))
                        {
                            output.Enqueue((operators.Pop().ToString()));
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
