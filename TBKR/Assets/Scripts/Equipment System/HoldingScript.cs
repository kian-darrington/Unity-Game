using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldingScript : MonoBehaviour
{
    #region Singleton
    public static HoldingScript instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one istance of Inventory found!");
            return;
        }
        instance = this;

        this.gameObject.SetActive(false);
    }

    #endregion
}
