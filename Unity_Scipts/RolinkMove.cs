using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Uduino;
using UnityEngine.UI;

public class TrackFordward : MonoBehaviour
{
    public float MoveF = 150.001f;
    public GameObject Mouse;
    public float lastmsg;
    public float msg;
    public int started;
    public float moveSpeed = 1f;
    void Start()
    {

        Mouse.transform.position = new Vector3(0, 3, 0);
        UduinoManager.Instance.OnValueReceived += ValueReceived;
    }

    void Update()
    {

    }
    void ValueReceived(string value, UduinoDevice board)
    {
        if (started == 0)
        {
            lastmsg = float.Parse(value);
            started = 1;
        }
        else
        {
            //Debug.Log(value);
            msg = float.Parse(value);

            float forward = msg - lastmsg;
            //Debug.Log(MoveF);
            Mouse.transform.Translate(moveSpeed * forward*Time.deltaTime, 0, 0, Space.World);

            lastmsg = msg;

        }

    }

}
