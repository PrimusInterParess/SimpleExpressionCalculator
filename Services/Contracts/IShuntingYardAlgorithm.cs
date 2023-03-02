namespace MathExpressionSolver.Services.Contracts
{
    public interface IShuntingYardAlgorithm
    {
        Queue<string> ShuntingYardAlgorithmResult(string input);
    }
}
