using UnityEngine;
using UnityEngine.UI;

public class InBetween : MonoBehaviour
{
    Transform myTransform;

    public Transform CameraTransform;

    #region Singleton
    public static InBetween instance;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        mySprite = GetComponent<SpriteRenderer>();
        if (instance != null)
        {
            Debug.LogWarning("More than one istance of Inventory found!");
            return;
        }
        instance = this;
        instance.enabled = false;
    }

    #endregion

    SpriteRenderer mySprite;

    public Item item = null;

    public TMPro.TextMeshProUGUI text;

    private void OnEnable()
    {
        if (item != null)
        {
            mySprite.sprite = item.icon;
            mySprite.color = new Color(255f, 255f, 255f, 255f);
            myTransform.localScale = new Vector3(64f, 64f, 64f);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));

            text.text = item.name + "\n\n" + "Arm Stats:\n\n";
            text.text += "Wall Jump " + item.wallJumpForce + "\nAttack Damage " + item.attackDamage + "\n\nLeg Stats\n\n";
            text.text += "Jump " + item.jumpForce + "\nSpeed " + item.maxVelocity + "\nAcceleration " + item.speed;
        }
        else
            Debug.Log("Unable to locate sprite");
    }

    public void UpdateSprite()
    {
        mySprite.sprite = item.icon;
        text.text = item.name + "\n\n" + "Arm Stats:\n\n";
        text.text += "Wall Jump " + item.wallJumpForce + "\nAttack Damage " + item.attackDamage + "\n\nLeg Stats\n\n";
        text.text += "Jump " + item.jumpForce + "\nSpeed " + item.maxVelocity + "\nAcceleration " + item.speed;

    }
    private void OnDisable()
    {
        mySprite.sprite = null;
        mySprite.color = new Color(255f, 255f, 255f, 0f);
        text.text = "";
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 0.5f, 1f));
    }
}
