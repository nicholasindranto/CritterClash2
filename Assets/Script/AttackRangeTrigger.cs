using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRangeTrigger : MonoBehaviour
{
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.SetPlayerInRange(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyAttack.SetPlayerInRange(false);
        }
    }
}
