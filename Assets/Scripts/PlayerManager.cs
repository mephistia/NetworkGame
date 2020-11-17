using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : GameCharacter
{
    public int id;
    public string username;


    public float currentLifes;
    public float maxLifes;

    private Vector3 velocity = Vector3.zero;
    private Vector3 newPosition = Vector3.zero;
    private Quaternion newRotation = Quaternion.identity;

    public Sprite tankSprite;

    public void Initialize(int _id, string _username, float _maxHealth)
    {
        id = _id;
        username = _username;
        maxHealth = _maxHealth;
        currentLifes = maxLifes;
        health = maxHealth;

        // se for jogador tank
        if (id == 2)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = tankSprite;
        }

        // corrigir tamanho da barra
        healthBar.sizeDelta = new Vector2(health / maxHealth * 100, healthBar.sizeDelta.y);
        healthBarBG.sizeDelta = healthBar.sizeDelta;      
    }

    public void Respawn()
    {
        model.enabled = true;
        SetHealth(maxHealth);
    }

    // para inicializar
    public void SetPosition(Vector3 _position)
    {
        newPosition = _position;
        transform.position = _position;
    }
    public void SetRotation(Quaternion _rotation)
    {
        newRotation = _rotation;
        transform.rotation = _rotation;
    }


    public void NextPosition(Vector3 _position)
    {
        newPosition = _position;
    }

    public void NextRotation(Quaternion _rotation)
    {
        newRotation = _rotation;
    }

    private void Start()
    {
        SetPosition(transform.position);
        SetRotation(transform.rotation);
    }
    private void FixedUpdate()
    {
        transform.rotation = newRotation;

        // suavizar até a posição recebida do server
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, Time.fixedDeltaTime);
    }
}
