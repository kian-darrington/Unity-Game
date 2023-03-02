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
    }

    private void Start()
    {
        StartCoroutine("DelaySettingStuffInactive");
    }

    IEnumerator DelaySettingStuffInactive()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }

    #endregion
}
