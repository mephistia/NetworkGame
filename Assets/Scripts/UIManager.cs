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
    public Image bgFire1, bgFire2;
    public Sprite tankFire1, tankFire2;
    public PlayerController player;
    public GameObject energyImgPrefab;
    public GameObject energyLayout;

    private bool changedSprites = false;
    
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



        if (Client.instance.myId == 2)
        {
            fire1.sprite = tankFire1;
            fire2.sprite = tankFire2;

            bgFire1.sprite = fire1.sprite;
            bgFire2.sprite = fire2.sprite;
            Debug.Log("Changed sprites");
        }

    }

    private void Update()
    {

        if (Client.instance.myId == 2 && !changedSprites)
        {
            fire1.sprite = tankFire1;
            fire2.sprite = tankFire2;

            bgFire1.sprite = fire1.sprite;
            bgFire2.sprite = fire2.sprite;
            changedSprites = true;
            Debug.Log("Changed sprites");
        }

        fire1.fillAmount = CalcFill(CalcTime(0), 0);
        fire2.fillAmount = CalcFill(CalcTime(1), 1); 

        // se tem
        if (player.energyCount > 0)
        {
            F.fillAmount = CalcFill(CalcTime(2), 2);
            E.fillAmount = CalcFill(CalcTime(3), 3);
        }
        else
        {
            F.fillAmount = 0;
            E.fillAmount = 0;
        }
    }

    public void AddEnergyUI()
    {
        player.energyCount++;
        Instantiate(energyImgPrefab, energyLayout.transform);
    }

    // preencher com quanto falta pro tempo de agora chegar no tempo que termina o cooldown
    private float CalcFill(float _time, int _skillNumber)
    {
        return (_time > 0) ? Mathf.InverseLerp(_time, player.timeStamps[_skillNumber], Time.time) : 1;
    }

    // calcular tempo que foi pressionado
    private float CalcTime(int _skillNumber)
    {
        return player.timeStamps[_skillNumber] - player.skillsCooldown[_skillNumber];
    }
}
