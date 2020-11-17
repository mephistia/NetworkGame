using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float[] timeStamps = new float[4];
    public float[] skillsCooldown = new float[4];
    public int energyCount = 0;

    private float pressedTime = 0f;

    private void Start()
    {
        if (Client.instance.myId == 1)
        {
            // cooldowns de atirador
            skillsCooldown[0] = .3f;
            skillsCooldown[1] = 5f;
            skillsCooldown[2] = 8f;
            skillsCooldown[3] = 12.5f;
        }
        else
        {
            // cooldowns de tank
            skillsCooldown[0] = 1.2f;
            skillsCooldown[1] = 5f;
            skillsCooldown[2] = 8f;
            skillsCooldown[3] = 12.5f;
        }

    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (timeStamps[0] <= Time.fixedTime)
            {
                Vector3 fire1Dir = CalculateShotDirection();
                timeStamps[0] = Time.fixedTime + skillsCooldown[0]; // tempo de agora + cooldown

                ClientSend.PlayerShoot(fire1Dir);
            }
        }

        if (Input.GetButtonDown("Fire2") && Client.instance.myId == 1)
        {            
            if (timeStamps[1] <= Time.fixedTime)
            {                
                timeStamps[1] = Time.fixedTime + skillsCooldown[1]; // tempo de agora + cooldown
                Vector3 fire2Dir = CalculateShotDirection();
               
                ClientSend.PlayerShootSkill(fire2Dir);
            }            
        }

        // tank pressiona
        if (Input.GetButton("Fire2") && Client.instance.myId == 2)
        {
            pressedTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Fire2") && Client.instance.myId == 2)
        {
            if (timeStamps[1] <= Time.fixedTime)
            {
                timeStamps[1] = Time.fixedTime + skillsCooldown[1]; // tempo de agora + cooldown
                Vector3 fire2Dir = CalculateShotDirection();

                ClientSend.PlayerTankSkill(fire2Dir, pressedTime);
            }

            pressedTime = 0f;
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

    private Vector3 CalculateShotDirection()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        mousePos.y += 1;
        return mousePos - transform.position;        
    }
}
