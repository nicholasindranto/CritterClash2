using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string enemyName;
    public float attackDelay;
    private float cooldownAttack = 1.5f; // nanti mengikuti gamemanager
    public bool isPlayerInRange = false; // kalau player di jangkauan attack
    public bool isAttacking = false; // kalau enemy lagi attack atau tidak
    private Rigidbody2D rb;
    private Animator anim;
    private EnemyChasing enemyChasing;
    private string lastDirection;

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
        string direction = enemyChasing.GetDirection(dir);

        string animName = "";

        if (enemyName == "Coffin") animName = direction + "_attack";
        else animName = direction + "_shoot";

        // if (direction != lastDirection)
        // {
        //     Debug.Log("masuk");
            // set arahnya untuk range attack
            if (animName == "left_shoot" || animName == "left_attack") transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);

            // update lasdirectionnya
        //     lastDirection = direction;
        // }
        
        anim.Play(animName); // play animasinya

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false; // udah gak attack
    }
}
