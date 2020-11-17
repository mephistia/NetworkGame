using GameServer;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    // ler o pacote na mesma ordem
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        //Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        // enviar o ack
        ClientSend.WelcomeReceived();


        // udp (usando mesma porta tcp):
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation =_packet.ReadQuaternion();
        float _maxHealth = _packet.ReadFloat();

        GameManager.instance.SpawnPlayer(_id, _username, _position, _rotation, _maxHealth);
    }

    public static void PlayerPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
            _player.NextPosition(_position);
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        if (GameManager.players.TryGetValue(_id, out PlayerManager _player))
            _player.NextRotation(_rotation);
    }


    public static void PlayerDisconnected(Packet _packet)
    {
        int _id = _packet.ReadInt();

        Destroy(GameManager.players[_id].gameObject);
        GameManager.players.Remove(_id);
    }

    public static void PlayerHealth(Packet _packet)
    {
        int _id = _packet.ReadInt();
        float _health = _packet.ReadFloat();

        GameManager.players[_id].SetHealth(_health);
    }

    public static void PlayerRespawned(Packet _packet)
    {
        int _id = _packet.ReadInt();

        GameManager.players[_id].Respawn();
    }

    public static void SpawnProjectile(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        //int _byPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectile(_projectileId, _position);
    }

    public static void ProjectilePosition(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.projectiles.TryGetValue(_projectileId, out ProjectileManager _projectile))
            _projectile.transform.position = _position;
    }

    public static void ProjectileDamaged(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        Vector3 _direction = _packet.ReadVector3();
        string _tag = _packet.ReadString();
        if (GameManager.projectiles.TryGetValue(_projectileId, out ProjectileManager _projectile))
            _projectile.DamageVisualFeedback(_position,_direction,_tag);
    }

    // INIMIGOS
    public static void SpawnEnemy(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _type = _packet.ReadInt();

        GameManager.instance.SpawnEnemy(_enemyId, _position, _type);
    }

    public static void EnemyPosition(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();

        if (GameManager.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
            _enemy.transform.position = _position;
    }

    public static void EnemyHealth(Packet _packet)
    {
        int _enemyId = _packet.ReadInt();
        float _health = _packet.ReadFloat();
        if (GameManager.enemies.TryGetValue(_enemyId, out EnemyManager _enemy))
            _enemy.SetHealth(_health);
    }

    public static void StatueHealth(Packet _packet)
    {
        float _health = _packet.ReadFloat();

        GameManager.statueManager.SetHealth(_health);
    }


    public static void SpawnEnergy(Packet _packet)
    {
        int _energyID = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        float _timeToDestroy = _packet.ReadFloat();

        GameManager.instance.SpawnEnergy(_energyID, _position, _timeToDestroy);
    }

    public static void DespawnEnergy(Packet _packet)
    {
        int _energyID = _packet.ReadInt();

        if (GameManager.energies.TryGetValue(_energyID, out EnergyManager _energy))
            _energy.Despawn();
    }

    public static void EnergyPickedUp(Packet _packet)
    {
        int _playerID = _packet.ReadInt();

        // confirmar
        if (_playerID == Client.instance.myId)
            UIManager.instance.AddEnergyUI();
    }

    public static void SpawnProjectileSkill(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _byPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectileSkill(_projectileId, _position);
    }

    public static void SpawnProjectileTank(Packet _packet)
    {
        int _projectileId = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        int _byPlayer = _packet.ReadInt();

        GameManager.instance.SpawnProjectileTank(_projectileId, _position);
    }


    public static void TankAttacked(Packet _packet)
    {
        Vector3 _tankAttackPosition = _packet.ReadVector3();

        GameManager.instance.SpawnTankAttack(_tankAttackPosition);
    }
}
