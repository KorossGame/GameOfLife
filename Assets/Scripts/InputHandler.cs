using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private Camera camera;
    private int height, width;

    [SerializeField] private int sensitivity = 5000;

    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camera) camera = Camera.main;

        // Kill or Revive Node
        if (Input.GetMouseButtonDown(0))
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Node"))
                {
                    Node objectToInteractWith = hit.transform.GetComponent<Node>();

                    if (objectToInteractWith.isAlive)
                    {
                        objectToInteractWith.Die();
                    }
                    else
                    {
                        objectToInteractWith.Revive();
                    }
                }
            }
        }

        // Relative to y pos
        float relativeSpeedUP = 1 / (camera.transform.position.y);
       
        // Move by keys
        if (Input.GetKey(KeyCode.A))
        {
            camera.transform.position += -camera.transform.right * Time.deltaTime * sensitivity * relativeSpeedUP;
        }
        if (Input.GetKey(KeyCode.D))
        {
            camera.transform.position += camera.transform.right * Time.deltaTime * sensitivity * relativeSpeedUP;
        }
        if (Input.GetKey(KeyCode.W))
        {
            camera.transform.position += camera.transform.up * Time.deltaTime * sensitivity * relativeSpeedUP;
        }
        if (Input.GetKey(KeyCode.S))
        {
            camera.transform.position += -camera.transform.up * Time.deltaTime * sensitivity * relativeSpeedUP;
        }

        // Zoom in/out
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            camera.transform.position += camera.transform.forward * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        }
    }
}
