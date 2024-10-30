using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class opencv_UDP_photo : MonoBehaviour
{

    //timer
    float currentTime;
    float startingTime = 5;
    bool timer = false;

    float currTime;
    float waitTime = 5;

    [SerializeField] Text countdownText;
    //audio
    AudioSource audioSoure;

    //screen shot
    float photoCount = 1;
    bool scrshoot = false;
    bool shoot_available = true;

    //color Change
    Renderer matColor;
    string hexCode;
    string main_emo1;
    string main_emo2;
    string sub1_emo1;
    string sub2_emo1;


    // Color_change color;
    public GameObject game_object;
    Color color;

    // 1. Declare Variables
    Thread receiveThread; //1
    UdpClient client; //2
    int port; //3

    //public GameObject Player; //4
    //AudioSource jumpSound; //5
    bool shoot = false; //6
                        //shoot = UDP로 받아오는 값
    string main_emo;
    string sub1_emo;
    string sub2_emo;
    string trim_sub2_emo;
    // 2. Initialize variables
    void Start()
    {
        print("컬러칩 사진체험");
        audioSoure = GetComponent<AudioSource>();
        //timer
        currentTime = startingTime;
        currTime = waitTime;

        port = 8888; //1 
                     //shoot = false; //2 
                     //shoot = gameObject.GetComponent<AudioSource>(); //3

        InitUDP(); //4
        main_emo = GameObject.FindWithTag("MainData").GetComponent<MainData>().main_emo;
        sub1_emo = GameObject.FindWithTag("MainData").GetComponent<MainData>().sub1_emo;
        sub2_emo = GameObject.FindWithTag("MainData").GetComponent<MainData>().sub2_emo;
        //trim_sub2_emo = sub2_emo.Trim();
        //main_emo = "Dynamic";
        //sub1_emo = "Erotic";
        //sub2_emo = "Elegant";

        print(sub2_emo);


        List<string> Romantic = new List<string>();
        Romantic.Add("#fdeded");
        Romantic.Add("#eeced9");

        List<string> Elegant = new List<string>();
        Elegant.Add("#8f8794");
        Elegant.Add("#b299c3");

        List<string> Gorgeous = new List<string>();
        Gorgeous.Add("#d7be2d");
        Gorgeous.Add("#833765");

        List<string> Natural = new List<string>();
        Natural.Add("#f9f3d3");
        Natural.Add("#dfedb0");

        List<string> Casual = new List<string>();
        Casual.Add("#c2448d");
        Casual.Add("#f59701");

        List<string> Dynamic = new List<string>();
        Dynamic.Add("#cb003f");
        Dynamic.Add("#f3d900");
      
        //main
        if (main_emo == "Romantic")
        {
            main_emo1 = Romantic[0];
            main_emo2 = Romantic[1];
        }
        else if (main_emo == "Erotic")
        {
            main_emo1 = Gorgeous[0];
            main_emo2 = Gorgeous[1];
        }
        else if (main_emo == "Elegant")
        {
            main_emo1 = Elegant[0];
            main_emo2 = Elegant[1];
        }
        else if (main_emo == "Natural")
        {
            main_emo1 = Natural[0];
            main_emo2 = Natural[1];
        }
        else if (main_emo == "Casual")
        {
            main_emo1 = Casual[0];
            main_emo2 = Casual[1];
        }
        else if (main_emo == "Dynamic")
        {
            main_emo1 = Dynamic[0];
            main_emo2 = Dynamic[1];
        }
        //sub1

        if (sub1_emo == "Romantic")
        {
            sub1_emo1 = Romantic[0];
        }
        else if (sub1_emo == "Elegant")
        {
            sub1_emo1 = Elegant[0];
        }
        else if (sub1_emo == "Erotic")
        {
            sub1_emo1 = Gorgeous[0];
        }
        else if (sub1_emo == "Natural")
        {
            sub1_emo1 = Natural[0];
        }
        else if (sub1_emo == "Casual")
        {
            sub1_emo1 = Casual[0];
        }
        else if (sub1_emo == "Dynamic")
        {
            sub1_emo1 = Dynamic[0];
        }

        //sub2
        
        if (sub2_emo == "Romantic")
        {
            sub2_emo1 = Romantic[0];
        }
        else if (sub2_emo == "Elegant")
        {
            sub2_emo1 = Elegant[0];
        }
        else if (sub2_emo == "Erotic")
        {
            sub2_emo1 = Gorgeous[0];
        }
        else if (sub2_emo == "Natural")
        {
            sub2_emo1 = Natural[0];
        }
        else if (sub2_emo == "Casual")
        {
            sub2_emo1 = Casual[0];
        }
        else if (sub2_emo == "Dynamic")
        {
            sub2_emo1 = Dynamic[0];
        }
        print("sub2" + sub2_emo1 + "color");
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
    private void ReceiveData()
    {
        client = new UdpClient(port); //1
        while (true) //2
        {
            try
            {

                IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("0.0.0.0"), port); //3
                byte[] data = client.Receive(ref anyIP); //4

                string text = Encoding.UTF8.GetString(data); //5
                                                             // print(">> " + text);

                Debug.Log(shoot);
                if (text == "shoot")
                {
                    shoot = true; //6
                    print("shoot");
                }
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
        


        //임시로 해둠 udp통신으로 true받아와야함
        if (Input.GetKeyDown(KeyCode.S) && shoot_available == true)
        {
            shoot = true;
            photoshoot();
        }
        //////////////
        if (shoot == true)
        {
            photoshoot();
        }

        //color change
        bgColor();

    }

    private void photoshoot()
    {
        timer = true;
        if (timer == true)
        {
            //
            shoot_available = false;

            countdownText.gameObject.SetActive(true);
            Debug.Log("Timer Start");

            currentTime -= 1 * Time.deltaTime;
            countdownText.text = currentTime.ToString("0");

            if (currentTime <= 0)
            {
                //countdownText.text = currentTime.ToString("SMILE");
                countdownText.gameObject.SetActive(false);

                scrshoot = true;
                if (scrshoot == true)
                {

                    //
                    shoot_available = true;
                    audioSoure.Play();
                    string screenshotname;
                    //임시로 ID 지정해둠 나중에 아이디 받아올예정
                    //string id = "Haerim";
                    string personal_id = GameObject.FindWithTag("MainData").GetComponent<MainData>().personal_id;
                    screenshotname = personal_id + photoCount + ".png";
                    if (!Directory.Exists(Application.dataPath + "/screenshot"))   // 폴더 없으면 생성
                    {
                        Directory.CreateDirectory(Application.dataPath + "/screenshot");
                        // Debug.Log("created");
                    }
                    ScreenCapture.CaptureScreenshot(Application.dataPath + "/screenshot/" + screenshotname);
                    Debug.Log(Application.dataPath);
                    photoCount++;

                    scrshoot = false;
                    if (photoCount >= 5)
                    {
                        Debug.Log("사진촬영 체험이 종료되었습니다. 관리자분께서는 업데이트를 해주시기 바랍니다.");
                        SceneManager.LoadScene("photo_loading");
                        photoCount = 1;
                    }
                }
                currentTime = startingTime;
                timer = false;
                shoot = false;
            }

        }

    }


    private void bgColor()
    {
        matColor = game_object.GetComponent<Renderer>();

        if (photoCount == 1)
        {
            hexCode = main_emo1;
            print(hexCode);
        }
        else if (photoCount == 2)
        {
            hexCode = main_emo2;
            print(hexCode);
        }
        else if (photoCount == 3)
        {
            hexCode = sub1_emo1;
            print(hexCode);
        }
        else if (photoCount == 4)
        {
            hexCode = sub2_emo1;
            print(hexCode);
        }
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            // print(color);
            matColor.material.color = color;
            //matColor.material.color = new Color(r_color / 255f, g_color / 255f, b_color / 255f);
            //Debug.Log(true);
        }
    }

}