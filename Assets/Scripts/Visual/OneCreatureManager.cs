using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneCreatureManager : MonoBehaviour 
{
    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text HealthText;
    public Text AttackText;
    [Header("Image References")]
    public Image CreatureGraphicImage;
    public Image CreatureGlowImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCreatureFromAsset();
    }

    private bool canAttackNow = false;
    public bool CanAttackNow
    {
        get
        {
            return canAttackNow;
        }

        set
        {
            canAttackNow = value;

            CreatureGlowImage.enabled = value;
        }
    }

    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.CardImage;

        AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }	

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
        } 
    }//TakeDamage


    public void TakeHealing(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            //use negative in amount to channel healing effect
            DamageEffect.CreateDamageEffect(transform.position, -amount);
            HealthText.text = healthAfter.ToString();
        } 
    }//TakeDamage


    public void ChangeAttack(int attackAfter)
    {
        
        if(attackAfter <0)
        {
            AttackText.text = 0.ToString();
        }else{
            AttackText.text = attackAfter.ToString();
        }
    }//Change Attack

    public void ChangeHealth(int healthAfter)
    {
        
        if(healthAfter <0)
        {
            HealthText.text = 0.ToString();
        }else{
            HealthText.text = healthAfter.ToString();
        }
    }//Change Attack
}
