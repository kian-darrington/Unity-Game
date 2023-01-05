using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int SoundOn;
    public int MusicOn;

    public GameData()
    {
        this.SoundOn = 1;
        this.MusicOn = 1;
    }
}
