using System.Collections;
using System.Collections.Generic;

namespace GraphAlgoritm.Classes;

public class Vertex
{
    private static int counter = 0;
    public string Name { get; set; }
    public List<Edge> Edges { get; set; }
    
    public int Distance { get; set; }
    public int Cost { get; set; }
    public Vertex Previous { get; set; }
    public bool Visited { get; set; }
    public int Number { get; set; }

    public Vertex(string name)
    {
        Name = name;
        Edges = new List<Edge>();
        Number = counter++;
    }

    public void AddEdgeSource(Vertex destination, int weight,Vertex source)
    {
        Edges.Add(new Edge(destination, weight,source));
    }
    
}