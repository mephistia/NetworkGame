using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float timeStamp = 0f;
    private float shootCooldown = 1f;
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (timeStamp <= Time.fixedTime)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                Vector3 projDir = mousePos - transform.position;
                timeStamp = Time.fixedTime + shootCooldown; // tempo de agora + cooldown
                Debug.Log("Shot made");
                ClientSend.PlayerShoot(projDir);

            }
            else
            {
                Debug.Log("Shot is in cooldown!");
            }

        }
    }

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        if (Application.isFocused)
        {
            bool[] _inputs = new bool[]
            {
                Input.GetKey(KeyCode.W),
                Input.GetKey(KeyCode.A),
                Input.GetKey(KeyCode.S),
                Input.GetKey(KeyCode.D)
            };

            float camDistance = Camera.main.transform.position.y - transform.position.y;
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDistance);
            Vector3 objPos = Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(mousePos.x - objPos.x, mousePos.y - objPos.y) * Mathf.Rad2Deg;

            ClientSend.PlayerRotation(angle);

            ClientSend.PlayerMovement(_inputs);
        }
        
    }
}
