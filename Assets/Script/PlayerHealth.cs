using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float hp = 10f;
    private Animator anim;
    public bool deathStatus;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        deathStatus = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerHit(float dmgAmount)
    {
        if (deathStatus) return;
        hp -= dmgAmount;
        if (hp <= 0)
        {
            deathStatus = true;
            StartCoroutine(DeathCoroutine());
        }
        else anim.SetTrigger("isHit");
    }

    IEnumerator DeathCoroutine()
    {
        Debug.Log("inside coroute death");
        anim.SetTrigger("isDeath"); // animasi
        yield return new WaitForSeconds(1f);
        Debug.Log("after coroutine death");
        Destroy(gameObject);
    }
}
