using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float radius;

    private void Start()
    {
        if (PlayerPrefs.HasKey(PrefsKeys.bulletDamageKey))
        {
            damage = PlayerPrefs.GetFloat(PrefsKeys.bulletDamageKey);
        }
        else
        {
            damage = 10;
            PlayerPrefs.SetFloat(PrefsKeys.bulletDamageKey, damage);
        }
        Player.Save += SaveDamage;
    }

    private void SaveDamage()
    {
        PlayerPrefs.SetFloat(PrefsKeys.bulletDamageKey, damage);
    }

    public void StartAttack()
    {
        Collider2D[] hittenEnemies = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (var i in hittenEnemies)
        {
            i.GetComponent<Enemy>().GetDamage(damage);
        }
    }
}
