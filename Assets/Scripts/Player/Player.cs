﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public delegate void SaveDelegate();
    public static SaveDelegate Save;

    public delegate void HealthUpdate(int currentHealth);
    public static HealthUpdate HPUpdate;

    public delegate void ParamsUpdate(int currentHealth);
    public static ParamsUpdate paramUpdate;

    [Header("Stats")]
    [SerializeField] float fireRate;
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [Header("Animations")]
    [SerializeField] Animator characterAnim;
    [SerializeField] Animator weaponAnim;
    [SerializeField] Animator overlayAnim;

    float nextShotTime;
    bool isAlive;
    bool canAttack;
    PlayerAttackScript attackScript;

    // Start is called before the first frame update
    void Start()
    {
        Save += SaveParams;
        isAlive = true;
        SyncronizePrayerParams();
        nextShotTime = Time.time;
        if (HPUpdate != null) { HPUpdate(currentHealth); };
        attackScript = GetComponent<PlayerAttackScript>();
        weaponAnim.gameObject.SetActive(false);
        overlayAnim.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().buildIndex == PrefsKeys.gardenSceneTag)
            canAttack = false;
        else
            canAttack = true;
    }

    private void SyncronizePrayerParams()
    {
        if (PlayerPrefs.HasKey(PrefsKeys.maxHealthKey))
        {
            maxHealth = PlayerPrefs.GetInt(PrefsKeys.maxHealthKey);
        }
        else
        {
            maxHealth = 100;
            PlayerPrefs.SetInt(PrefsKeys.maxHealthKey, maxHealth);
        }

        if (PlayerPrefs.HasKey(PrefsKeys.currentHealthKey))
        {
            currentHealth = PlayerPrefs.GetInt(PrefsKeys.currentHealthKey);
        }
        else
        {
            currentHealth = maxHealth;
            PlayerPrefs.SetInt(PrefsKeys.currentHealthKey, currentHealth);
        }

        if (PlayerPrefs.HasKey(PrefsKeys.fireRateKey))
        {
            fireRate = PlayerPrefs.GetFloat(PrefsKeys.fireRateKey);
        }
        else
        {
            fireRate = 1f;
            PlayerPrefs.SetFloat(PrefsKeys.fireRateKey, fireRate);
        }        
    }
    private void SaveParams()
    {
        PlayerPrefs.SetInt(PrefsKeys.maxHealthKey, maxHealth);
        PlayerPrefs.SetInt(PrefsKeys.currentHealthKey, currentHealth);
        PlayerPrefs.SetFloat(PrefsKeys.fireRateKey, fireRate);
    }

    // Update is called once per frame
    void Update()
    {       
        if (canAttack)
        {
            if (Input.GetButton("Fire1") && nextShotTime <= Time.time && isAlive)
            {
                //  StopCoroutine(WaitForAnimation());

                weaponAnim.gameObject.SetActive(true);
                overlayAnim.gameObject.SetActive(true);
               // attackScript.gameObject.SetActive(true);
                attackScript.StartAttack();

                nextShotTime = Time.time + fireRate;

                characterAnim.SetTrigger("Shoot");
                weaponAnim.SetTrigger("Shoot");
                overlayAnim.SetTrigger("Shoot");

               // StartCoroutine(WaitForAnimation());
            }
        }
    }


    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Death();
        }
        if (HPUpdate != null) { HPUpdate(currentHealth); }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    void Death()
    {
        isAlive = false;
        SceneManager.LoadScene(PrefsKeys.deathSceneTag);
       // anim.SetBool("isAlive", false);
    }

    public bool GetStatus()
    {
        return isAlive;
    }

    public bool HealBonus(int heal)
    {
        if(currentHealth == maxHealth)
        {
            return false;
        }
        else
        {
            currentHealth += heal;
            return true;
        }
    }
}
