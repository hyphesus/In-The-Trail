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
        print(currentHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        for (int i = 0; i < healthSprites.Length; i++){
            if (currentHealth == i){
                print(i);
                healthSprites[i].enabled = true;
                continue;
            }
            healthSprites[i].enabled = false;
        }
    }
}
