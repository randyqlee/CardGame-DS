using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

    public BuffPanel buffPanel;

    public OverheadText overheadText;

//DS
    public List<GameObject> abilityCard;
    public GameObject portraitPreview;

    public GameObject abilityCardPreview;

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

            //CreatureGlowImage.enabled = value;
            new CreatureGlowCommand(CreatureGlowImage, value).AddToQueue();
        }
    }

    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite
        CreatureGraphicImage.sprite = cardAsset.CardImage;

        AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();

//DS
//removing abilityEffect
/*
    if (cardAsset.abilityEffect != null)
    {
        foreach (AbilityEffect ae in cardAsset.abilityEffect)

        {
            //abilityEffectSprite.Add(ae.abilityImage);
        }
    }
*/

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


    public void Explode()
    {
        GameObject explosion = Instantiate(GlobalSettings.Instance.ExplosionPrefab, transform.position, Quaternion.identity);

        Animator explosionAnim = explosion.GetComponent<Animator>();
        float animTime = explosionAnim.GetCurrentAnimatorStateInfo(0).length;
        
        Destroy(explosion,animTime);

        Command.CommandExecutionComplete();      


        // Sequence s = DOTween.Sequence();
        // s.PrependInterval(2f);
        // s.OnComplete(() => GlobalSettings.Instance.GameOverPanel.SetActive(true));            
        
    }
}
