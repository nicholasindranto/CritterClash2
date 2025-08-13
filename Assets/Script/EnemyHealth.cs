using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private bool deathStatus;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = 3f;
        deathStatus = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (!deathStatus)
        {
            anim.SetTrigger("isHit");
        }
        if (health <= 0)
        {
            deathStatus = true;
            GameManager.Instance.enemySpawned--;
            // add score berdasarkan level
            AddScore(GameManager.Instance.level);
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        anim.SetTrigger("isHit");
        yield return new WaitForSeconds(0.17f);
        Destroy(gameObject);
    }

    private void AddScore(int value)
    {
        // get score from player prefs
        int score = PlayerPrefs.GetInt("score", 0);

        // add the score
        score += value;

        // set to player prefs
        PlayerPrefs.SetInt("score", score);

        // save it
        PlayerPrefs.Save();
    }
}
