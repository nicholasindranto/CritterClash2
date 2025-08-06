using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string enemyName;
    public float attackDelay;
    public float secondUntilDone;
    private float cooldownAttack = 1.5f; // nanti mengikuti gamemanager
    public bool isPlayerInRange = false; // kalau player di jangkauan attack
    public bool isAttacking = false; // kalau enemy lagi attack atau tidak
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyChasing enemyChasing;
    private float lastDirectionX;
    private float lastDirectionY;

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
        else StopCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true; // lagi attack

        // ambil arahnya dulu
        Vector2 dir = (enemyChasing.player.position - transform.position).normalized;

        // update lastdirection
        lastDirectionX = dir.x;
        lastDirectionY = dir.y;

        if (dir.x != 0) transform.localScale = new Vector3((Mathf.Sign(dir.x)) * 1, 1, 1);
        else if (lastDirectionX != 0) transform.localScale = new Vector3((Mathf.Sign(lastDirectionX)) * 1, 1, 1);

        anim.SetFloat("DirectionX", lastDirectionX);
        anim.SetFloat("DirectionY", lastDirectionY);

        if (enemyName == "Coffin") anim.SetBool("isAttack", isAttacking);
        else anim.SetBool("isShooting", isAttacking);

        yield return new WaitForSeconds(attackDelay);

        if (enemyName == "Coffin")
        {
            
        }
        else
        {
            
        }

        yield return new WaitForSeconds(secondUntilDone);

        isAttacking = false; // udah gak attack

        if (enemyName == "Coffin") anim.SetBool("isAttack", isAttacking);
        else anim.SetBool("isShooting", isAttacking);
    }
}
