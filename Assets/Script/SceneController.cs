using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Level1()
    {
        GameManager.Instance.level = 1;
        SceneManager.LoadScene("level1");
    }

    public void Level2()
    {
        GameManager.Instance.level = 2;
        SceneManager.LoadScene("level2");
    }

    public void Level3()
    {
        GameManager.Instance.level = 3;
        SceneManager.LoadScene("level3");
    }

    public void Home()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void LevelChoose()
    {
        SceneManager.LoadScene("levelchoose");
    }

    public void Settings()
    {
        SceneManager.LoadScene("settings");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene("howtoplay");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
