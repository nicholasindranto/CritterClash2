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
            StartCoroutine(DeathCoroutine());
        }
    }

    IEnumerator DeathCoroutine()
    {
        anim.SetTrigger("isHit");
        yield return new WaitForSeconds(0.17f);
        Destroy(gameObject);
    }
}
