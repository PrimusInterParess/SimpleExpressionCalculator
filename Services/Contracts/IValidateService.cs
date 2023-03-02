namespace MathExpressionSolver.Services.Contracts
{
    public interface IValidateService
    {
        public void IsInputValid(string input);

        public bool ZeroNumber(double number);
    }
}
