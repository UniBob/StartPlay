using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] AnimationClip atack;
    [SerializeField] float damage;
    [SerializeField] float radius;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Enemy enemy = collision.GetComponent<Enemy>();
    //    if (enemy != null)
    //    {
    //        enemy.GetDamage(damage);
    //    }
    //}

    public void StartAttack()
    {
        Collider2D[] hittenEnemies = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (var i in hittenEnemies)
        {
            i.GetComponent<Enemy>().GetDamage(damage);
        }
    }

}
