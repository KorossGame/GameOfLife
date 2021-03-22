using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupGrid : MonoBehaviour
{
    [SerializeField] private Text XCells, YCells, AliveRules, ReproductionRules;

    private string[] resultingStrings;

    private Vector3 requiredWorldSize;
    private List<int> requiredAliveRules = new List<int>();
    private List<int> requiredReproductionRules = new List<int>();

    public void Setup()
    {
        try
        {
            // Check if any fields are empty
            if (String.IsNullOrEmpty(XCells.ToString()) || String.IsNullOrEmpty(YCells.ToString()) || String.IsNullOrEmpty(AliveRules.ToString()) || String.IsNullOrEmpty(ReproductionRules.ToString()))
            {
                return;
            }

            // World size
            requiredWorldSize.x = Math.Abs(Int32.Parse(XCells.text)) > 200 ? 200 : Math.Abs(Int32.Parse(XCells.text));
            requiredWorldSize.z = Math.Abs(Int32.Parse(YCells.text)) > 200 ? 200 : Math.Abs(Int32.Parse(YCells.text));

            // Required Alive Rules
            resultingStrings = AliveRules.text.ToString().Split(';');
            if (resultingStrings.Length >= 1)
            {
                foreach (string value in resultingStrings)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        requiredAliveRules.Add(Math.Abs(Int32.Parse(value)));
                    }
                }
            }

            // Required Reproduction Rules
            resultingStrings = ReproductionRules.text.ToString().Split(';');
            if (resultingStrings.Length >= 1)
            {
                foreach (string value in resultingStrings)
                {
                    if (!String.IsNullOrEmpty(value))
                    {
                        requiredReproductionRules.Add(Math.Abs(Int32.Parse(value)));
                    }
                }
            }

            // Clear previous values
            Grid.instance.aliveRules.Clear();
            Grid.instance.reproductionRules.Clear();

            // Setup a Grid variables
            Grid.instance.gridWorldSize = requiredWorldSize;
            Grid.instance.aliveRules = requiredAliveRules;
            Grid.instance.reproductionRules = requiredReproductionRules;

            // Load level
            SceneManager.LoadScene("GenerationLevel", LoadSceneMode.Single);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log("Values are not correct");
        }
    }
}
