using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int SoundOn;
    public int MusicOn;
    public Vector3 playerPosition;
    public Item[] itemsaves;
    public bool[] worldLimbs;

    public GameData()
    {
        this.SoundOn = 1;
        this.MusicOn = 1;
        this.playerPosition = Vector3.zero;
        this.itemsaves = new Item[12];
        this.worldLimbs = new bool[12];
    }
}
