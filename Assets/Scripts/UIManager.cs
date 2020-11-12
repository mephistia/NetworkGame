using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject startMenu;

    public InputField usernameField;

    public Image fire1, fire2, F, E;

    public PlayerController player;

    private void Awake()
    {
        // script único
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object...");
            Destroy(this);
        }
    }

    private void Start()
    {
        fire1.fillAmount = fire2.fillAmount = F.fillAmount = E.fillAmount = 1f;
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ConnectToServer();
    }

    private void Update()
    {
        // tempo em que foi pressionado
        float timePressed = player.timeStamps[0] - player.skillsCooldown[0];

        fire1.fillAmount = (timePressed > 0) ? Mathf.InverseLerp(timePressed, player.timeStamps[0], Time.time) : 1;   
    }
}
