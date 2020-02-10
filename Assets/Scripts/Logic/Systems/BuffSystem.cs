using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    public static BuffSystem Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public void AddBuff(CreatureLogic source, CreatureLogic target, string buffName, int buffCooldown)
    {

        BuffEffect buffEffect = System.Activator.CreateInstance(System.Type.GetType(buffName), new System.Object[]{source, target, buffCooldown}) as BuffEffect;
        

        //if buff, can only affect allies
        if(buffEffect.isBuff && source.canBuff && target.canBeBuffed && !target.isDead)
        //if(buffEffect.isBuff)
        {
            //check if same team
            if(target.owner == source.owner)
            {
                AddBuff(buffEffect);        
            }
        }

        //if debuff, can only affect enemies
        if(buffEffect.isDebuff && source.canDebuff && target.canBeDebuffed && !target.isDead)
        {
            //check if same team
            if(target.owner != source.owner)
            {
                AddBuff(buffEffect);        
            }
        }


    }

    public void AddBuff(BuffEffect buff)
    {
        bool buffExists = false;

        //check if same buff already exists, just update the buff
        foreach (BuffEffect be in buff.target.buffEffects)
        {
            if (be.GetType().Name == buff.GetType().Name)
            {    
                if (be.buffCooldown < buff.buffCooldown)
                    be.buffCooldown = buff.buffCooldown;

            
            //Set Armor to Original Value when re-applied
            if (be.GetType().Name == "Armor")
            {                           
                if(be.target.Armor < be.defaultArmor)
                be.target.Armor = be.defaultArmor;
            }
            
                buffExists = true;

                new UpdateBuffCommand(be).AddToQueue();

                new ShowBuffPreviewCommand(buff, buff.source.ID, buff.GetType().Name).AddToQueue();

                buff.target.E_buffApplied(buff);

                break;                
            }
        }

        if (!buffExists)
        {
            buff.RegisterCooldown();
            
            buff.CauseBuffEffect();
            buff.target.buffEffects.Add(buff);

           new AddBuffCommand(buff, buff.target.UniqueCreatureID).AddToQueue();

           new ShowBuffPreviewCommand(buff, buff.source.ID, buff.GetType().Name).AddToQueue();

            buff.target.E_buffApplied(buff);
        }
        

    }

    public void RemoveBuff(CreatureLogic creature, BuffEffect buff)
    {
        buff.UndoBuffEffect();
        buff.UnregisterCooldown();         
        new DestroyBuffCommand(buff, creature.UniqueCreatureID).AddToQueue();
        creature.buffEffects.Remove(buff);
    }

   public void RemoveRandomBuff(CreatureLogic creature)
    {
        BuffEffect buff = creature.RandomBuff();
        if (buff != null)
        {
            RemoveBuff(creature,buff);
        }       
    }

   public void RemoveRandomDebuff(CreatureLogic creature)
    {
        BuffEffect buff = creature.RandomDebuff();
        if (buff != null)
        {
            RemoveBuff(creature,buff);
        }       
    }

    public void RemoveAllBuffs(CreatureLogic creature)
    {
        int i = creature.buffEffects.Count;
        if(creature.buffEffects.Count > 0)
        {
            for(int x = i-1; x>=0; x--)
            {
                RemoveBuff(creature,creature.buffEffects[x]);
            }
        }
    }//RemoveAllBuffs

    public void RemoveDeBuffsAll(CreatureLogic creature)
    {
        int i = creature.buffEffects.Count;
        for(int x = i-1; x>=0; x--)
        {
            if(creature.buffEffects[x].isDebuff)
            {
                RemoveBuff(creature,creature.buffEffects[x]);
            }            
        }
    }

    public void RemoveBuffsAll(CreatureLogic creature)
    {
        int i = creature.buffEffects.Count;
        for(int x = i-1; x>=0; x--)
        {
            if(creature.buffEffects[x].isBuff)
            {
                RemoveBuff(creature,creature.buffEffects[x]);
            }            
        }
    }



}
