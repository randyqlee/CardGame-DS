using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalATB : MonoBehaviour
{
    public static GlobalATB Instance;

	public int globalATB = 1400;
	public float maxCharacterTurn = 10f;

    public bool tick = false;
	public bool freezeTick = false;

    public List<CreatureLogic> activeCL;
    public List<CreatureLogic> sortActiveCL;

    public delegate void GlobalTick();
    public event GlobalTick e_Tick;



    void Awake()
    {
        Instance = this;
    }
    public void Start()
    {;
        activeCL = new List<CreatureLogic>();
        sortActiveCL = new List<CreatureLogic>();

    }

    public void RunGlobalATB()
    {
        StartCoroutine (RunTick());
    }

	IEnumerator RunTick()
	{
        tick = false;
        yield return new WaitForSeconds(1);
        tick = true;

        if (tick)
            if (e_Tick != null)
                e_Tick();

        yield return null;
        //check if any hero reached full ATB
        if (!freezeTick)
            StartCoroutine (RunTick());

        else CheckActiveCL ();

    }

    public void ATBTimerFull()
    {
        freezeTick = true;
    }

    public void TurnEnd(CreatureLogic cl)
    {
        cl.owner.HighlightPlayableCards(true);
        cl.owner.PArea.ControlsON = false;
        CheckActiveCL();
    }


//check all CL on Table if Turn >= 1, then add to ActiveCL list
    void CheckActiveCL()
    {
        if (activeCL != null)
			activeCL.Clear();

        foreach (Player p in Player.Players)
        {
            foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            {
                if (cl.timer.energy >= globalATB)
                {
                    activeCL.Add(cl);
                }
            }
        }

        if (activeCL.Count > 0)
        {
            //sort activeCL
            sortActiveCL = SortCL(activeCL);
            //make the CL with highest >1 turn active, add checks if !stun, etc. else. move to next CL
            ActivateCL(sortActiveCL[sortActiveCL.Count - 1]);
            
        }
        else
        {
            freezeTick = false;
            StartCoroutine (RunTick());
        }

    }

    void ActivateCL (CreatureLogic cl)
    {
        TargetSystem.Instance.ClearAllEnemiesAsTargets(cl.owner.otherPlayer);
        TurnManager.Instance.whoseTurn = cl.owner;
        cl.owner.PArea.ControlsON = true;
        cl.OnTurnStart();
        cl.owner.HighlightPlayableCards();

    }

    List<CreatureLogic> SortCL(List<CreatureLogic> unsortedCL)
    {
        var returnList = unsortedCL;
        returnList.Sort(CompareCLTurn);
        return returnList;
    }

    int CompareCLTurn(CreatureLogic i1, CreatureLogic i2)
    {
        return i1.timer.energy.CompareTo(i2.timer.energy); 
    }


}
