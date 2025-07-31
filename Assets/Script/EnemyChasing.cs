using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    public Transform player; // reference ke player nya
    public float moveSpeed = 1f; // kecepatan gerakan enemy
    private Rigidbody2D rb; // reference ke rigidbody nya
    private string lastDirection;
    private Animator anim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        // menentukan direction sesuai posisi enemy dan player
        Vector2 dir = (player.position - transform.position).normalized;
        string direction = GetDirection(dir);

        if (direction != lastDirection)
        {
            string animName = direction + "_walk";
            if (animName == "left_walk") transform.localScale = new Vector3(-1, 1, 1); // hadap kiri
            else transform.localScale = new Vector3(1, 1, 1); // hadap biasa
            anim.Play(direction + "_walk");
            lastDirection = direction;
        }

        // enemy bergerak ke arah player
        transform.position = Vector2.MoveTowards(transform.position, player.position, 2f * Time.deltaTime);
    }

    private string GetDirection(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y)) return dir.x > 0 ? "right" : "left";
        else return dir.y > 0 ? "up" : "down";
    }
}
