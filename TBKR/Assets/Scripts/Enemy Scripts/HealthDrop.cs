using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour
{
    public List<Sprite> Animations;

    public float Speed = 1f;

    SpriteRenderer mySprite;

    Transform PlayerTransform;

    Rigidbody2D myBody;

    float TimePassage = 0f;

    int CurrentFrame = 0;

    public float FrameRate = 0.1f;

    bool SeePlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myBody = GetComponent<Rigidbody2D>();
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        myBody.velocity = new Vector2(Random.Range(-Speed, Speed), Random.Range(0, Speed));
    }

    // Update is called once per frame
    void Update()
    {
        TimePassage += Time.deltaTime;

        if (SeePlayer)
        {
            Vector3 Point1 = transform.position;
            Vector3 Point2 = PlayerTransform.position;
            float xDistance = Point2.x - Point1.x;
            float yDistance = Point2.y - Point1.y;
            if (xDistance < 0)
                yDistance = -yDistance;
                
            float xSpeed = 0, ySpeed = 0;
            if (yDistance != 0)
                xSpeed = Mathf.Atan(xDistance / yDistance);
            else
                xSpeed = 1f;
            if (xDistance != 0)
                ySpeed = Mathf.Atan(yDistance / xDistance);
            else
                ySpeed = 1f;
            if (yDistance < 0)
                xSpeed *= -1f;
            myBody.velocity = new Vector2(xSpeed * Speed, ySpeed * Speed);
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
        }

    }
}
