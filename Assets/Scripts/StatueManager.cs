using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueManager : GameCharacter
{

    private void Start()
    {
        maxHealth = 100f;
        health = maxHealth;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        healthBarBG.sizeDelta = healthBar.sizeDelta;
    }

    public override void SetHealth(float _health)
    {
        health = _health;

        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        Debug.Log($"Statue health: {health}");

        if (health <= 0f)
        {
            // animação?
            //...
            Debug.Log("Game Over!!!!");
        }
    }
}
