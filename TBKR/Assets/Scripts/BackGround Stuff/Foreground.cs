using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foreground : MonoBehaviour
{

    Transform camera;

    Vector3 tempPos;


    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("Main Camera").transform;
    }

    public void foregroundLeft()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x -= 2;
            if ((camera.position.x - tempPos.x) > 1495)
            {
                tempPos.x += (1495 * 2);
            }
        }

        transform.position = tempPos;
    }

    public void foregroundRight()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x += 2;
            if ((tempPos.x - camera.position.x) > 1495)
            {
                tempPos.x -= (1495 * 2);
            }
        }

        transform.position = tempPos;
    }

    public void foregroundDown()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y -= 2;
        }

        transform.position = tempPos;
    }

    public void foregroundUp()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y += 2;
        }

        transform.position = tempPos;
    }
}
