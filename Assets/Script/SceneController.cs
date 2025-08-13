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

    public void Credit()
    {
        SetAllUIInvisible();
        SceneManager.LoadScene("credit");
    }

    public void Level1()
    {
        SetAllUIInvisible();
        GameManager.Instance.level = 1;
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("level1");
    }

    public void Level2()
    {
        SetAllUIInvisible();
        GameManager.Instance.level = 2;
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("level2");
    }

    public void Level3()
    {
        SetAllUIInvisible();
        GameManager.Instance.level = 3;
        PlayerPrefs.SetInt("score", 0);
        SceneManager.LoadScene("level3");
    }

    public void Home()
    {
        SetAllUIInvisible();
        SceneManager.LoadScene("mainmenu");
    }

    public void LevelChoose()
    {
        SetAllUIInvisible();
        SceneManager.LoadScene("lvlchoose");
    }

    public void Settings()
    {
        SetAllUIInvisible();
        SceneManager.LoadScene("settings");
    }

    public void HowToPlay()
    {
        SetAllUIInvisible();
        SceneManager.LoadScene("howtoplay");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void SetAllUIInvisible()
    {
        // reset semuanya
        GameManager.Instance.uiWin.SetActive(false);
        GameManager.Instance.uiLose.SetActive(false);
        GameManager.Instance.countdownUI.SetActive(false);
        GameManager.Instance.scoreUI.SetActive(false);
        GameManager.Instance.StopAllCoroutines();
        GameManager.Instance.buttonGameplay.SetActive(false);
    }
}
