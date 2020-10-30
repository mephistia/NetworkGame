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

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;

        // enviar o ack
        ClientSend.WelcomeReceived();


        // udp (usando mesma porta tcp):
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UDPTest(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP. Contains message: {_msg}");
        ClientSend.UDPTestReceived();
    }
}
