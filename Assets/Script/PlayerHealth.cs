using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float hp = 10f;
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
        hp -= dmgAmount;
        if (!deathStatus)
        {
            anim.SetTrigger("isHit");
        }
        if (hp <= 0)
        {
            deathStatus = true;
            anim.SetTrigger("isDeath");
        }
    }
}
