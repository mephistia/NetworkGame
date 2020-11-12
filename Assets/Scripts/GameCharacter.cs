using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public SpriteRenderer model;

    public RectTransform healthBar, healthBarBG;

    private void Start()
    {
        health = maxHealth;
    }
    public virtual void SetHealth(float _health)
    {
        health = _health;

        // reduz o tamanho em x
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);

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
