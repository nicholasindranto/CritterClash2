using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHealth playerHealthScript = other.GetComponent<PlayerHealth>();
        if (playerHealthScript != null)
        {
            // damage sesuai levelnya
            playerHealthScript.PlayerHit(GameManager.Instance.level);
            Destroy(gameObject);
        }
    }
}
