﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class TableVisual : MonoBehaviour 
{
    // PUBLIC FIELDS

    // an enum that mark to whish caracter this table belongs. The alues are - Top or Low
    public AreaPosition owner;

    // a referense to a game object that marks positions where we should put new Creatures
    public SameDistanceChildren slots;

    // PRIVATE FIELDS

    // list of all the creature cards on the table as GameObjects
    private List<GameObject> CreaturesOnTable = new List<GameObject>();

    // are we hovering over this table`s collider with a mouse
    private bool cursorOverThisTable = false;

    // A 3D collider attached to this game object
    private BoxCollider col;
    

    // PROPERTIES

    // returns true if we are hovering over any player`s table collider
    public static bool CursorOverSomeTable
    {
        get
        {
            TableVisual[] bothTables = GameObject.FindObjectsOfType<TableVisual>();
            return (bothTables[0].CursorOverThisTable || bothTables[1].CursorOverThisTable);
        }
    }

    // returns true only if we are hovering over this table`s collider
    public bool CursorOverThisTable
    {
        get{ return cursorOverThisTable; }
    }

    // METHODS

    // MONOBEHAVIOUR METHODS (mouse over collider detection)
    void Awake()
    {
        col = GetComponent<BoxCollider>();
    }

    // CURSOR/MOUSE DETECTION
    void Update()
    {
        // we need to Raycast because OnMouseEnter, etc reacts to colliders on cards and cards "cover" the table
        // create an array of RaycastHits
        RaycastHit[] hits;
        // raycst to mousePosition and store all the hits in the array
        hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30f);

        bool passedThroughTableCollider = false;
        foreach (RaycastHit h in hits)
        {
            // check if the collider that we hit is the collider on this GameObject
            if (h.collider == col)
                passedThroughTableCollider = true;
        }
        cursorOverThisTable = passedThroughTableCollider;
    }
   
    // method to create a new creature and add it to the table
    public void AddCreatureAtIndex(CardAsset ca, int UniqueID ,int index)
    {
        // create a new creature from prefab
        GameObject creature = GameObject.Instantiate(GlobalSettings.Instance.CreaturePrefab, slots.Children[index].transform.position, Quaternion.identity) as GameObject;

        // apply the look from CardAsset
        OneCreatureManager manager = creature.GetComponent<OneCreatureManager>();
        manager.cardAsset = ca;
        manager.ReadCreatureFromAsset();

        //DS
        OneCardManager ocm = creature.GetComponent<OneCreatureManager>().abilityCardPreview.GetComponent<OneCardManager>();
             ocm.NameText.text = ca.name;
             
             ocm.DescriptionText.text = ca.Description;
             ocm.CardGraphicImage.sprite = ca.CardImage;


        // add tag according to owner
        foreach (Transform t in creature.GetComponentsInChildren<Transform>())
            t.tag = owner.ToString()+"Creature";
        
        // parent a new creature gameObject to table slots
        creature.transform.SetParent(slots.transform);

        // add a new creature to the list
        CreaturesOnTable.Insert(index, creature);
        //Debug.Log("Creature Index: " +index +" Creature Name: " +creature.GetType().Name.ToString());

        // let this creature know about its position
        WhereIsTheCardOrCreature w = creature.GetComponent<WhereIsTheCardOrCreature>();
        w.Slot = index;
        if (owner == AreaPosition.Low)
            w.VisualState = VisualStates.LowTable;
        else
            w.VisualState = VisualStates.TopTable;

        // add our unique ID to this creature
        IDHolder id = creature.AddComponent<IDHolder>();
        id.UniqueID = UniqueID;

        // after a new creature is added update placing of all the other creatures
        ShiftSlotsGameObjectAccordingToNumberOfCreatures();
        PlaceCreaturesOnNewSlots();


        //DS
        AddSkillsToPanel(manager,UniqueID);

        //end command execution
        Command.CommandExecutionComplete();
    }


    // returns an index for a new creature based on mousePosition
    // included for placing a new creature to any positon on the table
    public int TablePosForNewCreature(float MouseX)
    {
        // if there are no creatures or if we are pointing to the right of all creatures with a mouse.
        // right - because the table slots are flipped and 0 is on the right side.
        if (CreaturesOnTable.Count == 0 || MouseX > slots.Children[0].transform.position.x)
            return 0;
        else if (MouseX < slots.Children[CreaturesOnTable.Count - 1].transform.position.x) // cursor on the left relative to all creatures on the table
            return CreaturesOnTable.Count;
        for (int i = 0; i < CreaturesOnTable.Count; i++)
        {
            if (MouseX < slots.Children[i].transform.position.x && MouseX > slots.Children[i + 1].transform.position.x)
                return i + 1;
        }
        Debug.Log("Suspicious behavior. Reached end of TablePosForNewCreature method. Returning 0");
        return 0;
    }

    // Destroy a creature
    public void RemoveCreatureWithID(int IDToRemove)
    {
        // TODO: This has to last for some time
        // Adding delay here did not work because it shows one creature die, then another creature die. 
        // 
        //Sequence s = DOTween.Sequence();
        //s.AppendInterval(1f);
        //s.OnComplete(() =>
        //   {
                
        //    });
        GameObject creatureToRemove = IDHolder.GetGameObjectWithID(IDToRemove);
        
        //ORIGINAL SCRIPT
        //CreaturesOnTable.Remove(creatureToRemove);
        //Destroy(creatureToRemove);
        // ShiftSlotsGameObjectAccordingToNumberOfCreatures();
        // PlaceCreaturesOnNewSlots();

        //New SCRIPT

       
        StartCoroutine(HideCreature(creatureToRemove));
            


        
    }

    IEnumerator HideCreature(GameObject creatureToRemove)
    {
        yield return new WaitForSeconds(2f);

        foreach (GameObject go in creatureToRemove.GetComponent<OneCreatureManager>().abilityCard)
        {
            //go.SetActive(false);
            go.GetComponent<AbilityCard>().abilityImage.DOColor(new Color(0.2f,0.2f,0.2f,1f),1f);
            //go.GetComponent<AbilityCard>().abilityImage.color = new Color (0.2f,0.2f,0.2f,0.2f);
        }
        
        creatureToRemove.SetActive(false);

        Command.CommandExecutionComplete();

        StopCoroutine(HideCreature(creatureToRemove));
    }

   

    public void ResurrectCreatureWithID(int IDToResurrect)
    {
        
                
        //    });
        GameObject creatureToResurrect = IDHolder.GetGameObjectWithID(IDToResurrect);
        
        //ORIGINAL SCRIPT
        // CreaturesOnTable.Remove(creatureToRemove);
        // Destroy(creatureToRemove);
        // ShiftSlotsGameObjectAccordingToNumberOfCreatures();
        // PlaceCreaturesOnNewSlots();

        //New SCRIPT
        creatureToResurrect.SetActive(true);



        Command.CommandExecutionComplete();
    }

    /// <summary>
    /// Shifts the slots game object according to number of creatures.
    /// </summary>
    void ShiftSlotsGameObjectAccordingToNumberOfCreatures()
    {
        float posX;
        if (CreaturesOnTable.Count > 0)
            posX = (slots.Children[0].transform.localPosition.x - slots.Children[CreaturesOnTable.Count - 1].transform.localPosition.x) / 2f;
        else
            posX = 0f;

        slots.gameObject.transform.DOLocalMoveX(posX, 0.3f);  
    }

    /// <summary>
    /// After a new creature is added or an old creature dies, this method
    /// shifts all the creatures and places the creatures on new slots.
    /// </summary>
    void PlaceCreaturesOnNewSlots()
    {
        foreach (GameObject g in CreaturesOnTable)
        {
            g.transform.DOLocalMoveX(slots.Children[CreaturesOnTable.IndexOf(g)].transform.localPosition.x, 0.3f);
            // apply correct sorting order and HandSlot value for later 
            // TODO: figure out if I need to do something here:
            // g.GetComponent<WhereIsTheCardOrCreature>().SetTableSortingOrder() = CreaturesOnTable.IndexOf(g);
        }
    }


