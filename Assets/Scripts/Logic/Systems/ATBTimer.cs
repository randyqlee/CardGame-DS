using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ATBTimer
{

    public CreatureLogic owner;

	public int baseSpeed;
    public int currSpeed;
	

    public int _energy = 0;
	public int energy
    {
        get{return _energy;}
        set
        {
            _energy = value;
            new UpdateEnergyCommand(owner.UniqueCreatureID, energy).AddToQueue();
        }
    }

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
        currSpeed = baseSpeed;
    }

    void OnTick()
    {
        if (GlobalATB.Instance.tick && !isPaused)
		{
			energy += currSpeed;
			if (energy >= GlobalATB.Instance.globalATB)
			{
                    e_fullATB();
					
			}

		}
    }

	public void ResetTurn()
	{
		energy = 0;
		e_turnEnd(owner);
        e_ResetCD();
	}

	public void modifyATB (int value)
	{
		energy += value;
	}

}
