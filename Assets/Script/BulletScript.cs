using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage = 1f;

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
        EnemyHealth enemyHealthScript = other.GetComponent<EnemyHealth>();

        if (enemyHealthScript != null)
        {
            enemyHealthScript.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
