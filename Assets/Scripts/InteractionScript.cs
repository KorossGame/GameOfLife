using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    public void updateGrid()
    {
        Grid.instance.NextGeneration();
    }

    public void resetGrid()
    {
        Grid.instance.ClearGrid();
    }
}
