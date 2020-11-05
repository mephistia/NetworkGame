using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public SpriteRenderer model;

    private void Start()
    {
        health = maxHealth;
    }
    public void SetHealth(float _health)
    {
        health = _health;
        if (health <= 0f)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // animação?
        // ...

        model.enabled = false;
    }
}
