using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfo : MonoBehaviour
{
    float CurrentPlayerDamage = 0;

    public float Health = 20;
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
                Destroy(gameObject);
        }
    }
    void GetDamage(float Damage)
    {
        CurrentPlayerDamage = Damage;
    }
}
