using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoundaryDetect : MonoBehaviour
{
    public TrackControl TC;
    public MouseLickingDetect MLD;
    public float boundaryBack;
    public float boundaryFront;
    public float punishlockTime = 5f;
    public float mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = TC.mousePosition;
        //Debug.Log(mousePosition);
        DetectBoundary(mousePosition);
    }

    void DetectBoundary(float mousePosition)
    {
        if (mousePosition < boundaryBack || mousePosition > boundaryFront)
        {
            // out-of-bounds: Execute punishment
            Debug.Log("Out of Boundary! "+ mousePosition);
            MLD.Punishement();
        }
    }
}
