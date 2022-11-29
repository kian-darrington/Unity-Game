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

    public Item item = null;

    private void OnEnable()
    {
        if (item != null)
        {
            icon.sprite = item.icon;
        }
    }

    private void Update()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }
}
