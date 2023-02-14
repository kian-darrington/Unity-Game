using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int SoundOn;
    public int MusicOn;
    public Vector3 playerPosition;
    /*
    public int[] invSlotNum;
    public string[] itemName;
    public Sprite[] itemIcon;
    public float[] itemSpeed;
    public float[] itemMaxVelocity;
    public float[] itemJumpForce;
    public float[] itemAttackDamage;
    public float[] itemWallJumpForce;
    public float[] itemAirTimeBuffer;
    public float[] itemThrowingDistance;
    */
    public Item[] itemsaves;

    public GameData()
    {
        this.SoundOn = 1;
        this.MusicOn = 1;
        this.playerPosition = Vector3.zero;
        /*
        this.invSlotNum = new int[12];
        this.itemName = new string[12];
        this.itemIcon = new Sprite[12];
        this.itemSpeed = new float[12];
        this.itemMaxVelocity = new float[12];
        this.itemJumpForce = new float[12];
        this.itemAttackDamage = new float[12];
        this.itemWallJumpForce = new float[12];
        this.itemAirTimeBuffer = new float[12];
        this.itemThrowingDistance = new float[12];
        */
        this.itemsaves = new Item[12];
    }
}
