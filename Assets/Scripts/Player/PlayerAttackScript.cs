using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] AnimationClip atack;
    [SerializeField] float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetDamage(damage);
        }
    }

    public void StartAttack(Vector3 pos)
    {
        transform.position = pos;
        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(atack.length);

        DoAfterAnimation();
    }

    private void DoAfterAnimation()
    {
        gameObject.SetActive(false);
    }
}
