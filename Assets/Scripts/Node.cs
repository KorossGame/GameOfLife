using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool isAlive = false;

    public void Revive()
    {
        isAlive = true;
        GetComponent<Renderer>().material = Grid.instance.aliveMaterial;
    }

    public void Die()
    {
        isAlive = false;
        GetComponent<Renderer>().material = Grid.instance.deadMaterial;
    }
}
