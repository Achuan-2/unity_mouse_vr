using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uduino;
using UnityEngine.SceneManagement;

public class KeyboardControl : MonoBehaviour
{

    public GameObject Mouse;
    public TrackControl TC;
    public float moveSpeed=10f;

    void Start()
    {

    }


    void Update()
    {

        // enter ↑/↓ to move position;
        SimulateMove();

        // enter R go back to start point;
        if (Input.GetKeyDown(KeyCode.R))
        {
            TC.GotoStartPoint(TC.startPoint);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

    }


    void SimulateMove()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Mouse.transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Mouse.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0, Space.World);
        }
        else
        {
            return;
        }
    }



}
