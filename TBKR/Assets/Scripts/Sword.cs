using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Rigidbody2D myBody;

    public float MultiplicationFactor = 1f;
    public float DespawnRate = 5f;

    float xVelocity = 0f, yVelocity = 0f;

    public Sword(float f1, float f2)
    {
        xVelocity = f1;
        yVelocity = f2;
    }

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();

        myBody.angularVelocity = (Random.value - 0.5f) * MultiplicationFactor;
        myBody.velocity = new Vector3(xVelocity, yVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            StartCoroutine("DespawnTime");
    }

    IEnumerator DespawnTime()
    {
        yield return new WaitForSeconds(DespawnRate);
        Destroy(this);
    }
}
