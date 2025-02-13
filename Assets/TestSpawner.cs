using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public int rows = 5;
    public int cols = 5;
    public float spacing = 1.5f;
    public SpawnPattern pattern;

    public enum SpawnPattern
    {
        Chessboard,
        Rows,
        Columns,
        Diagonal,
        Random,
        Border,
        Cross,
        XShape,
        CheckerboardOffset,
        Circle,
        Diamond,
        Spiral,
        RandomClusters,
        Waves,
        Triangle
    }

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        Vector2 startPos = new Vector2(-cols * spacing / 2, -rows * spacing / 2);
        Vector2 center = new Vector2(cols / 2, rows / 2);
        float radius = Mathf.Min(rows, cols) / 2f;

        int spiralCount = 0; // Track number of spiral iterations

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                bool shouldSpawn = ShouldSpawn(row, col, center, radius, ref spiralCount);
                if (shouldSpawn)
                {
                    Vector2 spawnPos = startPos + new Vector2(col * spacing, row * spacing);
                    Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
                }
            }
        }
    }

    bool ShouldSpawn(int row, int col, Vector2 center, float radius, ref int spiralCount)
    {
        switch (pattern)
        {
            case SpawnPattern.Chessboard:
                return (row + col) % 2 == 0;
            case SpawnPattern.Rows:
                return row % 2 == 0;
            case SpawnPattern.Columns:
                return col % 2 == 0;
            case SpawnPattern.Diagonal:
                return row == col;
            case SpawnPattern.Random:
                return Random.value > 0.5f;
            case SpawnPattern.Border:
                return row == 0 || row == rows - 1 || col == 0 || col == cols - 1;
            case SpawnPattern.Cross:
                return col == cols / 2 || row == rows / 2;
            case SpawnPattern.XShape:
                return row == col || row + col == cols - 1;
            case SpawnPattern.CheckerboardOffset:
                return (row / 2 + col) % 2 == 0;
            case SpawnPattern.Circle:
                return Vector2.Distance(new Vector2(col, row), center) <= radius;
            case SpawnPattern.Diamond:
                return Mathf.Abs(row - (int)center.y) + Mathf.Abs(col - (int)center.x) <= radius;
            case SpawnPattern.Spiral:
                spiralCount++;
                return (row + col + spiralCount / 2) % 4 == 0;
            case SpawnPattern.RandomClusters:
                return Random.value > 0.75f || (row > 0 && col > 0 && Random.value > 0.5f);
            case SpawnPattern.Waves:
                return Mathf.Sin(col * 0.5f) * rows / 2 + rows / 2 > row;
            case SpawnPattern.Triangle:
                return row <= col;
            default:
                return true;
        }
    }
}
