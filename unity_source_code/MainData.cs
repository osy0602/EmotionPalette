using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainData : MonoBehaviour
{
    public static MainData Instance;
    public string MainEmotion = "Dynamic";
    public GameObject game_object;
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
    public bool data_Load = false;
    public float dynamic_per;
    public float elegant_per;
    public float romantic_per;
    public float casual_per;
    public float erotic_per;
    public float natural_per;
    public string personal_id;
    public AudioSource audioSource;
    public AudioClip bgm;
    public bool audioIn = false;
    public bool audiostop = false;
    private void Awake()
    {

        print("start");
       
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void Start(){
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;
    }
    // Start is called before the first frame update
    public void Update()
    {
        
        if(data_Load)
        {
            savedData();

        }
        if(audioIn){
            audioSource.Play();
            audioIn= false;
        }
        if(audiostop){
            audioSource.Stop();
            audiostop= false;
        }
        /*
        if(Input.GetKeyDown(KeyCode.E))
        {
            print("Pressed E");
            Application.Quit();
        }
        */
        
    }

    private void savedData()
    {
        personal_id = GameObject.Find("IDInput").GetComponent<UI_InputField>().InputId;
        color1 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color1;
        color2 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color2;
        color3 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color3;
        color4 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color4;
        color5 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color5;
        color6 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color6;
        color7 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color7;
        color8 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color8;
        color9 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color9;
        color10 = GameObject.Find("Main Camera").GetComponent<tcp_test>().color10;
        dunehex = GameObject.Find("Main Camera").GetComponent<tcp_test>().dunehex;
        rate_main = GameObject.Find("Main Camera").GetComponent<tcp_test>().rate_main;
        rate_sub1 = GameObject.Find("Main Camera").GetComponent<tcp_test>().rate_sub1;
        rate_sub2 = GameObject.Find("Main Camera").GetComponent<tcp_test>().rate_sub2;
        main_emo = GameObject.Find("Main Camera").GetComponent<tcp_test>().main_emo;
        sub1_emo = GameObject.Find("Main Camera").GetComponent<tcp_test>().sub1_emo;
        sub2_emo = GameObject.Find("Main Camera").GetComponent<tcp_test>().sub2_emo;
        dynamic_per = GameObject.Find("Main Camera").GetComponent<tcp_test>().dynamic_per;
        elegant_per = GameObject.Find("Main Camera").GetComponent<tcp_test>().elegant_per;
        romantic_per = GameObject.Find("Main Camera").GetComponent<tcp_test>().romantic_per;
        casual_per = GameObject.Find("Main Camera").GetComponent<tcp_test>().casual_per;
        erotic_per = GameObject.Find("Main Camera").GetComponent<tcp_test>().erotic_per;
        natural_per = GameObject.Find("Main Camera").GetComponent<tcp_test>().natural_per;
        //print(sub2_emo);
        data_Load = false;
        print("ID saved DATA : " + personal_id);
    }
}
