using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 lastMoveDir; // ambil terakhir dia abis gerak menghadap kemana
    private Animator anim;

    public Transform aim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = moveInput * speed; // pergerakan atas bawah nya

        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3((Mathf.Sign(moveInput.x)) * 3, 3, 3); // hadap sesuai arahnya kanan atau kiri
                                                                                     // Mathf.Sign = kita ambil positif atau negatifnya
        }
        else if (lastMoveDir.x != 0)
        {
            transform.localScale = new Vector3((Mathf.Sign(lastMoveDir.x)) * 3, 3, 3); // hadap sesuai arah gerakan terakhir
        }

        // arahkan aim sesuai arah terakhir gerakan
        if (lastMoveDir != Vector2.zero)
        {
            float angle = Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) * Mathf.Rad2Deg;
            // Mathf.Atan2() -> menghasilkan derajat kaya 0 untuk kanan, 90 untuk atas, 180 untuk kiri
            aim.rotation = Quaternion.Euler(0, 0, angle - 90);
            // angle - 90 -> karna by default arahnya keatas jadi di -90
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // ambil dari inputactionnya, lalu baca inputnya (atas bawah kanan kirinya)

        if (context.canceled) // gak gerak alias WASD atau arrow key gak ditekan
        {
            anim.SetBool("isWalking", false); // idle
            anim.SetFloat("LastInputX", lastMoveDir.x); // arah sesuai dengan terakhir kali arah jalannya
            anim.SetFloat("LastInputY", lastMoveDir.y);
        }
        else
        {
            lastMoveDir = moveInput.normalized; // simpan arah terakhirr gerakan
            anim.SetBool("isWalking", true); // jalankan animasi walk
        }

        anim.SetFloat("InputX", moveInput.x); // set arah animasinya sesuai kanan atau kiri
        anim.SetFloat("InputY", moveInput.y); // set arah animasinya sesuai atas atau bawah
    }
}
