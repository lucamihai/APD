namespace APD.L3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var quadraticEquationSolver = new QuadraticEquationSolver();
            var solutions = quadraticEquationSolver.Solve(1, -4, 1);
        }
    }
}
