using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour
{

    Transform player;

    Vector3 tempPos;

    [SerializeField]
    float yLimit = 1f;

    [SerializeField]
    float xLimit = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        tempPos = transform.position;
        if (player != null)
        {
            if (Mathf.Abs(tempPos.x - player.position.x) > xLimit)
            {
                if (tempPos.x - player.position.x < 0)
                {
                    tempPos.x = player.position.x - xLimit;
                    xLimit--;
                }
                else
                {
                    tempPos.x = player.position.x + xLimit;
                    xLimit++;
                }
            }
            if (Mathf.Abs(tempPos.y - player.position.y) > yLimit)
            {
                if (tempPos.y - player.position.y < 0)
                {
                    tempPos.y = player.position.y - yLimit;
                    yLimit--;
                }
                else
                {
                    tempPos.y = player.position.y + yLimit;
                    yLimit++;
                }
            }
        }

        transform.position = tempPos;
    }
}
