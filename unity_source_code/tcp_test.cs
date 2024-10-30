//메인 카메라에 스크립트 적용

using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.Text;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class tcp_test : MonoBehaviour
{
    TcpClient client;
    string serverIP = "127.0.0.1";
    int port = 8000;

    byte[] receivedBuffer;
    StreamReader reader;
    bool socketReady = false;
    NetworkStream stream;

    Renderer matColor;
    // Color_change color;
    public GameObject game_object;
    Color color;
    string hexCode;

    public string color1;
    public string color2;
    public string color3;
    public string color4;
    public string color5;

    public string color6;
    public string color7;
    public string color8;
    public string color9;
    public string color10;

    public string dunehex;

    public float rate_main;
    public float rate_sub1;
    public float rate_sub2;

    public string main_emo;
    public string sub1_emo;
    public string sub2_emo;
    public bool mainDataPlay = false;
    bool sended_to_python = false;
    public float dynamic_per;
    public float elegant_per;
    public float romantic_per;
    public float casual_per;
    public float erotic_per;
    public float natural_per;
    bool changeScene = false;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        //CheckReceive();
        currentTime = 5;

    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
        if (changeScene)
        {
            currentTime -= 1 * Time.deltaTime;
            if (currentTime <= 0)
            {
                SceneManager.LoadScene("title_scene");
            }
        }
        if (socketReady)
        {


            if (stream.DataAvailable)
            {
                receivedBuffer = new byte[1000];
                stream.Read(receivedBuffer, 0, receivedBuffer.Length); // stream에 있던 바이트배열 내려서 새로 선언한 바이트배열에 넣기
                string msg = Encoding.UTF8.GetString(receivedBuffer, 0, receivedBuffer.Length); // byte[] to string
                Debug.Log(msg);
                //문자열 분할하기
                string[] split_msg = msg.Split(" ");

                color1 = split_msg[1];
                color2 = split_msg[2];
                color3 = split_msg[3];
                color4 = split_msg[4];

                color5 = split_msg[5];
                color6 = split_msg[6];
                color7 = split_msg[7];

                color8 = split_msg[8];
                color9 = split_msg[9];
                color10 = split_msg[10];

                dunehex = split_msg[5];


                //matColor = game_object.GetComponent<Renderer>();


                /*
                List<string> hex_colors = new List<string>();
                for (int i = 0; i < split_msg.Length; i++)
                {
                    hex_colors.Add(split_msg[i]);
                    print(hex_colors);
                }

                for (int i = 0; i < hex_colors.Count; i++) 
                { 
                    Console.WriteLine(hex_colors[i]); 
                }
                */


                for (int i = 11; i < split_msg.Length; i++)
                {
                    if (i == 11)
                    {
                        rate_main = float.Parse(split_msg[i]);
                        //print("유사율 main" + rate_main);
                    }
                    else if (i == 12)
                    {
                        rate_main = float.Parse(split_msg[i]);
                        //print("유사율 sub1" + rate_main);
                    }
                    else if (i == 13)
                    {
                        rate_main = float.Parse(split_msg[i]);
                        //print("유사율 sub2" + rate_main);
                    }
                    else if (i == 14)
                    {
                        main_emo = split_msg[i];
                        //print("감정 main" + main_emo);
                    }
                    else if (i == 15)
                    {
                        sub1_emo = split_msg[i];
                        //print("감정 sub1" + sub1_emo);
                    }
                    else if (i == 16)
                    {
                        sub2_emo = split_msg[i];
                        //print("감정 sub2" + sub2_emo);
                    }
                    else if (i == 17)
                    {
                        dynamic_per = float.Parse(split_msg[i]);
                    }
                    else if (i == 18)
                    {
                        elegant_per = float.Parse(split_msg[i]);
                    }
                    else if (i == 19)
                    {
                        romantic_per = float.Parse(split_msg[i]);
                    }
                    else if (i == 20)
                    {
                        casual_per = float.Parse(split_msg[i]);
                    }
                    else if (i == 21)
                    {
                        erotic_per = float.Parse(split_msg[i]);
                    }
                    else if (i == 22)
                    {
                        natural_per = float.Parse(split_msg[i]);
                    }
                }
                GameObject.Find("TCPData").GetComponent<MainData>().data_Load = true;


            }

        }
    }
    void color_split()
    {

    }

    public void CheckReceive()
    {
        
        if (socketReady) return;
        try
        {
            client = new TcpClient(serverIP, port);

            if (client.Connected)
            {
                stream = client.GetStream();
                Debug.Log("TCP Initialized");
                socketReady = true;
            }
            if (sended_to_python == false)
            {
                string personal_id = GameObject.Find("IDInput").GetComponent<UI_InputField>().InputId;
                print(personal_id);
                int byteCount = Encoding.UTF8.GetByteCount(personal_id);
                byte[] sendBuffer = new byte[byteCount];
                sendBuffer = Encoding.UTF8.GetBytes(personal_id);
                stream.Write(sendBuffer, 0, sendBuffer.Length);
                sended_to_python = true;
            }

        }
        catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
        changeScene = true;

    }

    void OnApplicationQuit()
    {
        CloseSocket();
    }

    void CloseSocket()
    {
        if (!socketReady) return;

        reader.Close();
        client.Close();
        socketReady = false;
    }

}