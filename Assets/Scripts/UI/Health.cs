using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static Health instance;
    public Image[] healthSprites; // Array of health sprite images
    public int maxHealth = 5; // Number of health units
    public int currentHealth;
    public AudioSource audioSource; // AudioSource to play the sound
    public AudioClip damageClip; // Audio clip to play when health decreases

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

        PlayDamageSound(); // Play the sound whenever damage is taken
        print(currentHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        for (int i = 0; i < healthSprites.Length; i++)
        {
            if (currentHealth == i)
            {
                print(i);
                healthSprites[i].enabled = true;
                continue;
            }
            healthSprites[i].enabled = false;
        }
    }

    void PlayDamageSound()
    {
        if (audioSource != null && damageClip != null)
        {
            audioSource.PlayOneShot(damageClip);
        }
    }
}
