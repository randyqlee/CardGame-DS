using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DragUltiAttack : DraggingActions {

    // reference to the sprite with a round "Target" graphic
    private SpriteRenderer sr;
    // LineRenderer that is attached to a child game object to draw the arrow
    private LineRenderer lr;
    // reference to WhereIsTheCardOrCreature to track this object`s state in the game
    public WhereIsTheCardOrCreature whereIsThisCreature;
    // the pointy end of the arrow, should be called "Triangle" in the Hierarchy
    private Transform triangle;
    // SpriteRenderer of triangle. We need this to disable the pointy end if the target is too close.
    private SpriteRenderer triangleSR;
    // when we stop dragging, the gameObject that we were targeting will be stored in this variable.
    private GameObject Target;
    // Reference to creature manager, attached to the parent game object
    public OneCreatureManager manager;

    //Reference to Skill Hoverpreview
    private SkillHoverPreview skillHoverPreview;

    //Reference to ulti skill
    private bool canUlti;

    void Awake()
    {
        // establish all the connections
        sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerName = "AboveEverything";

        lr = GetComponentInChildren<LineRenderer>();
        lr.sortingLayerName = "AboveEverything";

        triangle = transform.Find("Triangle");
        triangleSR = triangle.GetComponent<SpriteRenderer>();
        triangle.GetComponent<Canvas>().sortingLayerName = "AboveEverything";

        skillHoverPreview = GetComponent<SkillHoverPreview>();


        // manager = GetComponentInParent<OneCreatureManager>();
        // whereIsThisCreature = GetComponentInParent<WhereIsTheCardOrCreature>();

        //refer to reference gameobject
        // manager = GetComponentInParent<AbilityCard>().referenceCreature.GetComponent<OneCreatureManager>();
        // whereIsThisCreature = GetComponentInParent<AbilityCard>().referenceCreature.GetComponent<WhereIsTheCardOrCreature>();
    }

    public override bool CanDrag
    {
        get
        {   
            // TEST LINE: just for testing 
            // return true;

            // we can drag this card if 
            // a) we can control this our player (this is checked in base.canDrag)
            // b) creature "CanAttackNow" - this info comes from logic part of our code into each creature`s manager script
            
            //GetComponentInParent<AbilityCard>().CanUlti(canUlti);
            
            return base.CanDrag && manager.CanAttackNow && GetComponentInParent<AbilityCard>().CanUlti(canUlti);
            
            //return base.CanDrag;
            
        }
    }

    public override void OnStartDrag()
    {
        whereIsThisCreature.VisualState = VisualStates.Dragging;
        // enable target graphic
        sr.enabled = true;
        // enable line renderer to start drawing the line.
        lr.enabled = true;

       

    }

    public override void OnDraggingInUpdate()
    {

        Vector3 notNormalized = transform.position - transform.parent.position;
        Vector3 direction = notNormalized.normalized;
        float distanceToTarget = (direction*2.3f).magnitude;
        if (notNormalized.magnitude > distanceToTarget)
        {
            // draw a line between the creature and the target
            lr.SetPositions(new Vector3[]{ transform.parent.position, transform.position - direction*2.3f });
            lr.enabled = true;

            // position the end of the arrow between near the target.
            triangleSR.enabled = true;
            triangleSR.transform.position = transform.position - 1.5f*direction;

            // proper rotarion of arrow end
            float rot_z = Mathf.Atan2(notNormalized.y, notNormalized.x) * Mathf.Rad2Deg;
            triangleSR.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        }
        else
        {
            // if the target is not far enough from creature, do not show the arrow
            lr.enabled = false;
            triangleSR.enabled = false;
        }

        
            
    }

    public override void OnEndDrag()
    {
        Target = null;
        RaycastHit[] hits;
        // TODO: raycast here anyway, store the results in 
        hits = Physics.RaycastAll(origin: Camera.main.transform.position, 
            direction: (-Camera.main.transform.position + this.transform.position).normalized, 
            maxDistance: 30f) ;

        foreach (RaycastHit h in hits)
        {
            if ((h.transform.tag == "TopPlayer" && this.tag == "LowCreature") ||
                (h.transform.tag == "LowPlayer" && this.tag == "TopCreature"))
            {
                // go face
                Target = h.transform.gameObject;
            }
            else if ((h.transform.tag == "TopCreature" && this.tag == "LowCreature") ||
                    (h.transform.tag == "LowCreature" && this.tag == "TopCreature"))
            {
                // hit a creature, save parent transform
                Target = h.transform.parent.gameObject;
            }
               
        }

        bool targetValid = false;

        if (Target != null)
        {
            int targetID = Target.GetComponent<IDHolder>().UniqueID;
            //Debug.Log("Target ID: " + targetID);
            if (targetID == GlobalSettings.Instance.LowPlayer.PlayerID || targetID == GlobalSettings.Instance.TopPlayer.PlayerID)
            {
                // attack character
                Debug.Log("Attacking "+Target);
                Debug.Log("TargetID: " + targetID);
                CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID].GoFace();
                targetValid = true;

                //DS - fix, set ThisPreviewEnabled back to True
                if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
                else
                whereIsThisCreature.VisualState = VisualStates.TopTable;
            }
            //ORIGINAL
            //else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null)

            else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null && CreatureLogic.CreaturesCreatedThisGame[targetID].canBeAttacked)
            {
                // if targeted creature is still alive, attack creature
                targetValid = true;
                CreatureLogic.CreaturesCreatedThisGame[GetComponentInParent<IDHolder>().UniqueID].AttackCreatureWithID(targetID);                

                //DS - fix, set ThisPreviewEnabled back to True
                if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
                else
                whereIsThisCreature.VisualState = VisualStates.TopTable;

                //Debug.Log("Attacking "+Target);
            }
            else if (CreatureLogic.CreaturesCreatedThisGame[targetID] != null && !CreatureLogic.CreaturesCreatedThisGame[targetID].canBeAttacked)
            {
                // if targeted creature is still alive, attack creature
                Debug.Log("Invalid Target: Attack a Taunt Creature");
                //Debug.Log("Attacking "+Target);

                //DS - fix, set ThisPreviewEnabled back to True
                if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
                else
                whereIsThisCreature.VisualState = VisualStates.TopTable;                
            }
                
        }

        if (!targetValid)
        {
            // not a valid target, return
            if(tag.Contains("Low"))
                whereIsThisCreature.VisualState = VisualStates.LowTable;
            else
                whereIsThisCreature.VisualState = VisualStates.TopTable;
            whereIsThisCreature.SetTableSortingOrder();
        }

        // return target and arrow to original position
        transform.localPosition = Vector3.zero;
        sr.enabled = false;
        lr.enabled = false;
        triangleSR.enabled = false;

    }

    // NOT USED IN THIS SCRIPT
    protected override bool DragSuccessful()
    {
        return true;
    }


    //DS
        public override void OnCancelDrag()
    {}
}
