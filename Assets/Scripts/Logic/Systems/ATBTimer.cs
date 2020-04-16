using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ATBTimer
{

    public CreatureLogic owner;

	public float baseSpeed;
	float rate;

	public float turn = 0;

	public bool isPaused = false;


    public delegate void inputCL(CreatureLogic cl);

    
    public event inputCL e_turnEnd;

public delegate void noInput();
        public event noInput e_fullATB;
        public event noInput e_ResetCD;

    public ATBTimer(CreatureLogic cl, int Speed)
    {
        this.owner = cl;
        this.baseSpeed = Speed;

    }


    // Start is called before the first frame update
    public void Start()
    {

		ComputeRate();
        GlobalATB.Instance.e_Tick += OnTick;
        e_fullATB += GlobalATB.Instance.ATBTimerFull;
        e_turnEnd += GlobalATB.Instance.TurnEnd;
        
    }

    void ComputeRate()
    {
        this.rate = baseSpeed/GlobalATB.Instance.globalATB;
    }

    void OnTick()
    {
        if (GlobalATB.Instance.tick && !isPaused)
		{
			turn += rate;
            new UpdateEnergyCommand(owner.UniqueCreatureID, turn*GlobalATB.Instance.globalATB).AddToQueue();

			if (turn >= 1)
			{
                    e_fullATB();
					
			}

		}
    }

	public void ResetTurn()
	{
        Debug.Log ("Reset Turn");
		turn = 0;
        new UpdateEnergyCommand(owner.UniqueCreatureID, turn*GlobalATB.Instance.globalATB).AddToQueue();
		e_turnEnd(owner);
        e_ResetCD();
	}

	public void modifyATB (float value)
	{
		turn = turn + value;
        new UpdateEnergyCommand(owner.UniqueCreatureID, turn*GlobalATB.Instance.globalATB).AddToQueue();

	}

}
