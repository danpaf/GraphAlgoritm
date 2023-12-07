namespace GraphAlgoritm.Classes;

public class Matrix
{
    public int[,] AdjacencyMatrix { get; set; }
    public int[,] IncidenceMatrix { get; set; }
    public int[,] WeightMatrix { get; set; }
    public int[,] DistanceMatrix { get; set; }
    public int[,] CostMatrix { get; set; }
    public int[,] CapacityMatrix { get; set; }
    public int[,] FlowMatrix { get; set; }
    public int[,] TransposeMatrix { get; set; }
    public int[,] InverseMatrix { get; set; }
    public int[,] IdentityMatrix { get; set; }
    public int[,] NullMatrix { get; set; }
    public int[,] DiagonalMatrix { get; set; }
    public int[,] LaplacianMatrix { get; set; }
    public int[,] AdjacencyMatrixToIncidenceMatrix { get; set; }
    public int[,] IncidenceMatrixToAdjacencyMatrix { get; set; }
    public int[,] AdjacencyMatrixToWeightMatrix { get; set; }
    public int[,] WeightMatrixToAdjacencyMatrix { get; set; }
}