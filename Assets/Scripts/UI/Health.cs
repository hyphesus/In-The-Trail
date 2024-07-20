using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    // Start is called before the first frame updatepublic static HealthManager instance;
    public static Health instance;
    public Image[] healthSprites; // Array of health sprite images
    public int maxHealth = 5; // Number of health units
    public int currentHealth;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        for (int i = 0; i < healthSprites.Length; i++)
        {
            if (i < currentHealth)
            {
                healthSprites[i].enabled = true; // Show health unit
            }
            else
            {
                healthSprites[i].enabled = false; // Hide health unit
            }
        }
    }
}
