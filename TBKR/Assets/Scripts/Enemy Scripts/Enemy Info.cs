using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    float CurrentPlayerDamage = 0;
    public int HealthOrbMin = 0, HealthOrbMax = 2;
    public float Health = 20;
    public GameObject HealthDropRef;
    GameObject newthingy;
    // Start is called before the first frame update
    void Awake()
    {
        Player.DamageChangeInfo += GetDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sword"))
        {
            Destroy(collision.gameObject);
            Health -= CurrentPlayerDamage;
            if (Health <= 0)
            {
                Destroy(gameObject);
                for (int i = 0; i < Random.Range(HealthOrbMin, HealthOrbMax + 1); i++)
                {
                    newthingy = Instantiate(HealthDropRef);
                    newthingy.transform.position = transform.position;
                }
            }
        }
    }
    void GetDamage(float Damage)
    {
        CurrentPlayerDamage = Damage;
    }
}
