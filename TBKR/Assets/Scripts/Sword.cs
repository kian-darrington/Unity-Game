using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Rigidbody2D myBody;

    public float MultiplicationFactor = 10f;
    public float DespawnRate = 5f;
    public float SpinMax = 1300f, SpinMin = 400f;

    float xVelocity = 0f, yVelocity = 0f;

    public Sword(float f1, float f2)
    {
        xVelocity = f1;
        yVelocity = f2;
    }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        myBody.angularVelocity = Random.Range(-1f, 1f) * MultiplicationFactor;
        myBody.velocity = new Vector3(xVelocity, yVelocity);
    }

    public void SetVelocity(float f1, float f2)
    {
        xVelocity = f1;
        yVelocity = f2;

        if (f1 < 0f)
            myBody.angularVelocity = Random.Range(400f, 1300f);
        else
            myBody.angularVelocity = Random.Range(-1300f,-400f);
        myBody.AddForce(new Vector2(xVelocity, yVelocity), ForceMode2D.Impulse);

        StartCoroutine(DespawnTime());
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(DespawnRate);
        Destroy(gameObject);
    }
}
