using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.D)
        };

        float camDistance = Camera.main.transform.position.y - GameManager.players[Client.instance.myId].transform.position.y;
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDistance);
        Vector3 objPos = Camera.main.WorldToScreenPoint(GameManager.players[Client.instance.myId].transform.position);
        float angle = Mathf.Atan2(mousePos.x - objPos.x, mousePos.y - objPos.y) * Mathf.Rad2Deg;

        ClientSend.PlayerRotation(angle);

        ClientSend.PlayerMovement(_inputs);
    }

    


}
