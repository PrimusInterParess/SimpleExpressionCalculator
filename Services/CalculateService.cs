using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class CalculateService : ICalculate
    {
        private readonly IStringService _stringService;
        private readonly IValidateService _validateService;
        private readonly HashSet<char> highPriority = new HashSet<char> { '*', '/' };
        private readonly HashSet<char> lowPrority = new HashSet<char> { '+', '-' };
        public CalculateService(
            IStringService stringService,
            IValidateService validateService)
        {
            _stringService = stringService;
            _validateService = validateService;
        }
        public string ProcessData(string input)
        {

            input = this._stringService.RemoveWhiteSpace(input);

            var isInputValid = this._validateService.IsInputValid(input);

            if (isInputValid == false)
            {
                throw new InvalidOperationException("Invalid input data");
            }

            var shuntingYardAlgorithmResult = this.ShuntingYardAlgorithm(input);

            var reversedPolishAlgorithm = ReversedPolishAlgorithm(shuntingYardAlgorithmResult);


            return reversedPolishAlgorithm;
        }

        private string ReversedPolishAlgorithm(Queue<string> shuntingYardAlgorithmResult)
        {
            var numberStack = new Stack<double>();

            foreach (var element in shuntingYardAlgorithmResult)
            {
                var currentElement = element;

                if (double.TryParse(currentElement, out double addNumber))
                {
                    numberStack.Push(addNumber);
                }
                else
                {
                    var result = 0.0;

                    switch (currentElement)
                    {
                        case "+":
                            result = this.Add(numberStack.Pop(), numberStack.Pop());
                            break;
                        case "-":
                            result = this.Subtract(numberStack.Pop(), numberStack.Pop());
                            break;
                        case "*":
                            result = this.Multiply(numberStack.Pop(), numberStack.Pop());
                            break;
                        case "/":
                            result = this.Divide(numberStack.Pop(), numberStack.Pop());
                            break;
                    }

                    numberStack.Push(result);

                }
            }

            return string.Join("", numberStack);
        }

        private Queue<string> ShuntingYardAlgorithm(string input)
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


        private double Add(double first, double second)
        {
            return first + second;
        }

        private double Multiply(double first, double second)
        {
            return first * second;
        }

        private double Subtract(double first, double second)
        {
            return second - first;
        }

        private double Divide(double first, double second)
        {
            return second / first;
        }
    }
}
