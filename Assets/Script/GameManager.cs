using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // singleton
    public static GameManager Instance;
    // reference ke levelnya
    public int level = 3;
    public GameObject[] enemyPrefab;
    public int maxEnemySpawn = 0;
    public int enemySpawned = 0;
    public float cooldownAttack = 0f;
    [SerializeField] private AudioClip[] bgmPlaylist;
    // berapa lama harus bertahan biar game win
    [SerializeField] private float surviveTime = 120f; // 2 menit
    // reference ke ui gamewin sama game lose
    public GameObject uiWin;
    public GameObject uiLose;
    [SerializeField] private float timer;
    // gamenya end??
    public bool gameEnded = false;
    private AudioSource audioSource;
    public TextMeshProUGUI countdownText;
    public GameObject countdownUI;
    public TextMeshProUGUI scoreText;
    public GameObject scoreUI;
    private int lastScore;
    public GameObject buttonGameplay;
    private int lastMusicIndex = -1; // reference ke music yang play sekarang

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
        audioSource.volume = PlayerPrefs.GetFloat("audio", 1f);

        lastScore = PlayerPrefs.GetInt("score", 0);
        scoreText.text = "Score\n" + lastScore;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // cari dulu scene yang ada levelnya
        if (scene.name.StartsWith("level"))
        {
            // reset semuanya
            StopAllCoroutines();
            timer = surviveTime;
            gameEnded = false;
            countdownUI.SetActive(true);
            scoreUI.SetActive(true);
            buttonGameplay.SetActive(true);
            PlayerPrefs.SetInt("score", 0);
            lastScore = PlayerPrefs.GetInt("score", 0);
            scoreText.text = "Score\n" + lastScore;
            enemySpawned = 0;

            StartCoroutine(StartCountdownCoroutine());
        }
        else
        {
            // countdownnya di stop
            StopAllCoroutines();
        }
    }

    private void OnEnable()
    {
        // mendengarkan ke player health, playernya mati kaga
        PlayerHealth.OnPlayerDied += HandleLoseCondition;

        // biar bisa njalanin coroutinenya
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // gak mau ndengerin lagi
        PlayerHealth.OnPlayerDied -= HandleLoseCondition;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        // play bgm nya
        PlayRandomBGM();

        // set timernya
        timer = surviveTime;

        // set all ui win and lose and also countdownui to be false
        uiWin.SetActive(false);
        uiLose.SetActive(false);
        countdownUI.SetActive(false);
        scoreUI.SetActive(false);
        buttonGameplay.SetActive(false);

        // set gameended nya
        gameEnded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded) return;

        // set up maximum jumlah enemy dan cooldown attacknya biar gak langsung ngejar
        if (level == 1 && maxEnemySpawn != 5 && cooldownAttack != 1.5f)
        {
            cooldownAttack = 1.5f;
            maxEnemySpawn = 5;
        }
        else if (level == 2 && maxEnemySpawn != 10 && cooldownAttack != 1f)
        {
            cooldownAttack = 1f;
            maxEnemySpawn = 10;
        }
        else if (level == 3 && maxEnemySpawn != 15 && cooldownAttack != 0.5f)
        {
            cooldownAttack = 0.5f;
            maxEnemySpawn = 15;
        }

        // update the volume each time from player prefs
        audioSource.volume = PlayerPrefs.GetFloat("audio", 1f);

        // kalau bgm nya selesai maka random lagunya
        if (!audioSource.isPlaying) PlayRandomBGM();
    }

    private void PlayRandomBGM()
    {
        if (bgmPlaylist.Length == 0) return;

        // pilih lagu acak
        int randMusic;

        do
        {
            randMusic = Random.Range(0, bgmPlaylist.Length);
        } while (randMusic == lastMusicIndex && bgmPlaylist.Length > 1);

        // play lagunya
        lastMusicIndex = randMusic;
        audioSource.clip = bgmPlaylist[randMusic];
        audioSource.Play();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void HandleWinCondition()
    {
        gameEnded = true;
        uiWin.SetActive(true);
        uiLose.SetActive(false);
    }

    private void HandleLoseCondition()
    {
        gameEnded = true;
        uiWin.SetActive(false);
        uiLose.SetActive(true);
    }

    public IEnumerator StartCountdownCoroutine()
    {
        while (timer > 0 && !gameEnded)
        {
            // countdown
            timer--;
            UpdateTimerUI(timer);
            if (timer <= 0)
            {
                HandleWinCondition(); // menang
                yield break;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void UpdateTimerUI(float time)
    {
        // pastikan time tidak negatif
        time = Mathf.Max(0, time);

        // set menit dan detiknya
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        countdownText.text = $"{minutes:00}:{seconds:00}";

        countdownText.color = time <= 10f ? Color.red : Color.white;
    }

    private void FixedUpdate()
    {
        if (lastScore != PlayerPrefs.GetInt("score", 0))
        {
            int score = PlayerPrefs.GetInt("score", 0);
            lastScore = score;
            scoreText.text = "Score\n" + lastScore;
        }
    }
}
