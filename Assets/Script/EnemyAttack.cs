using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string enemyName;
    public float attackDelay;
    public bool isPlayerInRange = false; // kalau player di jangkauan attack
    public bool isAttacking = false; // kalau enemy lagi attack atau tidak
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyChasing enemyChasing;

    public void SetPlayerInRange(bool value)
    {
        isPlayerInRange = value;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyChasing = GetComponent<EnemyChasing>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // kalau gak ada enemynya atau lagi attack y gak dijalanin
        if (enemyChasing.player == null || isAttacking) return;

        // kalau dalam jarak serang maka attack
        if (isPlayerInRange) StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true; // lagi attack

        anim.Play("attack"); // play animasinya

        yield return new WaitForSeconds(attackDelay);
    }
}
