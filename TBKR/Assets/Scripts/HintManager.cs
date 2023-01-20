using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public GameObject Text1;

    public GameObject Text2;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colliding");
        if (name == "HintTrigger1")
        {
            Text1 = GameObject.Find("Hint1");
            Text1.SetActive(true);
            Debug.Log("with 1");
        }
        else if (name == "HintTrigger2")
        {
            Text2 = GameObject.Find("Hint2");
            Text2.SetActive(true);
            Debug.Log("with 2");
        }
    }

}
