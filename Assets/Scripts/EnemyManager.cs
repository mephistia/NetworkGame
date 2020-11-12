using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EnemyType
{
    melee,
    ranger,
    tank,
    sorcerer
}

public class EnemyManager : GameCharacter
{
    public int id;
    public EnemyType type;

    public void Initialize(int _id, EnemyType _type)
    {
        id = _id;

        type = _type;

        switch (type)
        {
            // atualizar com balanceamento!
            case EnemyType.melee:
                maxHealth = 100f;
                break;
            case EnemyType.ranger:
            case EnemyType.sorcerer:
                maxHealth = 50f;
                break;
            case EnemyType.tank:
                maxHealth = 150f;
                break;
            default:
                break;
        }

        health = maxHealth;
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        healthBarBG.sizeDelta = healthBar.sizeDelta;
    }

    public override void SetHealth(float _health)
    {
        health = _health;

        // reduz o tamanho em x
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);

        if (health <= 0f)
        {
            GameManager.enemies.Remove(id);
            // animação?
            //...
            Destroy(gameObject);
        }
    }
}
