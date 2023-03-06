using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class ShuntingYardAlgorithm : IShuntingYardAlgorithm
    {

        private readonly HashSet<char> highPriority = new HashSet<char> { '*', '/' };
        private readonly HashSet<char> lowPrority = new HashSet<char> { '+', '-' };
        private const char Subtraction = '-';
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

                    operand = ExtractNumber(input, startIndex, operand, ref i);
                    output.Enqueue(operand);
                }
                else
                {
                    if (expressionElement == OpenBracket)
                    {
                        if (operators.Count != 0)
                        {
                            var token = operators.Peek();

                            // TODO: fix the functionality for negative numbers
                            // TODO: fix it so it should work with numbers with more than one digit 
                            if (input[i + 1] == Subtraction &&
                                token == Subtraction)
                            {
                                operators.Pop();
                                operators.Push(expressionElement);
                                operators.Push(Addition);
                                i++;

                                var operand = input[i + 1].ToString();
                                var startIndex = i + 2;

                                operand = ExtractNumber(input, startIndex, operand, ref i);

                                if (input[i + 1] == Addition)
                                {
                                    output.Enqueue(operand);
                                    operators.Push(Subtraction);
                                    i += 1;
                                }

                                if (input[i + 1] == Subtraction)
                                {
                                    output.Enqueue(operand);
                                    operators.Push(Addition);
                                    i += 1;
                                }
                            }
                            else
                            {
                                operators.Push(expressionElement);
                            }
                        }
                        // TODO: apply functionality for negative numbers
                        //if (input[i + 1] == Subtraction &&
                        //         input[i + 3] == Addition)
                        //{
                        //    operators.Push(expressionElement);
                        //    output.Enqueue(input[i + 2].ToString());
                        //    output.Enqueue(input[i + 4].ToString());
                        //    operators.Push(Subtraction);
                        //    i += 4;
                        //}
                        //
                        // if (input[i + 1] == Subtraction &&
                        //         input[i + 3] == Subtraction)
                        //{
                        //    operators.Push(expressionElement);
                        //    output.Enqueue(Subtraction + input[i + 2].ToString());
                        //    output.Enqueue( input[i + 4].ToString());
                        //    operators.Push(Subtraction);
                        //    i += 4;
                        //}
                        else
                        {
                            operators.Push(expressionElement);

                        }
                    }
                    // TODO: apply rule for [-,+] inside brackets
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

        private static string ExtractNumber(string input, int startIndex, string operand, ref int i)
        {
            while (startIndex <= input.Length - 1 &&
                   char.IsNumber(input[startIndex]))
            {
                operand += input[startIndex];
                startIndex++;
                i++;
            }

            return operand;
        }
    }
}
