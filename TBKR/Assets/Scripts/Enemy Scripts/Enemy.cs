using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/EnemyStats")]
public class Enemy : ScriptableObject
{
    new public string name = "New Enemy";
    public float knockBack = 10f;
    public float attackDamage = 0f;
}

