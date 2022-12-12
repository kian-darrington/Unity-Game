using UnityEngine;
using UnityEngine.UI;

public class InBetween : MonoBehaviour
{
    Transform myTransform;

    public Transform CameraTransform;

    #region Singleton
    public static InBetween instance;

    public float divisionFactor = 64f;

    public float offsetx = 0f;
    public float offsety = 0f;

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
            myTransform.localScale = new Vector3(64f, 64f, 64f);
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        }
        else
            Debug.Log("Unable to locate sprite");
    }

    private void OnDisable()
    {
        mySprite.sprite = null;
        mySprite.color = new Color(255f, 255f, 255f, 0f);
    }

    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 0.5f, 1f));
    }
}
