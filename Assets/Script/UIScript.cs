using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private TextMeshProUGUI healthText;

    private float lastHP;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        healthText = GetComponent<TextMeshProUGUI>();
        lastHP = playerHealth.hp;
        healthText.text = "Health = " + lastHP;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate() {
        if (playerHealth != null && lastHP != playerHealth.hp)
        {
            lastHP = playerHealth.hp;
            healthText.text = "Health = " + lastHP;
        }
    }
}
