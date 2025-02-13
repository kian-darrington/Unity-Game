using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swallow : EnemyInfo
{
    public List<Sprite> Animations;

    public Vector3 Point1, Point2;

    public float FlightSpeed = 5f;

    public float FrameRate = 0.25f;

    float xSpeed = 0f, ySpeed = 0f;

    bool xSwitch = false;

    float TimePassage = 0f;

    int CurrentFrame = 0;

    Rigidbody2D myBody;

    SpriteRenderer mySprite;

    private void Start()
    {
        
        HealthOrbMax = 1;
        myBody = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        transform.position = Point1;

        float xDistance = Point2.x - Point1.x;
        float yDistance = Point2.y - Point1.y;
        if (xDistance < 0)
        {
            Vector3 temp = Point1;
            Point1 = Point2;
            Point2 = temp;
            xDistance = Point2.x - Point1.x;
            yDistance = Point2.y - Point1.y;
        }

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
        myBody.velocity = new Vector2(xSpeed * FlightSpeed, ySpeed * FlightSpeed);
    }
    

    // Update is called once per frame
    void Update()
    {
        TimePassage += Time.deltaTime;
        if (transform.position.x < Point1.x)
        {
            myBody.velocity = new Vector2(xSpeed * FlightSpeed, ySpeed * FlightSpeed);
            xSwitch = true;
            mySprite.flipX = xSwitch;
        }
        else if (transform.position.x > Point2.x)
        {
            myBody.velocity = new Vector2(-xSpeed * FlightSpeed, -ySpeed * FlightSpeed);
            xSwitch = false;
            mySprite.flipX = xSwitch;
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
}
