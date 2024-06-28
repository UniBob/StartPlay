using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]

public class DamageDiller : MonoBehaviour
{
    [SerializeField] float damage;
    private void Start()
    {
        damage = FindObjectOfType<Player>().GetBulletDamage();
    }
    public float GetDamage() { return damage; }
    public void IncreaseDamage(float dam) { damage += dam; }
    public void SetDamage(float dam) { damage = dam; }
}
