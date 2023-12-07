namespace GraphAlgoritm.Classes;

public class Edge
{
    public Vertex Destination { get; set; }
    public int Weight { get; set; }
    public Vertex Source { get; set; }

    public Edge(Vertex destination, int weight, Vertex source)
    {
        Destination = destination;
        Weight = weight;
        Source = source;
    }
}
