using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quotes : MonoBehaviour
{
    public GameObject Body;
    private bool climb;
    List<string> quote = new List<string> { "Tis' but a flesh wound.", "Ni!", "I'm not worthy!", "Bring out yer' dead!", "*Coconut clopping sounds*", "Violence inherent in the system!", "1... 2... 5... 3!", "Your mother was a hampster!", "European or African Swallow?", "It's a vicous rodent!" };
    

    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, 10);
        TextMeshProUGUI mText = Body.GetComponent<TextMeshProUGUI>();
        mText.text = quote[r];
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
