using System;
using System.Collections.Generic;
using GraphAlgoritm.Classes;

namespace GraphAlgoritm.Logic.Alghoritms;

public class AlghoritmLogic
{
    private Dictionary<Vertex, Vertex> parent;

    public List<Vertex> TopologicalSort(Graph graph)
    {
        // Список для хранения отсортированных вершин
        List<Vertex> sortedVertices = new List<Vertex>();
    
        // Множество для отслеживания посещенных вершин
        HashSet<Vertex> visited = new HashSet<Vertex>();

        // Обходим все вершины графа
        foreach (var vertex in graph.Vertices)
        {
            // Если вершина не посещена, вызываем вспомогательную функцию для топологической сортировки
            if (!visited.Contains(vertex))
            {
                TopologicalSortUtil(vertex, visited, sortedVertices);
            }
        }

        sortedVertices.Reverse(); // Последовательность вершин будет в обратном порядке (топологическая сортировка)
        return sortedVertices;
    }

    private void TopologicalSortUtil(Vertex vertex, HashSet<Vertex> visited, List<Vertex> sortedVertices)
    {
        visited.Add(vertex); // Помечаем вершину как посещенную

        // Обходим все рёбра данной вершины
        foreach (var edge in vertex.Edges)
        {
            // Если конечная вершина ребра еще не посещена, рекурсивно вызываем топологическую сортировку
            if (!visited.Contains(edge.Destination))
            {
                TopologicalSortUtil(edge.Destination, visited, sortedVertices);
            }
        }

        sortedVertices.Add(vertex); // Добавляем вершину в список отсортированных вершин
    }

    public List<Edge> PrimMST(Graph graph)
    {
        List<Edge> mst = new List<Edge>(); // Список для хранения рёбер минимального остовного дерева
        HashSet<Vertex> visited = new HashSet<Vertex>(); // Множество для отслеживания посещенных вершин

        visited.Add(graph.Vertices[0]); // Начинаем с первой вершины

        // Пока в MST не будет (V-1) рёбер для V вершин
        while (mst.Count < graph.Vertices.Count - 1)
        {
            Edge minEdge = null; // Минимальное ребро для добавления в MST
            int minWeight = int.MaxValue; // Минимальный вес ребра

            // Проходим по всем посещенным вершинам
            foreach (var visitedVertex in visited)
            {
                // Проходим по всем рёбрам посещенных вершин
                foreach (var edge in visitedVertex.Edges)
                {
                    // Если ребро соединяет посещенную и непосещенную вершины и его вес меньше текущего минимального веса
                    if (!visited.Contains(edge.Destination) && edge.Weight < minWeight)
                    {
                        minEdge = edge; // Обновляем минимальное ребро
                        minWeight = edge.Weight; // Обновляем минимальный вес
                    }
                }
            }

            if (minEdge != null)
            {
                mst.Add(minEdge); // Добавляем минимальное ребро в MST
                visited.Add(minEdge.Destination); // Помечаем конечную вершину ребра как посещенную
            }
            else
            {
                break; // Все рёбра, соединяющие посещённые и непосещённые вершины, уже добавлены
            }
        }

        return mst; // Возвращаем минимальное остовное дерево
    }

    public List<Edge> KruskalMST(Graph graph)
    {
        List<Edge> mst = new List<Edge>();

        // Сначала создадим список всех рёбер
        List<Edge> allEdges = new List<Edge>();
        foreach (var vertex in graph.Vertices)
        {
            allEdges.AddRange(vertex.Edges);
        }

        // Отсортируем список рёбер по весу
        allEdges.Sort((e1, e2) => e1.Weight.CompareTo(e2.Weight));

        // Инициализируем дополнительные структуры данных для хранения компонентов связности (например, используя Union-Find)
        parent = new Dictionary<Vertex, Vertex>();
        foreach (var vertex in graph.Vertices)
        {
            parent[vertex] = vertex;
        }

        // Проходим по отсортированному списку рёбер и добавляем минимальные рёбра, не образующие циклы
        foreach (var edge in allEdges)
        {
            if (!FormsCycle(edge, mst)) // Проверка на образование цикла
            {
                mst.Add(edge);
            }
        }

        return mst;
    }

