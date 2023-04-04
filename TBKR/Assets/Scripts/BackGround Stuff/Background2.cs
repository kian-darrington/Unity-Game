using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2 : MonoBehaviour
{

    Transform camera;

    Vector3 tempPos;

    public float MoveValx;
    public float MoveValy;
    public float Jumpval;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("MainCamera").transform;
    }

    public void backgroundLeft()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x -= MoveValx;
            if (((200 + camera.position.x) - (200 + tempPos.x)) > Jumpval)
            {

                tempPos.x += (Jumpval * 2);
            }
        }

        transform.position = tempPos;
    }

    public void backgroundRight()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x += MoveValx;
            if (((200 + tempPos.x) - (200 + camera.position.x)) > Jumpval)
            {

                tempPos.x -= (Jumpval * 2);
            }
        }

        transform.position = tempPos;
    }

    public void backgroundDown()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y -= MoveValy;
        }

        transform.position = tempPos;
    }

    public void backgroundUp()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y += MoveValy;
        }

        transform.position = tempPos;
    }
}
