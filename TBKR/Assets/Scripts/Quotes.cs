using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quotes : MonoBehaviour
{
    public GameObject Body;
    private bool climb;
    private int time;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (transform.position.y >= -0.40)
        {
            climb = false;
        }
       else if (transform.position.y <= -0.70)
        {
            climb = true;
        }


       if (climb == true)
        {
            pos.y = transform.position.y + 0.001f;
            transform.position = pos;
        }
        else if (climb == false)
        {
            pos.y = transform.position.y - 0.001f;
            transform.position = pos;
        }
    }
}
