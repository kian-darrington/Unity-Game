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


    public Item item = null;

    private void Update()
    {
        transform.position = new Vector3(0f, 0f, 0f);
    }
}
