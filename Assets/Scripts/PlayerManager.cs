using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GameCharacter
{
    public int id;
    public string username;


    public float currentLifes;
    public float maxLifes;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;
        currentLifes = maxLifes;
    }
    

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }
}
