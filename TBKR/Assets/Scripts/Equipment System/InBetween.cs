using UnityEngine;
using UnityEngine.UI;

public class InBetween : MonoBehaviour
{
    Transform myTransform;

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

    private void OnEnable()
    {
        if (item != null)
        {
            mySprite.sprite = item.icon;
            mySprite.color = new Color(255f, 255f, 255f, 255f);
        }
    }

    private void OnDisable()
    {
        mySprite.sprite = null;
        mySprite.color = new Color(255f, 255f, 255f, 0f);
    }

    private void Update()
    {
        transform.position = new Vector3(Input.mousePosition.x / 32f, Input.mousePosition.y / 32f, Input.mousePosition.z / 32f);
    }
}
