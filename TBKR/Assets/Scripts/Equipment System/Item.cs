using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public float speed = 0f;
    public float maxVelocity = 0f;
    public float jumpForce = 0f;
    public float attackDamage = 0f;
    public float wallJumpForce = 0f;
    public float airTimeBuffer = 0f;
    public int limbID = 0;
}
