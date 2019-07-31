using UnityEngine;
using System.Collections;
using DG.Tweening;

public class CreatureAttackVisual : MonoBehaviour 
{
    private OneCreatureManager manager;
    private WhereIsTheCardOrCreature w;
    

    void Awake()
    {
        manager = GetComponent<OneCreatureManager>();    
        w = GetComponent<WhereIsTheCardOrCreature>();
        
    }

    public void AttackTarget(int targetUniqueID, int damageTakenByTarget, int damageTakenByAttacker, int attackerHealthAfter, int targetHealthAfter,int attackerUniqueID, bool CanAttack)
    {
        //Debug.Log(targetUniqueID);
       
        
        manager.CanAttackNow = CanAttack;        

        GameObject target = IDHolder.GetGameObjectWithID(targetUniqueID);

      
        
        

       
        

        // bring this creature to front sorting-wise.
        w.BringToFront();
        VisualStates tempState = w.VisualState;
        w.VisualState = VisualStates.Transition;

        transform.DOMove(target.transform.position, 0.7f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InBack).OnComplete(() =>
            {
                if(damageTakenByTarget>0)
                {                    
                    DamageEffect.CreateDamageEffect(target.transform.position, damageTakenByTarget);
//DS
Debug.Log("DoPunch");
                    //target.transform.DOPunchRotation (new Vector3 (0,180,0), 2f, 10, 1);
                    //target.transform.DOPunchPosition (target.transform.position-transform.position, 1f, 1, 1, false);

                    target.transform.DOPunchPosition (target.transform.position/4 - transform.position/4 , 1f, 10, 0.5f, false);

                   
                    
                }
                if(damageTakenByAttacker>0)                    
                    DamageEffect.CreateDamageEffect(transform.position, damageTakenByAttacker);                
                if (targetUniqueID == GlobalSettings.Instance.LowPlayer.PlayerID || targetUniqueID == GlobalSettings.Instance.TopPlayer.PlayerID)
                {
                    // target is a player
                    target.GetComponent<PlayerPortraitVisual>().HealthText.text = targetHealthAfter.ToString();
                }
                else
                    target.GetComponent<OneCreatureManager>().HealthText.text = targetHealthAfter.ToString();

                w.SetTableSortingOrder();
                w.VisualState = tempState;

                manager.HealthText.text = attackerHealthAfter.ToString();
                Sequence s = DOTween.Sequence();
                s.AppendInterval(1f);
                s.OnComplete(Command.CommandExecutionComplete);
                //Command.CommandExecutionComplete();
            });
    }
        
}
