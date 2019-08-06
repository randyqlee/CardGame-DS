using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// This class will show damage dealt to creatures or players
/// </summary>

public class SpecialEffect : MonoBehaviour {

  
    // The text component to show the amount of damage taken by target like: "-2"
    public Text AmountText;

//DS
//    public GameObject SfxExplosion_1_Prefab;
//    public GameObject SFX_Prefab;


    void Awake()
    {
        
        
        
//DS        
//        SfxExplosion_1_Prefab = GlobalSettings.Instance.SfxIMplosion_1_Prefab; 
//        SFX_Prefab = GlobalSettings.Instance.SfxIMplosion_1_Prefab; 
    }

    // A Coroutine to control the fading of this damage effect
    private IEnumerator ShowDamageEffect()
    {
       

//DS

        //GameObject sfx_explosion = GameObject.Instantiate(SfxExplosion_1_Prefab, this.transform.position, Quaternion.identity) as GameObject;
        //GameObject sfx_explosion = GameObject.Instantiate(SFX_Prefab, this.transform.position, Quaternion.identity) as GameObject;

        
        yield return new WaitForSeconds(0.5f);
       
//DS
        //Destroy (sfx_explosion);


        // after the effect is shown it gets destroyed.
        Destroy(this.gameObject);



    }
    /// <summary>
    /// Creates the damage effect.
    /// This is a static method, so it should be called like this: DamageEffect.CreateDamageEffect(transform.position, 5);
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="amount">Amount.</param>
   
    public static void CreateDamageEffect(Vector3 position, int amount)
    {

     
        //GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SfxExplosion_1_Prefab, position, Quaternion.identity) as GameObject;
        
        GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SFX_TakeDamage_Prefab[Random.Range(0,GlobalSettings.Instance.SFX_TakeDamage_Prefab.Count)], position, Quaternion.identity) as GameObject;
        // Get DamageEffect component in this new game object
        //SpecialEffect se = newDamageEffect.GetComponent<SpecialEffect>();
        
        //se.StartCoroutine(se.ShowDamageEffect());

        Sequence s = DOTween.Sequence();
        s.AppendInterval(0.5f);
        s.OnComplete(() => Destroy(newDamageEffect));

    


    }

    public static void CreateAttackEffect(Vector3 position, int amount)
    {

     
       //GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SfxExplosion_1_Prefab, position, Quaternion.identity) as GameObject;
        
        GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SFX_Attack_Prefab[Random.Range(0,GlobalSettings.Instance.SFX_TakeDamage_Prefab.Count)], position, Quaternion.identity) as GameObject;
        // Get DamageEffect component in this new game object
        //SpecialEffect se = newDamageEffect.GetComponent<SpecialEffect>();
        
        //se.StartCoroutine(se.ShowDamageEffect());

        Sequence s = DOTween.Sequence();
        s.AppendInterval(0.5f);
        s.OnComplete(() => Destroy(newDamageEffect));

    }

    public static void CreateSkillEffect(Vector3 position, int amount)
    {

     
        //GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SfxExplosion_1_Prefab, position, Quaternion.identity) as GameObject;
        
        GameObject newDamageEffect = GameObject.Instantiate(GlobalSettings.Instance.SFX_UseSkill_Prefab[Random.Range(0,GlobalSettings.Instance.SFX_TakeDamage_Prefab.Count)], position, Quaternion.identity) as GameObject;
        // Get DamageEffect component in this new game object
        //SpecialEffect se = newDamageEffect.GetComponent<SpecialEffect>();
        
        //se.StartCoroutine(se.ShowDamageEffect());

        Sequence s = DOTween.Sequence();
        s.AppendInterval(0.5f);
        s.OnComplete(() => Destroy(newDamageEffect));

    }

    public static void TempEffectSfx(Vector3 position, int amount)
    {
        CreateDamageEffect(position, amount);
        Command.CommandExecutionComplete();

    }
}
