
class Program
{
    static void Main()
    {
        string inputFile = "In0301.txt";
        string outputFile = "out0301.txt";

        string[] lines = File.ReadAllLines(inputFile);

        if (int.TryParse(lines[0], out int n))
        {
            for (int i = 1; i <= n * 2; i += 2)
            {
                string odmiana1 = lines[i];
                string odmiana2 = lines[i + 1];

                string lcs = ZnajdzNajdluzszyWspolnyPodciag(odmiana1, odmiana2);

                File.AppendAllText(outputFile, $"{lcs.Length} {lcs}" + Environment.NewLine);
            }
        }
        else
        {
            Console.WriteLine("Błąd parsowania liczby par odmian tulibajtu.");
        }
    }

    static string ZnajdzNajdluzszyWspolnyPodciag(string s1, string s2)
    {
        int[,] dp = new int[s1.Length + 1, s2.Length + 1];

        for (int i = 0; i <= s1.Length; i++)
        {
            for (int j = 0; j <= s2.Length; j++)
            {
                if (i == 0 || j == 0)
                {
                    dp[i, j] = 0;
                }
                else if (s1[i - 1] == s2[j - 1])
                {
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                }
                else
                {
                    dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                }
            }
        }

        int index = dp[s1.Length, s2.Length];
        char[] lcs = new char[index];

        int k = s1.Length, l = s2.Length;
        while (k > 0 && l > 0)
        {
            if (s1[k - 1] == s2[l - 1])
            {
                lcs[--index] = s1[k - 1];
                k--;
                l--;
            }
            else if (dp[k - 1, l] > dp[k, l - 1])
            {
                k--;
            }
            else
            {
                l--;
            }
        }

        return new string(lcs);
    }
}

/**/
/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class PrimsAlgorithm
{
    static List<(int u, int v, int weight)> JarnikPrim(List<(int, int)[]> graph, int startVertex)
    {
        int n = graph.Count;

        List<(int u, int v, int weight)> MDR = new List<(int u, int v, int weight)>();

        // zbior krawędzi Q
        List<(int u, int v, int weight)> Q = new List<(int u, int v, int weight)>();
        for (int uIndex = 0; uIndex < n; uIndex++)
        {
            foreach ((int neighbor, int weight) in graph[uIndex])
            {
                Q.Add((uIndex, neighbor, weight));
            }
        }

        // Sortowanie krawędzi według wag
        Q = Q.OrderBy(e => e.weight).ToList();

        // Inicjalizacja zbioru W
        List<int> W = new List<int> { startVertex };

        int edgeCount = 0; 

        while (W.Count < n)
        {
            var edge = Q.FirstOrDefault(e => (W.Contains(e.u) && !W.Contains(e.v)) || (W.Contains(e.v) && !W.Contains(e.u)));

            if (edge.u != -1)
            {
                MDR.Add((edge.u, edge.v, edge.weight));
                W.Add(edge.v);
                Q.RemoveAll(e => W.Contains(e.v) && !W.Contains(e.u));
                edgeCount++; 
            }
        }

        Console.WriteLine($"Liczba krawędzi w minimalnym drzewie rozpinającym: {edgeCount}");

        return MDR;
    }

    static void Main()
    {
       
        string[] lines = File.ReadAllLines("In0304.txt");
        int n = int.Parse(lines[0]);

        // lista incydencji
        List<(int, int)[]> graph = new List<(int, int)[]>(n);
        for (int i = 1; i <= n; i++)
        {
            string[] tokens = lines[i].Split(' ');

            // Numer z wierzchołka z którego idziemy odpowiada numerowi linii
            int u = i - 1;
            List<(int, int)> neighbors = new List<(int, int)>();
            for (int j = 0; j < tokens.Length - 1; j += 2)
            {
                int v = int.Parse(tokens[j]) - 1;
                int weight = int.Parse(tokens[j + 1]);
                neighbors.Add((v, weight));
            }
            graph.Add(neighbors.ToArray());
        }

        // Wybór startowego wierzchołka
        int startVertex = 11;

        // Wywołanie algorytmu Prima
        List<(int u, int v, int weight)> minimumSpanningTree = JarnikPrim(graph, startVertex);

        using (StreamWriter writer = new StreamWriter("Out0304.txt"))
        {
            foreach (var edge in minimumSpanningTree)
            {
                writer.Write($"{edge.u + 1} {edge.v + 1} [{edge.weight}], ");
            }
            writer.WriteLine();
            int totalWeight = minimumSpanningTree.Sum(e => e.weight);
            writer.WriteLine($"Suma wag: {totalWeight}");
            writer.WriteLine($"Liczba krawędzi w minimalnym drzewie rozpinającym: {minimumSpanningTree.Count}");
        }
    }
}


}*/
