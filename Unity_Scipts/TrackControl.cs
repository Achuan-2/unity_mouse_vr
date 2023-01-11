using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Uduino;
using UnityEngine.UI;

public class TrackControl : MonoBehaviour
{
    public GameObject Mouse;
    public float startPoint =0f;
    public float trackMoveSpeed = 1f;
    public float mousePosition;
    public float lastmsg;
    public float msg;
    private int started;

    void Start()
    {

        GotoStartPoint(startPoint);
        UduinoManager.Instance.OnDataReceived += RotaryEncoderValueReceived;
    }

    void Update()
    {
        mousePosition = Mouse.transform.position.x;
    }

    void RotaryEncoderValueReceived(string value, UduinoDevice board)
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
            Mouse.transform.Translate(trackMoveSpeed * forward*Time.deltaTime, 0, 0, Space.World);

            lastmsg = msg;

        }

    }

    public void GotoStartPoint(float startPoint)
    {
        Vector3 pos = Mouse.transform.position;
        pos.x = startPoint;
        Mouse.transform.position = pos;

    }

}
