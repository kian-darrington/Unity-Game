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
        if (instance != null)
        {
            Debug.LogWarning("More than one istance of Inventory found!");
            return;
        }
        instance = this;
        instance.enabled = false;
    }

    #endregion

    public Image icon;

    public Item item;

    private void OnEnable()
    {
        if (!item)
        {
            icon.sprite = item.icon;
        }
    }

    private void Update()
    {
        myTransform.position = Input.mousePosition;
    }
}
