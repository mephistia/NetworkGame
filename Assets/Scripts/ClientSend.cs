﻿using GameServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets

    // verificar que recebeu o pacote (ack)
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }


    public static void PlayerMovement(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
        {
            _packet.Write(_inputs.Length);

            foreach (bool _input in _inputs)
            {
                _packet.Write(_input);
            }

            SendUDPData(_packet);
        }
    }

    public static void PlayerRotation(float _angle)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerRotation))
        {
            _packet.Write(_angle);
            SendUDPData(_packet);
        }
    }

    public static void PlayerShoot(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerShootSkill(Vector3 _facing)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShootSkill))
        {
            _packet.Write(_facing);

            SendTCPData(_packet);
        }
    }

    public static void PlayerTankSkill(Vector3 _facing, float _pressedTime)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerShootSkill))
        {
            _packet.Write(_facing);
            _packet.Write(_pressedTime);

            SendTCPData(_packet);
        }
    }

    public static void AskCombine(PlayerController _player)
    {
        using (Packet _packet = new Packet((int)ClientPackets.askCombine))
        {
            if (_player.TryGetComponent<PlayerManager>(out PlayerManager _playerM))
            {
                _packet.Write(_playerM.id);

                SendTCPData(_packet);
            }
        }
    }

    #endregion
}
