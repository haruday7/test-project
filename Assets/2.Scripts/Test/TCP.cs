using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEditor.PackageManager;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class TCP : MonoBehaviour
{
    Socket socket;
    string serverIP = "127.0.0.1";
    int port = 8080;
    byte[] receivedBuffer;
    bool socketReady = false;

    // Start is called before the first frame update
    void Start()
    {
        CheckReceive();
        StartCoroutine(Check());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Check()
    {
        int num = 0;
        while (this.socketReady)
        {
            //송신
            byte[] data = Encoding.UTF8.GetBytes("" + num);
            this.socket.Send(BitConverter.GetBytes(data.Length));
            this.socket.Send(data);
            Debug.Log("송신" + num++);

            //수신
            data = new byte[4];
            this.socket.Receive(data, data.Length, SocketFlags.None);
            Array.Reverse(data);
            data = new byte[BitConverter.ToInt32(data, 0)];
            this.socket.Receive(data, data.Length, SocketFlags.None);
            string msg = Encoding.Default.GetString(data);
            Debug.Log("수신" + msg);
            yield return null;
        }
    }
    private void CheckReceive()
    {
        if (this.socketReady) return;
        try
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.Connect(new IPEndPoint(IPAddress.Parse(this.serverIP), this.port));
            if (this.socket.Connected)
            {
                //this.stream = this.client.GetStream();
                Debug.Log("Connect Success");
                this.socketReady = true;
            }

        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }
}
