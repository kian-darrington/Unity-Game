using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int SoundOn;
    public int MusicOn;
    public Vector3 playerPosition;

    public GameData()
    {
        this.SoundOn = 1;
        this.MusicOn = 1;
        this.playerPosition = Vector3.zero;
        // may need to set this^^ as zeros and set coordinates in menu.
    }
}
