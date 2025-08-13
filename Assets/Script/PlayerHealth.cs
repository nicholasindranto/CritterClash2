using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public float hp = 10f;
    private Animator anim;
    public bool deathStatus;
    // event driven OOP untuk gamelose condition
    public static event Action OnPlayerDied;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        deathStatus = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerHit(float dmgAmount)
    {
        if (deathStatus || GameManager.Instance.gameEnded) return;
        hp -= dmgAmount;
        // kurangi scorenya
        SubtractScore(1);
        if (hp <= 0)
        {
            deathStatus = true;
            // beri tahu gamemanager bahwa player died
            OnPlayerDied?.Invoke();
            StartCoroutine(DeathCoroutine());
        }
        else anim.SetTrigger("isHit");
    }

    IEnumerator DeathCoroutine()
    {
        Debug.Log("inside coroute death");
        anim.SetTrigger("isDeath"); // animasi
        yield return new WaitForSeconds(1f);
        Debug.Log("after coroutine death");
        Destroy(gameObject);
    }

    private void SubtractScore(int value)
    {
        int score = PlayerPrefs.GetInt("score", 0);
        score -= value;
        // pastikan gak negatif
        score = Mathf.Max(0, score);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.Save();
    }
}
