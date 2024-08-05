using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] bool canDealDamage;
    Collider2D collider;
    private void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        canDealDamage = false;
        if (PlayerPrefs.HasKey(PrefsKeys.bulletDamageKey))
        {
            damage = PlayerPrefs.GetFloat(PrefsKeys.bulletDamageKey);
        }
        else
        {
            damage = 20;
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
        collider.enabled = true;
    }

    public void EndAttack()
    {
        collider.enabled = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<Enemy>().GetDamage(damage);
    }
}
