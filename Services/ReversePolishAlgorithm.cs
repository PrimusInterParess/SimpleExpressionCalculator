using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class ReversePolishAlgorithm:IReversePolishAlgorithm
    {
        private readonly IValidateService _validateService;
        private const string Division = "/";
        private const string Multiplication = "*";
        private const string Addition = "+";
        private const string Subtraction = "-";

        public ReversePolishAlgorithm(IValidateService validateService)
        {
            _validateService = validateService;

        }

        public string ReversePolishAlgorithmResult(Queue<string> shuntingYardAlgorithmResult)
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

                    if (numberStack.Count >= 2)
                    {
                        switch (currentElement)
                        {
                            case Addition:
                                result = this.Add(numberStack.Pop(), numberStack.Pop());
                                break;
                            case Subtraction:
                                result = this.Subtract(numberStack.Pop(), numberStack.Pop());
                                break;
                            case Multiplication:
                                result = this.Multiply(numberStack.Pop(), numberStack.Pop());
                                break;
                            case Division:
                                result = this.Divide(numberStack.Pop(), numberStack.Pop());
                                break;
                        }

                        numberStack.Push(result);
                    }

                }
            }

            return string.Join("", numberStack);
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
            if (this._validateService.ZeroNumber(first) ||
                this._validateService.ZeroNumber(second))
            {
                throw new InvalidOperationException("Invalid operation-Zero division");
            }

            return second / first;
        }
    }
}
