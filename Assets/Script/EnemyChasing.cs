using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    public Transform player; // reference ke player nya
    public float moveSpeed = 1f; // kecepatan gerakan enemy
    private Rigidbody2D rb; // reference ke rigidbody nya
    private Animator anim;
    private EnemyAttack enemyAttack;
    private float lastDirectionX;
    private float lastDirectionY;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        enemyAttack = GetComponent<EnemyAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.Log("gak ada player");
            Destroy(gameObject);
            return;
        }

        if (!enemyAttack.isPlayerInRange)
            {
                // menentukan direction sesuai posisi enemy dan player
                Vector2 dir = (player.position - transform.position).normalized;

                // update lastdirection
                lastDirectionX = dir.x;
                lastDirectionY = dir.y;

                if (dir.x != 0) transform.localScale = new Vector3((Mathf.Sign(dir.x)) * 1, 1, 1);
                else if (lastDirectionX != 0) transform.localScale = new Vector3((Mathf.Sign(lastDirectionX)) * 1, 1, 1);

                enemyAttack.AimAtDirection(dir);

                anim.SetFloat("DirectionX", dir.x);
                anim.SetFloat("DirectionY", dir.y);

                // enemy bergerak ke arah player
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
    }
}
