using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private AudioSource audioSource;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        // play bgm nya
        PlayRandomBGM();
    }

    // Update is called once per frame
    void Update()
    {
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
        int randMusic = Random.Range(0, bgmPlaylist.Length);

        // play lagunya
        audioSource.clip = bgmPlaylist[randMusic];
        audioSource.Play();
    }
}
