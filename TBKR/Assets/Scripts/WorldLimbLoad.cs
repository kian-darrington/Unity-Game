using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLimbLoad : MonoBehaviour, IDataPersistance
{
    public GameObject[] Limbs;

    public void LoadData(GameData data)
    {
        for (int i = 0; i < data.worldLimbs.Length; i++)
        {
            this.Limbs[i] = data.worldLimbs[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < data.worldLimbs.Length; i++)
        {
            data.worldLimbs[i] = this.Limbs[i];
        }
    }
}
