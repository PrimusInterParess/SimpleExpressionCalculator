using MathExpressionSolver.Services;
using Xunit;

namespace MathExpressionSolver.Tests
{
    public class ExpressionCalculatorTests   
    {
        [Fact]
        public void ZeroDivisionShouldThrowException()
        {
            var zeroNumberExpression = "23*(2 +3)/2*3/0";

            var stringService = new StringService();
            var validateService = new ValidateService();
            var polishAlgo = new ReversePolishAlgorithm(validateService);
            var shuntingYardAlgo = new ShuntingYardAlgorithm();

            var calculateService = new CalculateService(stringService,validateService,shuntingYardAlgo,polishAlgo);

            Assert.Throws<InvalidOperationException>(()=>calculateService.ProcessData(zeroNumberExpression));
        }

        [Fact]
        public void CorrectExpressionShouldReturnString()
        {
            var correctExpression = "(1 + 2) * 4 / 6";

            var stringService = new StringService();
            var validateService = new ValidateService();
            var polishAlgo = new ReversePolishAlgorithm(validateService);
            var shuntingYardAlgo = new ShuntingYardAlgorithm();

            var calculateService = new CalculateService(stringService, validateService, shuntingYardAlgo, polishAlgo);

            var actual = calculateService.ProcessData(correctExpression);

            var expected = "2";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UnbalancedParenthesisShouldThrowException()
        {
            var invalidParenthesisExpression = "(1 + 2) * 4 / 6))";

            var stringService = new StringService();
            var validateService = new ValidateService();
            var polishAlgo = new ReversePolishAlgorithm(validateService);
            var shuntingYardAlgo = new ShuntingYardAlgorithm();

            var calculateService = new CalculateService(stringService, validateService, shuntingYardAlgo, polishAlgo);

            Assert.Throws<InvalidOperationException>(() => calculateService.ProcessData(invalidParenthesisExpression));

        }
    }
}
