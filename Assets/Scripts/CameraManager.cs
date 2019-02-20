using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera room1;
    public Camera room2;
    public Camera room3;

    [SerializeField]
    private float lowerBound;
    [SerializeField]
    private float upperBound;

    private Camera activeCam;
    private CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        activeCam = room1;
        activeCam.enabled = true;
        room2.enabled = false;
        room3.enabled = false;
        cc = FindObjectOfType<CharacterController>();
    }
    
    void LateUpdate()
    {
        Debug.Log(activeCam);
        if (activeCam.Equals(room1))
        {
            // blank, this camera is static
        }
        else if (activeCam.Equals(room2))
        {
            // Track with axis
            Vector3 tempVec3 = new Vector3
            {
                z = cc.transform.position.z,
                y = activeCam.transform.position.y,
                x = activeCam.transform.position.x
            };
            if (tempVec3.z < lowerBound)
            {
                tempVec3.z = lowerBound;
            } 
            else if (tempVec3.z > upperBound)
            {
                tempVec3.z = upperBound;
            }
            activeCam.transform.position = tempVec3;
        }
        else if (activeCam.Equals(room3))
        {
            // LookAt character
            activeCam.transform.LookAt(cc.transform);
        }
    }

    public void EnterRoom1()
    {
        activeCam.enabled = false;
        activeCam = room1;
        activeCam.enabled = true;
    }

    public void EnterRoom2()
    {
        activeCam.enabled = false;
        activeCam = room2;
        activeCam.enabled = true;
    }

    public void EnterRoom3()
    {
        activeCam.enabled = false;
        activeCam = room3;
        activeCam.enabled = true;
    }
}
