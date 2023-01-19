using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuControl : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        this.GameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (input.getKeyDown("Escape"))
        {
            this.GameObject.SetActive(true);
            
        }
    }
}
