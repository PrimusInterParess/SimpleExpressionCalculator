using MathExpressionSolver.Services.Contracts;

namespace MathExpressionSolver.Services
{
    public class CalculateService : ICalculate
    {
        private readonly IStringService _stringService;
        private readonly IValidateService _validateService;
        private readonly IShuntingYardAlgorithm _shuntingYardAlgorithm;
        private readonly IReversePolishAlgorithm _reversePolishAlgorithm;

        public CalculateService(
            IStringService stringService,
            IValidateService validateService,
            IShuntingYardAlgorithm shuntingYardAlgorithm,
            IReversePolishAlgorithm reversePolishAlgorithm)
        {
            _stringService = stringService;
            _validateService = validateService;
            _shuntingYardAlgorithm = shuntingYardAlgorithm;
            _reversePolishAlgorithm = reversePolishAlgorithm;
        }
        public string ProcessData(string input)
        {

            input = this._stringService.RemoveWhiteSpace(input);

            try
            {
                this._validateService.IsInputValid(input);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }

            var shuntingYardAlgorithmResult = this._shuntingYardAlgorithm.ShuntingYardAlgorithmResult(input);
            try
            {
                var reversePolishAlgorithmResult = this._reversePolishAlgorithm.ReversePolishAlgorithmResult(shuntingYardAlgorithmResult);
                return reversePolishAlgorithmResult;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
           
        }
    }
}
