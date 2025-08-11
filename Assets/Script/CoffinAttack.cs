using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinAttack : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            // damage sesuai levelnya
            other.GetComponent<PlayerHealth>().PlayerHit(GameManager.Instance.level);
        }
    }
}
