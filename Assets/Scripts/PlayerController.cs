using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float[] timeStamps = new float[4];
    public float[] skillsCooldown = new float[4];

    public bool[] isCoolingDown = new bool[4];

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            isCoolingDown[i] = false;
        }

        skillsCooldown[0] = 3f;
        skillsCooldown[1] = 2f;
        skillsCooldown[2] = 3f;
        skillsCooldown[3] = 2f;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (timeStamps[0] <= Time.fixedTime)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                mousePos.y += 1;
                Vector3 projDir = mousePos - transform.position;
                timeStamps[0] = Time.fixedTime + skillsCooldown[0]; // tempo de agora + cooldown
                isCoolingDown[0] = true;

                ClientSend.PlayerShoot(projDir);

            }
            else
            {
                Debug.Log("Shot is on cooldown!");
            }

        }

        for (int i = 0; i < 4; i++)
        {
            if (timeStamps[i] <= Time.fixedTime)
            {
                isCoolingDown[i] = false;
                if (i == 0)
                    Debug.Log($"Skill {i} is available");
            }
            else
            {
                if (i == 0)
                    Debug.Log($"Skill {i} is cooling down.");
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
