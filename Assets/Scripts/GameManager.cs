using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, PlayerManager> players = new Dictionary<int, PlayerManager>();
    public static Dictionary<int, ProjectileManager> projectiles = new Dictionary<int, ProjectileManager>();
    public static Dictionary<int, EnemyManager> enemies = new Dictionary<int, EnemyManager>();
    public static Dictionary<int, EnergyManager> energies = new Dictionary<int, EnergyManager>();

    public GameObject localPlayerPrefab;
    public GameObject playerPrefab;
    public GameObject projectilePrefab;
    public GameObject projectileSkillPrefab;
    public GameObject projectileTankPrefab;
    public GameObject enemyPrefab;
    public GameObject energyPrefab;
    public GameObject statue;
    public GameObject tankAttackEffect;
    public static StatueManager statueManager;

    private GameObject virtualCamera;

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
        statueManager = statue.GetComponent<StatueManager>();
    }

    public void SpawnPlayer(int _id, string _username, Vector3 _position, Quaternion _rotation, float _maxHealth)
    {
        GameObject _player;
        if (_id == Client.instance.myId)
        {
            _player = Instantiate(localPlayerPrefab, _position, _rotation);
            virtualCamera = GameObject.FindGameObjectWithTag("VirtualCamera");
            virtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = _player.transform;
            UIManager.instance.player = _player.GetComponent<PlayerController>();
        }
        else
        {
            _player = Instantiate(playerPrefab, _position, _rotation);
        }

        _player.GetComponent<PlayerManager>().Initialize(_id, _username, _maxHealth);
        _player.GetComponentInChildren<TextMeshPro>().SetText(_username);
        players.Add(_id, _player.GetComponent<PlayerManager>());
    }


    public void SpawnProjectile(int _id, Vector3 _position)
    {
        GameObject _projectile = Instantiate(projectilePrefab, _position, Quaternion.identity);
        _projectile.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectile.GetComponent<ProjectileManager>());
    }

    public void SpawnEnemy(int _id, Vector3 _position, int _type)
    {
        GameObject _enemy = Instantiate(enemyPrefab, _position, Quaternion.identity);
        EnemyType _enemyType = (EnemyType)_type;

        _enemy.GetComponent<EnemyManager>().Initialize(_id, _enemyType);
        enemies.Add(_id, _enemy.GetComponent<EnemyManager>());
    }

    public void SpawnEnergy(int _id, Vector3 _position, float _timeToDestroy)
    {
        GameObject _energy = Instantiate(energyPrefab, _position, Quaternion.identity);
        _energy.GetComponent<EnergyManager>().Initialize(_id, _timeToDestroy);
        energies.Add(_id, _energy.GetComponent<EnergyManager>());
    }

    public void SpawnProjectileSkill(int _id, Vector3 _position)
    {
        GameObject _projectileSkill = Instantiate(projectileSkillPrefab, _position, Quaternion.identity);
        _projectileSkill.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectileSkill.GetComponent<ProjectileManager>());
    }

    public void SpawnProjectileTank(int _id, Vector3 _position)
    {
        GameObject _projectileTank = Instantiate(projectileTankPrefab, _position, Quaternion.identity);
        _projectileTank.GetComponent<ProjectileManager>().Initialize(_id);
        projectiles.Add(_id, _projectileTank.GetComponent<ProjectileManager>());
    }

    public void SpawnTankAttack(Vector3 _position)
    {
        Instantiate(tankAttackEffect, _position, Quaternion.identity);
    }
}
