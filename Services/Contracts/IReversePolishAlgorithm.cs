namespace MathExpressionSolver.Services.Contracts
{
    public interface IReversePolishAlgorithm
    {
        string ReversePolishAlgorithmResult(Queue<string> shuntingYardAlgorithmResult);
    }
}
