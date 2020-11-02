using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private GameObject _playerObj;
    private Vector3 _rotationInput;
    private Vector2 _movementInput;
    private Transform _transform;
    private Rigidbody2D _rb;
    private float _projCooldown = 1f;
    private float _timeStamp = 0;
    public float _speed = 10f;
    public GameObject _projectilePrefab;


    void Start()
    {
        _playerObj = (GameObject)GameObject.FindGameObjectWithTag("PlayerSelf");
        _transform = _playerObj.GetComponent<Transform>();
        _rb = _playerObj.GetComponent<Rigidbody2D>();

    }

    // aplicar Behaviors
    void Update()
    {
        GameBehaviors.RotateToMousePos(_transform);

        // Mouse esquerdo
        if (Input.GetButtonDown("Fire1"))
        {
            // se pode atirar (tempo já passou do cooldown)
            if (_timeStamp <= Time.time)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                Vector3 projDir = mousePos - _transform.position;
                GameBehaviors.Shoot(_projectilePrefab, projDir, _transform);
                _timeStamp = Time.time + _projCooldown; // tempo de agora + cooldown
            }
        }

    }

    void FixedUpdate()
    {
        GameBehaviors.MovePlayer(_rb, GetMovement(), _speed);
    }

    // retornar input de movimento
    public Vector2 GetMovement()
    {
        _movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        return _movementInput.normalized;
    }


}
