using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform aim;
    public GameObject bullet;
    public float fireForce = 10f;
    private float shootCooldown = 0.25f;
    private float shootTimer = 0.5f;
    private Animator anim;
    private PlayerMovement playerMove;
    private PlayerHealth playerHealth;

    private void OnShoot()
    {
        if (shootTimer > shootCooldown)
        {
            // bikin pelurunya
            shootTimer = 0;
            GameObject intBullet = Instantiate(bullet, aim.position, aim.rotation);
            intBullet.GetComponent<Rigidbody2D>().AddForce(aim.up * fireForce, ForceMode2D.Impulse);
            Destroy(intBullet, 2f);

            // ambil shoot directionnya
            Vector2 shootDir = playerMove.LastMoveDir;

            // handle animasinya
            anim.SetFloat("LastInputX", shootDir.x);
            anim.SetFloat("LastInputY", shootDir.y);
            anim.SetBool("isShooting", true);

            // reset animasi shooting setelah delay
            StartCoroutine(ResetShootAnim());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMove = GetComponent<PlayerMovement>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if (!playerHealth.deathStatus) // kalo nggak mati
            {
                OnShoot();
            }
        }
    }

    private IEnumerator ResetShootAnim()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isShooting", false);
    }
}
