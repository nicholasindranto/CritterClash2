using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public string enemyName;
    public float attackDelay;
    public float secondUntilDone;
    public GameObject attackColPivot; // pivot attacknya
    public Transform enemyAim; // lokasi aim si range attack
    [SerializeField] private GameObject enemyBullet;
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
        SetAttackColliderPivot(false); // make sure at first it is all false
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

        SetAttackColliderPivot(true); // cause you attack we enable it
        AimAtDirection(dir); // arahkan aiming nya

        if (enemyName != "Coffin") // cactus atau coyote
        {
            // bikin bulletnya
            GameObject eBullet = Instantiate(enemyBullet, enemyAim.position, attackColPivot.transform.rotation);
            eBullet.GetComponent<Rigidbody2D>().AddForce(-attackColPivot.transform.up * 10f, ForceMode2D.Impulse);
            Destroy(eBullet, 2f);
        }

        yield return new WaitForSeconds(secondUntilDone);

        SetAttackColliderPivot(false); // it's already done attack
        isAttacking = false; // udah gak attack

        if (enemyName == "Coffin") anim.SetBool("isAttack", isAttacking);
        else anim.SetBool("isShooting", isAttacking);
    }

    public void AimAtDirection(Vector2 dir)
    {
        if (dir.y > 0.5f) attackColPivot.transform.rotation = Quaternion.Euler(0, 0, 180); // atas
        else if (dir.y < -0.5f) attackColPivot.transform.rotation = Quaternion.Euler(0, 0, 0); // bawah
        else if (dir.x > 0.5f) attackColPivot.transform.rotation = Quaternion.Euler(0, 0, 90); // kanan
        else if (dir.x < -0.5f) attackColPivot.transform.rotation = Quaternion.Euler(0, 0, -90); // kiri
    }

    private void SetAttackColliderPivot(bool attack)
    {
        attackColPivot.SetActive(attack);
    }
}