//DS - change these into Command and Tweens
// if possible, make the skill buttons delegates. if not, update everytime cooldown changes.
// if hero dies, also deactivate (hide) the corresponding GO from the OneCreatureManager reference
    void AddSkillsToPanel(OneCreatureManager manager, int UniqueID)
    {
        CreatureLogic cl = CreatureLogic.CreaturesCreatedThisGame[UniqueID];
        
        //Create portraitWSkill Gameobject
        GameObject ps = GameObject.Instantiate (GlobalSettings.Instance.PortraitWSkillsPreviewPrefab, GetComponentInParent<PlayerArea>().skillPanel.abilities.transform) as GameObject;

         GameObject portrait = GameObject.Instantiate (GlobalSettings.Instance.AbilityCardPreviewPrefab, ps.GetComponent<PortraitWSkill>().portrait.transform) as GameObject;
            portrait.GetComponent<AbilityCard>().abilityImage.sprite = cl.ca.HeroPortrait;            
            manager.portraitPreview = portrait;
            
           //DS: Updates Portrait information
           portrait.GetComponent<AbilityCard>().abilityCardPreview.GetComponent<OneCardManager>().cardAsset = cl.ca;
           IDHolder id = portrait.AddComponent<IDHolder>();
           id.UniqueID = UniqueID;

           
           //Set the tag of the Portraits
           foreach(Transform t in portrait.GetComponent<AbilityCard>().GetComponentsInChildren<Transform>())           
           {
               t.tag = owner.ToString()+"Creature";
           }

           //DS:  Assign reference creature gameobject to AbilityCard
           //portrait.GetComponent<AbilityCard>().referenceCreature = manager.gameObject;

           portrait.GetComponentInChildren<DragUltiAttack>().manager = manager;
           portrait.GetComponentInChildren<DragUltiAttack>().whereIsThisCreature = manager.gameObject.GetComponent<WhereIsTheCardOrCreature>();

        foreach (CreatureEffect ce in cl.creatureEffects)
        {
             //GameObject ac = GameObject.Instantiate (GlobalSettings.Instance.AbilityCardPreviewPrefab, GetComponentInParent<PlayerArea>().skillPanel.abilities.transform) as GameObject;
             
             GameObject ac = GameObject.Instantiate (GlobalSettings.Instance.AbilityCardPreviewPrefab, ps.GetComponent<PortraitWSkill>().skills.transform) as GameObject;

           

             if (ce.abilityPreviewSprite!=null) {
             ac.GetComponent<AbilityCard>().abilityImage.sprite = ce.abilityPreviewSprite;            
             ac.GetComponent<AbilityCard>().abilityCooldownText.text = ce.remainingCooldown.ToString();

           

             

             if(ce.skillType == SkillType.Ultimate)
             portrait.GetComponent<AbilityCard>().abilityCooldownText.text = ce.remainingCooldown.ToString();
             else
             portrait.GetComponent<AbilityCard>().abilityCooldownText.text = "";
          


             OneCardManager ocm = ac.GetComponent<AbilityCard>().abilityCardPreview.GetComponent<OneCardManager>();
             ocm.NameText.text = ce.Name;
             ocm.ManaCostText.text = ce.creatureEffectCooldown.ToString();
             ocm.DescriptionText.text = ce.abilityDescription;
             ocm.CardGraphicImage.sprite = ce.abilityPreviewSprite;

             manager.abilityCard.Add(ac);

             ce.abilityCard = ac.GetComponent<AbilityCard>();

              if(ce.skillType == SkillType.Ultimate)
              ce.abilityCard = portrait.GetComponent<AbilityCard>();


          
             new UpdateCooldownCommand (ce.abilityCard, ce.remainingCooldown, ce.creatureEffectCooldown).AddToQueue();

             }

           
            
        }

        


        // foreach (CreatureEffect ce in cl.creatureEffects)
        // {
                         

        //      if (ce.abilityPreviewSprite!=null) 
        //      {
             
        //      if(ce.skillType == SkillType.Ultimate)
        //      portrait.GetComponent<AbilityCard>().abilityCooldownText.text = ce.remainingCooldown.ToString();
        //      else
        //      portrait.GetComponent<AbilityCard>().abilityCooldownText.text = "";         

        //      new UpdateCooldownCommand (ce.abilityCard, ce.remainingCooldown, ce.creatureEffectCooldown).AddToQueue();

        //      }
            
        // }    

            
            
    }

}
