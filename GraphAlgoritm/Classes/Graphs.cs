using System.Collections.Generic;

namespace GraphAlgoritm.Classes;

public class Graph
{
    public List<Vertex> Vertices { get; set; }
    public List<Edge> Edges { get; set; }
    
    public Graph()
    {
        Vertices = new List<Vertex>(); // Инициализация списка вершин
        Edges = new List<Edge>(); // Инициализация списка рёбер
    }
    
    public void AddVertex(Vertex vertex)
    {
        Vertices.Add(vertex);
    }
    public void AddEdge(Vertex source, Vertex destination, int weight)
    {
        Edge newEdge = new Edge(destination, weight, source);
        source.AddEdgeSource( destination, weight,source);
        Edges.Add(newEdge);
    }
    
}