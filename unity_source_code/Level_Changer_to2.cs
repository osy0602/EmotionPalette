using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level_Changer_to2 : MonoBehaviour
{
    public Animator animator;
    // Update is called once per frame
    float currentTime;
    float startingTime = 60;
    public void Start()
    {

        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S) | currentTime <= 0)
        {
            FadeToLevel(1);
            Invoke("SceneChange", 1);
        }
    }
    public void FadeToLevel(int levelIndex)
    {
        animator.SetTrigger("Fade_Out");
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("exp2_intro");
    }
}
