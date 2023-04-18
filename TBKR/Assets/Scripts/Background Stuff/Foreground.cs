using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreground : MonoBehaviour
{

    Transform camera;

    Transform ForG;

    Vector3 tempPos;
    Vector3 Another;

    public float MoveValx;
    public float MoveValy;
    public float Jumpval;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").transform;
        ForG = GameObject.FindWithTag("Foreground 2").transform;
        Another = ForG.position;
        Debug.Log(Another.x - transform.position.x);
    }

    public void foregroundLeft()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x -= MoveValx;
            //Debug.Log((Mathf.Abs(tempPos.x) - Mathf.Abs(camera.position.x)));
            if (((200 + camera.position.x) - (200 + tempPos.x)) > Jumpval)
            {

                tempPos.x += (Jumpval * 2);
                Debug.Log("Snapped foreground left");
            }
        }

        transform.position = tempPos;
    }

    public void foregroundRight()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x += MoveValx;
            //Debug.Log((Mathf.Abs(tempPos.x) - Mathf.Abs(camera.position.x)));
            if (((200 + tempPos.x) - (200 + camera.position.x)) > Jumpval)
            {

                tempPos.x -= (Jumpval * 2);
                Debug.Log("Snapped foreground right");
            }
        }

        transform.position = tempPos;
    }

    public void foregroundDown()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y -= MoveValy;
        }

        transform.position = tempPos;
    }

    public void foregroundUp()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y += MoveValy;
        }

        transform.position = tempPos;
    }
}