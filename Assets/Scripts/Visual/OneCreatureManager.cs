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
    public Text ArmorText;
    public Text AttackText;
    public Text NameText;

    [Header("Image References")]
    public Image CreatureGraphicImage;
    public Sprite CreatureGraphicImage_Primary;
    public Sprite CreatureGraphicImage_Secondary;
    public Image CreatureGlowImage;

    public Image ValidTargetGlowImage;
    public GameObject Armor;

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

    private bool isValidTarget = false;
    public bool IsValidTarget
    {
        get
        {
            return isValidTarget;
        }

        set
        {
            isValidTarget = value;

            //CreatureGlowImage.enabled = value;
            new CreatureGlowCommand(ValidTargetGlowImage, value).AddToQueue();
        }
    }

    private bool hasAttacked = true;
    public bool HasAttacked
    {
        get
        {
            return hasAttacked;
        }

        set
        {
            hasAttacked = value;

            Image image = CreatureGraphicImage.GetComponent<Image>();

            if (hasAttacked)
            {
                
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.25f);

            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);

            }
        }
    }

    public void ReadCreatureFromAsset()
    {
        // Change the card graphic sprite

        CreatureGraphicImage_Primary = cardAsset.CardImage;
        CreatureGraphicImage_Secondary = cardAsset.CardImage_Secondary;
        
        CreatureGraphicImage.sprite = CreatureGraphicImage_Primary;
        //CreatureGraphicImage.sprite = cardAsset.CardImage;

 
        AttackText.text = cardAsset.Attack.ToString();
        HealthText.text = cardAsset.MaxHealth.ToString();
        NameText.text = cardAsset.cardName.ToString();

        if(ArmorText!=null)
        ArmorText.text = cardAsset.Armor.ToString();

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

    public void TakeDamage(int amount, int healthAfter, int armorAfter)
    {
        
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
            if(ArmorText!=null)
            ArmorText.text = armorAfter.ToString();
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

    public void ChangeArmor(int armorAfter)
    {
        
        if(armorAfter <=0)
        {
            ArmorText.text = 0.ToString();
            Armor.SetActive(false);
        }else{
            Armor.SetActive(true);
            ArmorText.text = armorAfter.ToString();
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
