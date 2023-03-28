using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject background1;
    public GameObject background2;
    public GameObject foreground1;
    public GameObject foreground2;

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
        background1 = FindWithTag("Background 1");
        background2 = FindWithTag("Background 2");
        foreground1 = FindWithTag("Foreground 1");
        foreground2 = FindWithTag("Foreground 2");
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
                    background1.backgroundLeft();
                    background2.backgroundLeft();
                    foreground1.foregroundLeft();
                    foreground2.foregroundLeft();
                }
                else
                {
                    tempPos.x = player.position.x + xLimit;
                    background1.backgroundRight();
                    background2.backgroundRight();
                    foreground1.foregroundRight();
                    foreground2.foregroundRight();
                }
            }
            if (Mathf.Abs(tempPos.y - player.position.y) > yLimit)
            {
                if (tempPos.y - player.position.y < 0)
                {
                    tempPos.y = player.position.y - yLimit;
                    background1.backgroundDown();
                    background2.backgroundDown();
                    foreground1.foregroundDown();
                    foreground2.foregroundDown();
                }
                else
                {
                    tempPos.y = player.position.y + yLimit;
                    background1.backgroundUp();
                    background2.backgroundUp();
                    foreground1.foregroundUp();
                    foreground2.foregroundUp();
                }
            }
        }
        
        transform.position = tempPos;
    }
}