    public List<Edge> BellmanFord(Graph graph, Vertex source)
    {
        int verticesCount = graph.Vertices.Count;
        List<Edge> shortestPaths = new List<Edge>();

        int[] distances = new int[verticesCount];
        for (int i = 0; i < verticesCount; i++)
        {
            distances[i] = int.MaxValue;
        }
        distances[source.Number] = 0;

        for (int i = 0; i < verticesCount - 1; i++)
        {
            foreach (var edge in graph.Edges)
            {
                int u = edge.Source.Number;
                int v = edge.Destination.Number;
                int weight = edge.Weight;
                if (u == 0 && v == 0)
                {
                    throw new InvalidOperationException("NULS...");
                }
                if (u != 0 && v != 0) 
                {
                    if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                    {
                        distances[v] = distances[u] + weight;
                    }
                }
            }
        }

        foreach (var edge in graph.Edges)
        {
            int u = edge.Source.Number;
            int v = edge.Destination.Number;
            int weight = edge.Weight;

            if (u != 0 && v != 0) // Проверка на ноль
            {
                if (distances[u] != int.MaxValue && distances[u] + weight < distances[v])
                {
                    throw new InvalidOperationException("Граф содержит отрицательный цикл");
                }
            }
        }

        foreach (var edge in graph.Edges)
        {
            int u = edge.Source.Number;
            int v = edge.Destination.Number;
            int weight = edge.Weight;

            if (u != 0 && v != 0) // Проверка на ноль
            {
                if (distances[u] != int.MaxValue && distances[u] + weight == distances[v])
                {
                    shortestPaths.Add(edge);
                }
            }
        }

        return shortestPaths;
    }




    private bool FormsCycle(Edge edge, List<Edge> mst)
    {
        Vertex sourceParent = FindParent(edge.Source);
        Vertex destinationParent = FindParent(edge.Destination);

        if (sourceParent == destinationParent)
        {
            return true; // Образуется цикл
        }

        Union(sourceParent, destinationParent);
        return false; // Не образуется цикл
    }

    private Vertex FindParent(Vertex vertex)
    {
        if (parent[vertex] != vertex)
        {
            parent[vertex] = FindParent(parent[vertex]);
        }

        return parent[vertex];
    }

    private void Union(Vertex source, Vertex destination)
    {
        parent[source] = destination;
    }

    public int[,] FloydWarshall(Graph graph)
    {
        int verticesCount = graph.Vertices.Count;

        // Создание матрицы расстояний и инициализация её бесконечными значениями
        int[,] distances = new int[verticesCount, verticesCount];
        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                if (i == j)
                    distances[i, j] = 0;
                else
                    distances[i, j] = int.MaxValue;
            }
        }

        // Заполнение матрицы расстояний из списка рёбер
        foreach (var edge in graph.Edges)
        {
            int u = edge.Source.Number;
            int v = edge.Destination.Number;
            int weight = edge.Weight;
            distances[u, v] = weight;
        }

        // Алгоритм Флойда-Уоршелла
        for (int k = 0; k < verticesCount; k++)
        {
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (distances[i, k] != int.MaxValue && distances[k, j] != int.MaxValue &&
                        distances[i, k] + distances[k, j] < distances[i, j])
                    {
                        distances[i, j] = distances[i, k] + distances[k, j];
                    }
                }
            }
        }

        return distances;
    }

    public int[,] ConvertToFloydMatrix(List<Vertex> vertices, int n)
    {
        int[,] distances = new int[n, n];

        for (int i = 0; i < n; ++i)
        {
            for (int j = 0; j < n; ++j)
            {
                distances[i, j] = int.MaxValue;
            }
        }

        foreach (var vertex in vertices)
        {
            int u = vertex.Number;
            foreach (var edge in vertex.Edges)
            {
                int v = edge.Destination.Number;
                int weight = edge.Weight;
                distances[u, v] = weight;
            }
        }

        return distances;
    }


    public void PrintResults(List<Edge> edges)
    {
        Console.WriteLine("=== Results ===");
        foreach (var edge in edges)
        {
            Console.WriteLine(
                $"Source: {edge.Source.Name}, Destination: {edge.Destination.Name}, Weight: {edge.Weight}");
        }
    }

    public void PrintResults(List<Vertex> vertices)
    {
        Console.WriteLine("=== Topological Sort Results ===");
        foreach (var vertex in vertices)
        {
            Console.WriteLine($"Vertex Name: {vertex.Name}");
        }
    }

    public void PrintResults(int[,] shortestDistances, List<Vertex> vertices)
    {
        Console.WriteLine("=== Shortest Distances (Floyd-Warshall Algorithm) ===");
        int n = vertices.Count;

        Console.Write("     ");
        for (int i = 0; i < n; i++)
        {
            Console.Write($"{vertices[i].Name} ");
        }

        Console.WriteLine();

        for (int i = 0; i < n; ++i)
        {
            Console.Write($"{vertices[i].Name} ");
            for (int j = 0; j < n; ++j)
            {
                if (shortestDistances[i, j] == int.MaxValue)
                {
                    Console.Write("INF ");
                }
                else
                {
                    Console.Write($"{shortestDistances[i, j]} ");
                }
            }

            Console.WriteLine();
        }
    }
}