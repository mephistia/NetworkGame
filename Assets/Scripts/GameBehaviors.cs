using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviors 
{

    // Movimentar com inputs do jogador
    public static void MovePlayer(Rigidbody2D _rb2d, Vector2 _movement, float _speed)
    {
        _rb2d.velocity = _movement * _speed;
    }


    // Rotacionar para o mouse (usa cam principal)
    public static void RotateToMousePos(Transform _transform)
    {
        float camDistance = Camera.main.transform.position.y - _transform.position.y;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDistance);
        Vector3 objPos = Camera.main.WorldToScreenPoint(_transform.position);
        float angle = Mathf.Atan2(mousePos.x - objPos.x, mousePos.y - objPos.y) * Mathf.Rad2Deg;

        _transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle));
    }
}
