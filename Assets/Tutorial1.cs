using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Tutorial1 : MonoBehaviour
{
    public TutorialState tutorialState = TutorialState.ZERO;
    public static Tutorial1 Instance;
    public GameObject packOpenPrefab;
    public GameObject tapToExitScreen;
    public GameObject messagePanel;
    public Text messagePanelText;

    public Transform TOP_Panel;
    public Transform BOTTOM_Panel;

    public GameObject ScreenContent;

    public bool isFinished = false;

    public GameObject Visual;
    public GameObject turnManager;
    public GameObject Logic;

    public Player playerTOP;
    public Player playerLOW;

    bool firstAtkFinished = false;
    bool enemyAtkFinished = false;

    bool firstRoundReset = false;


    void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        messagePanel.SetActive(true);
        messagePanel.transform.localScale = new Vector3 (1f,0f,1f);
        StartCoroutine(StartSequence());
        
    }

    IEnumerator StartSequence()
    {
        SetPanelMessage("This is your Welcome Pack, open it and get your Reward!", TOP_Panel);

        yield return StartCoroutine(PackOpening());

        

        yield return new WaitForSeconds(1f);

            Visual.SetActive(true);
            turnManager.SetActive(true);

            TurnManager.Instance.e_ResetRound += NewRound;
            TurnManager.Instance.e_GameOver += GameOver;

            ScreenContent.SetActive(false);
            messagePanel.SetActive(false);
        

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(StartBattleTutorial());
       
    }

    IEnumerator PackOpening()
    {

        GameObject go = Instantiate(packOpenPrefab);

        yield return new WaitForSeconds (2f);

        

        do
        {
            yield return null;
        }
        while (!go.GetComponent<PackOpen>().packOpened);

        SetPanelMessage("Congratulations! You now have your first 3 heroes to fight with you", TOP_Panel);

        do
        {
            yield return null;
        }
        while (!go.GetComponent<PackOpen>().isFinished);

           
        StopCoroutine("PackOpening");
    }


    public void SetPanelMessage (string message, Transform position)
    {
        StartCoroutine (MessageRoutine(message, position));
        StopCoroutine("MessageRoutine");
    }

    IEnumerator MessageRoutine(string message, Transform position)
    {

        messagePanel.transform.position = position.position;


        if (messagePanel.activeSelf == false)
        {
            messagePanel.SetActive(true);
            messagePanel.transform.localScale = new Vector3(1f,0f,1f);
        }
        
        else
        {
            messagePanel.transform.DOScaleY(0f,1f);
            yield return new WaitForSeconds(1f);
        }

        messagePanelText.text = message;
        messagePanel.transform.DOScaleY(1f,1f);
        yield return new WaitForSeconds(1f);

    }

    void GameOver()
    {
        StartCoroutine (GameOverSequence());

    }

    IEnumerator GameOverSequence()
    {

        TurnManager.Instance.e_ResetRound -= NewRound;
        TurnManager.Instance.e_GameOver -= GameOver;

        ScreenContent.SetActive(true);

        Visual.SetActive(false);
        turnManager.SetActive(false);
        
        //if player wins
        if(Player.Players[0].gameIsOver)
        {
        TutorialPopupManager.Instance.popupMessages[7].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[7].SetActive(false);

        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));

        GameObject go = Instantiate(packOpenPrefab);

        yield return new WaitForSeconds (2f);

        do
        {
            yield return null;
        }
        while (!go.GetComponent<PackOpen>().isFinished);

        yield return null;


        //reward with pack

        }

        //if player loses
        else
        {
            Debug.Log("Try Again");
            yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        }

        
        Quit();

    }

	public void Quit()
	{
		     // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
	}

    void NewRound()
    {
        
        if (!firstRoundReset)
        {
            Player.Players[0].GetComponent<AITurnMaker>().isPaused = true;
            StartCoroutine(StartNewRound());
            firstRoundReset = true;
        }
    }

    IEnumerator StartNewRound()
    {
        foreach (Player p in Player.Players)
        {
            foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            {
                if (!cl.isDead)
                {
                    GameObject go = IDHolder.GetGameObjectWithID(cl.UniqueCreatureID);
                    go.GetComponentInChildren<Draggable>().enabled = false;
                    go.GetComponentInChildren<DragCreatureAttack>().enabled = false; 
                }
            }
        }

        TutorialPopupManager.Instance.popupMessages[6].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[6].SetActive(false);


        foreach (Player p in Player.Players)
        {
            foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            {
                if (!cl.isDead)
                {
                    GameObject go = IDHolder.GetGameObjectWithID(cl.UniqueCreatureID);
                    go.GetComponentInChildren<Draggable>().enabled = true;
                    go.GetComponentInChildren<DragCreatureAttack>().enabled = true; 
                }
            }
        }

        yield return null;
        Player.Players[0].GetComponent<AITurnMaker>().isPaused = false;
    }

    public IEnumerator StartBattleTutorial()
    {
        //SetPanelMessage ("Use your heroes to attack Enemies", BOTTOM_Panel);
        //yield return StartCoroutine(TapToExit.Instance.ListenForTap());
        //messagePanel.transform.DOScaleY(0f,1f);
        //yield return new WaitForSeconds(1f);
        //messagePanel.SetActive(false);
        //SetPanelMessage ("Deal enough damage to defeat all enemies and you win!", TOP_Panel);
        //yield return StartCoroutine(TapToExit.Instance.ListenForTap());
        //messagePanel.transform.DOScaleY(0f,1f);
        //yield return new WaitForSeconds(1f);
        //messagePanel.SetActive(false);

        TutorialPopupManager.Instance.popupMessages[2].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[2].SetActive(false);
        TutorialPopupManager.Instance.popupMessages[0].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[0].SetActive(false);
        TutorialPopupManager.Instance.popupMessages[1].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[1].SetActive(false);



        //start of attack tutorial
        TutorialPopupManager.Instance.popupMessages[3].SetActive(true);
        TutorialPopupManager.Instance.popupMessages[8].SetActive(true);
        yield return StartCoroutine(HighlightCreature());
        TutorialPopupManager.Instance.popupMessages[3].SetActive(false);
        TutorialPopupManager.Instance.popupMessages[8].SetActive(false);
        //end of first attack

        //start of enemy turn
        yield return new WaitForSeconds(2f);
        TutorialPopupManager.Instance.popupMessages[4].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[4].SetActive(false);
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(EnemyAttack());
        yield return new WaitForSeconds(2f);
        //end of enemy turn

        SwitchToAI();


        //start of player 2nd turn
        TutorialPopupManager.Instance.popupMessages[5].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[5].SetActive(false);

    

        
        


    }

    IEnumerator HighlightCreature()
    {
        firstAtkFinished = false;

        
        new StartATurnCommand(Player.Players[1]).AddToQueue();
        //GameObject g = IDHolder.GetGameObjectWithID(Player.Players[1].table.CreaturesOnTable[index].UniqueCreatureID);
        //g.GetComponent<OneCreatureManager>().CanAttackNow = true;

        //disable other heroes
        GameObject g0 = IDHolder.GetGameObjectWithID(Player.Players[1].table.CreaturesOnTable[0].UniqueCreatureID);
        GameObject g2 = IDHolder.GetGameObjectWithID(Player.Players[1].table.CreaturesOnTable[2].UniqueCreatureID);
        
        g0.GetComponentInChildren<Draggable>().enabled = false;        
        g2.GetComponentInChildren<Draggable>().enabled = false;

        g0.GetComponentInChildren<DragCreatureAttack>().enabled = false;        
        g2.GetComponentInChildren<DragCreatureAttack>().enabled = false;

        CreatureLogic cl = Player.Players[1].table.CreaturesOnTable[1];

        cl.e_AfterAttacking += FirstAttackFinished;

        

        do
        {
            yield return null;

        }
        while (!firstAtkFinished);

        
        g0.GetComponentInChildren<Draggable>().enabled = true;
        g2.GetComponentInChildren<Draggable>().enabled = true;

        g0.GetComponentInChildren<DragCreatureAttack>().enabled = true;        
        g2.GetComponentInChildren<DragCreatureAttack>().enabled = true;

    }

    void FirstAttackFinished(CreatureLogic cl)
    {
        firstAtkFinished = true;        
    }

    IEnumerator EnemyAttack()
    {
        enemyAtkFinished = false;
        Player p = Player.Players[0];
        int i = Random.Range(0,p.table.CreaturesOnTable.Count);
            while (p.table.CreaturesOnTable[i].isDead || p.table.CreaturesOnTable[i].AttacksLeftThisTurn <= 0)
            {
                i = Random.Range(0,p.table.CreaturesOnTable.Count);
            }
            CreatureLogic cl = p.table.CreaturesOnTable[i];

            cl.e_AfterAttacking += EnemyAtkFinished;
            



            //foreach (CreatureLogic cl in p.table.CreaturesOnTable)
            //{
                if (cl.AttacksLeftThisTurn > 0)
                {
                    // attack a random target with a creature
                    if (p.otherPlayer.table.CreaturesOnTable.Count > 0)
                    {
                        int index = Random.Range(0, p.otherPlayer.table.CreaturesOnTable.Count);
                        while (p.otherPlayer.table.CreaturesOnTable[index].isDead){
                            index = Random.Range(0, p.otherPlayer.table.CreaturesOnTable.Count);
                        }
                        CreatureLogic targetCreature = p.otherPlayer.table.CreaturesOnTable[index];
                        cl.AttackCreatureWithID(targetCreature.UniqueCreatureID);

                    }                                    

                }
        
        

        do
        {
            yield return null;

        }
        while (!enemyAtkFinished);
    }

    void EnemyAtkFinished(CreatureLogic cl)
    {
        enemyAtkFinished = true;        
    }

    void SwitchToAI()
    {
        Destroy(Player.Players[0].gameObject.GetComponent<PlayerTurnMaker>());
        Player.Players[0].gameObject.AddComponent<AITurnMaker>();

    }
}
