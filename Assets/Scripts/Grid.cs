using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static Grid instance;

    public List<int> aliveRules = new List<int> { 2, 3 };
    public List<int> reproductionRules = new List<int> { 3 };

    // Node size
    public float nodeRadius = 0.5f;
    private float nodeDiameter;

    // Customizable world area
    public Vector3 gridWorldSize;
    private int gridXCount, gridZCount;

    // Node List
    private Node[,] nodeList;
    private bool[,] nextGenerationList;

    // Working with nodes
    private Vector3 worldPoint;
    private GameObject cell;
    private Node currentNode;
    private Node testingNode;
    private int neighbours;

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform nodeHolder;

    // Node materials
    public Material deadMaterial;
    public Material aliveMaterial;

    private void Awake()
    {
        instance = this;
    }

    public void SetupAndGenerate()
    {
        SetupGrid();
        GenerateGrid();
    }

    private void SetupGrid()
    {
        nodeDiameter = nodeRadius * 2;

        gridXCount = (int)(gridWorldSize.x / nodeDiameter);
        gridZCount = (int)(gridWorldSize.z / nodeDiameter);

        // Create new array to store all the nodes
        nodeList = new Node[gridXCount, gridZCount];
        nextGenerationList = new bool[gridXCount, gridZCount];
    }

    private void GenerateGrid()
    {
        // Calculate the bottol left corner of the world area
        Vector3 bottomLeft = transform.position - (Vector3.right * gridWorldSize.x / 2) - (Vector3.forward * gridWorldSize.z / 2);

        // Save each node
        for (int x = 0; x < gridXCount; x++)
        {
            for (int z = 0; z < gridZCount; z++)
            {
                worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (z * nodeDiameter + nodeRadius);
                cell = Instantiate(cellPrefab, worldPoint, Quaternion.identity, nodeHolder);

                // Save the node
                nodeList[x, z] = cell.GetComponent<Node>();
            }
        }
    }

    public void NextGeneration()
    {
        // Go over all nodes
        for (int x = 0; x < gridXCount; x++)
        {
            for (int z = 0; z < gridZCount; z++)
            {
                nextGenerationList[x, z] = nodeList[x, z].isAlive;

                // Count the number of neighbours and save new node status to next wave array
                currentNode = nodeList[x, z];
                neighbours = CountAliveNeighbours(x, z);

                if (!aliveRules.Contains(neighbours))
                {
                    nextGenerationList[x, z] = false;
                }
                else
                {
                    if (currentNode.isAlive && aliveRules.Contains(neighbours))
                    {
                        nextGenerationList[x, z] = true;
                    }

                    if (!currentNode.isAlive && reproductionRules.Contains(neighbours))
                    {
                        nextGenerationList[x, z] = true;
                    }
                }
            }
        }

        ApplyNewGeneration();
    }

    public void ClearGrid()
    {
        for (int x = 0; x < gridXCount; x++)
        {
            for (int z = 0; z < gridZCount; z++)
            {
                nodeList[x, z].Die();
            }
        }
    }

    private void ApplyNewGeneration()
    {
        for (int x = 0; x < gridXCount; x++)
        {
            for (int z = 0; z < gridZCount; z++)
            {
                if (nextGenerationList[x, z] == true)
                {
                    nodeList[x, z].Revive();
                }
                else
                {
                    nodeList[x, z].Die();
                }
            }
        }
    }

    private int CountAliveNeighbours(int x, int z)
    {
        int result = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try
                {
                    testingNode = nodeList[x + i, z + j];

                    if (testingNode.isAlive && testingNode != nodeList[x,z])
                    {
                        result++;
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
        return result;
    }
}
