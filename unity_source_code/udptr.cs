using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class udptr : MonoBehaviour
{
    public double num;
    // 1. Declare Variables
    Thread receiveThread; //1
    UdpClient client; //2
    int port; //3

    //public GameObject Player; //4
    //AudioSource jumpSound; //5
    bool jump; //6

    // 2. Initialize variables
    void Start()
    {
        port = 7777; //1 
        jump = false; //2 
                      //jumpSound = gameObject.GetComponent<AudioSource>(); //3

        InitUDP(); //4
    }

    // 3. InitUDP
    private void InitUDP()
    {
        print("UDP Initialized");

        receiveThread = new Thread(new ThreadStart(ReceiveData)); //1 
        receiveThread.IsBackground = true; //2
        receiveThread.Start(); //3

    }

    // 4. Receive Data
    public void ReceiveData()
    {
        client = new UdpClient(port); //1
        while (true) //2
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
                byte[] data = client.Receive(ref anyIP); //4

                string text = Encoding.UTF8.GetString(data); //5
                num = Convert.ToDouble(text);
                //print(">> " + text);
                //print(">>"+ num);
                jump = true; //6
                //Debug.Log("connected");
            }
            catch (Exception e)
            {
                print(e.ToString()); //7
            }
        }
    }


    // 6. Check for variable value, and make the Player Jump!
    void Update()
    {
        if (jump == true)
        {
            jump = false;
           // Debug.Log("Success");
        }
    }
}