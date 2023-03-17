using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : EnemyInfo
{
    public List<Sprite> Animations;

    float TimePassage = 0f;

    public float FrameRate = 0.1f;

    int CurrentFrame = 0;

    Rigidbody2D myBody;

    SpriteRenderer mySprite;

    Transform PlayerTransform;

    float MaxSpeed = 4f, Speed = 0.15f;

    bool SeePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        Health = 30;
        mySprite = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (SeePlayer)
        {
            TimePassage += Time.deltaTime;
            if (transform.position.x - PlayerTransform.position.x > 0 && myBody.velocity.x > -MaxSpeed)
            {
                myBody.AddForce(new Vector2(-Speed, 0f), ForceMode2D.Impulse);
                mySprite.flipX = true;
            }
            else if (transform.position.x - PlayerTransform.position.x < 0 && myBody.velocity.x < MaxSpeed)
            {
                myBody.AddForce(new Vector2(Speed, 0f), ForceMode2D.Impulse);
                mySprite.flipX = false;
            }

            if (TimePassage > FrameRate)
            {
                CurrentFrame++;
                if (CurrentFrame == Animations.Capacity)
                    CurrentFrame = 0;
                mySprite.sprite = Animations[CurrentFrame];
                TimePassage = 0f;
            }
        }
        else
        {
            if (myBody.velocity.x != 0)
                myBody.velocity = new Vector2(0, myBody.velocity.y);
            if (mySprite.sprite != Animations[0])
                mySprite.sprite = Animations[0];
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SeePlayer = true;
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SeePlayer = false;
            myBody.velocity = new Vector2(0, 0);
        }

    }
}
