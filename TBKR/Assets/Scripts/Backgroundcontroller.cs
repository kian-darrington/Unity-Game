using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour
{

    Transform camera;

    Vector3 tempPos;

 
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindWithTag("Main Camera").transform;
    }

    public void backgroundLeft()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x -= 1;
            if ((camera.position.x - tempPos.x) > 1495)
            {
                tempPos.x += (1495 * 2);
            }
        }

        transform.position = tempPos;
    }

    public void backgroundRight()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.x += 1;
            if ((tempPos.x - camera.position.x) > 1495)
            {
                tempPos.x -= (1495 * 2);
            }
        }

        transform.position = tempPos;
    }

    public void backgroundDown()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y -= 1;
        }

        transform.position = tempPos;
    }

    public void backgroundUp()
    {
        tempPos = transform.position;

        if (camera != null)
        {
            tempPos.y += 1;
        }

        transform.position = tempPos;
    }
}
