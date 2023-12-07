using System.Collections.Generic;
using GraphAlgoritm.Classes;
using GraphAlgoritm.Logic.Alghoritms;

Graph graph = new Graph();
Vertex v1 = new Vertex("A");
Vertex v2 = new Vertex("B");
Vertex v3 = new Vertex("C");
Vertex v4 = new Vertex("D");
Vertex v5 = new Vertex("E");
Vertex v6 = new Vertex("F");
Vertex v7 = new Vertex("G");
Vertex v8 = new Vertex("H");
Vertex v9 = new Vertex("I");



graph.AddVertex(v1);
graph.AddVertex(v2);
graph.AddVertex(v3);
graph.AddVertex(v4);
graph.AddVertex(v5);
graph.AddVertex(v6);
graph.AddVertex(v7);
graph.AddVertex(v8);
graph.AddVertex(v9);

graph.AddEdge(v1, v2, 4);
graph.AddEdge(v1, v3, 3);
graph.AddEdge(v2, v3, 2);
graph.AddEdge(v2, v4, 5);
graph.AddEdge(v3, v4, 1);
graph.AddEdge(v3, v5, 6);
graph.AddEdge(v4, v5, 3);
graph.AddEdge(v4, v6, 2);
graph.AddEdge(v5, v6, 7);
graph.AddEdge(v5, v7, 4);
graph.AddEdge(v6, v7, 1);
graph.AddEdge(v1, v2, 5);
graph.AddEdge(v2, v3, 10);
graph.AddEdge(v1, v3, 6);


AlghoritmLogic logic = new AlghoritmLogic();
List<Edge> primMST = logic.PrimMST(graph);
logic.PrintResults(primMST);

List<Edge> kruskalMST = logic.KruskalMST(graph);
logic.PrintResults(kruskalMST);

List<Vertex> topologicalSort = logic.TopologicalSort(graph);
logic.PrintResults(topologicalSort);

List<Edge> bellmanFord = logic.BellmanFord(graph,v1);
logic.PrintResults(bellmanFord);

int[,] shortestDistances = logic.FloydWarshall(graph);
logic.PrintResults(shortestDistances, graph.Vertices);





