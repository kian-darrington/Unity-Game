using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dopess : MonoBehaviour
{
    SpriteRenderer mySprite;

    public List<Sprite> animations;

    float TimePassage = 0f, RandTimeSet , Glitchingframe;

    public float FrameRate = 0.1f;

    bool isGlitching = false;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        RandTimeSet = Random.Range(0.3f, 2f);
        Glitchingframe = Random.Range(0.05f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        TimePassage += Time.deltaTime;

        if (TimePassage > RandTimeSet && !isGlitching)
        {
            isGlitching = true;
            TimePassage = 0f;
            mySprite.sprite = animations[Random.Range(0, 3)];
            RandTimeSet = Random.Range(0.3f, 2f);
        }
        else if (TimePassage > Glitchingframe && isGlitching)
        {
            mySprite.sprite = animations[Random.Range(0,3)];
            Glitchingframe = Random.Range(0.05f, 0.2f);
            TimePassage = 0f;
            if (mySprite.sprite == animations[0])
            {
                isGlitching = false;
            }
        }
    }
}
