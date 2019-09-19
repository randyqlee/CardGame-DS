using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PackOpeningScreen : MonoBehaviour
{

    //DS TODO: move to GameManager
    public TutorialState tutorialState = TutorialState.ZERO;


    public static PackOpeningScreen Instance;

    public GameObject tapToExitScreen;

    public GameObject messagePanel;
    public Text messagePanelText;

    public Transform TOP_Panel;
    public Transform BOTTOM_Panel;

    public Button finishedButton;


    public GameObject ScreenContent;
    public GameObject PackPrefab;
    public Transform PacksParent;

    public Transform initialPackPosition;

    public Transform InitialPackSpot;
    // Start is called before the first frame update

    public GameObject CreatureCardPrefab;

    public List<CardAsset> creatures;

    public List<Transform> slots;

    public List<GameObject> CardsFromPackCreated;

    public bool isFinished = false;

    public GameObject Visual;
    public GameObject turnManager;
    public GameObject Logic;

    public Player playerTOP;
    public Player playerLOW;

    bool firstAtkFinished = false;
    bool enemyAtkFinished = false;

    bool firstRoundReset = false;


    void Start()
    {
        Instance = this;
        messagePanel.SetActive(true);
        messagePanel.transform.localScale = new Vector3 (1f,0f,1f);
        StartCoroutine(StartSequence());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator StartSequence()
    {

        yield return StartCoroutine(GivePack());

            Visual.SetActive(true);
            turnManager.SetActive(true);

            TurnManager.Instance.e_ResetRound += NewRound;
            TurnManager.Instance.e_GameOver += GameOver;



            Destroy(finishedButton);
            ScreenContent.SetActive(false);
            messagePanel.SetActive(false);
        

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(StartBattleTutorial());



        
    }

    void GameOver()
    {
        StartCoroutine (GameOverSequence());

    }

    IEnumerator GameOverSequence()
    {
        if(Player.Players[0].gameIsOver)
        {
        TutorialPopupManager.Instance.popupMessages[7].SetActive(true);
        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        TutorialPopupManager.Instance.popupMessages[7].SetActive(false);
        }

        else Debug.Log("Try Again");

        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));
        Application.Quit();

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

    public IEnumerator GivePack()
    {
        GameObject newPack = Instantiate(PackPrefab, PacksParent);

        newPack.transform.localPosition = initialPackPosition.transform.localPosition;
        //newPack.transform.localScale = Vector3.zero;

        newPack.transform.DOMove(PacksParent.transform.localPosition, 1f).SetEase(Ease.InQuad);

        newPack.transform.DOScale(1.5f,1f);
        yield return new WaitForSeconds(1f);

        newPack.transform.DOPunchScale(new Vector3(0.3f,0.3f,0f), 0.3f, 1, 0f);
        SetPanelMessage("This is your Welcome Pack, open it and get your Reward!", TOP_Panel);
 
        newPack.GetComponent<ScriptToOpenOnePack>().AllowToOpenThisPack();

        do
        {
            yield return null;            
        }
        while (!TapToExit.Instance.isTapped);

                for(int i=CardsFromPackCreated.Count-1; i>=0; i--)
                {
                    Destroy(CardsFromPackCreated[i]);
                }


    }

    void SwitchToAI()
    {
        Destroy(Player.Players[0].gameObject.GetComponent<PlayerTurnMaker>());
        Player.Players[0].gameObject.AddComponent<AITurnMaker>();

    }

    public void OpenPack()
    {

        StartCoroutine(ShowCardsFromPack());

        

        
    }

    IEnumerator ShowCardsFromPack()
    {
        messagePanel.transform.DOScaleY(0f,1f);

        int i = 0;
        foreach(CardAsset ca in creatures)
        {
            GameObject card;
            card = Instantiate(CreatureCardPrefab) as GameObject;

            card.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            OneCardManager manager = card.GetComponent<OneCardManager>();
            manager.cardAsset = ca;
            manager.ReadCardFromAsset();

            CardsFromPackCreated.Add(card);

            card.transform.position = InitialPackSpot.position;
            card.transform.DOMove(slots[i].position, 0.5f);
            card.transform.DOScale(1.5f, 0.5f);
            i++;
        }
        yield return new WaitForSeconds(1f);

        SetPanelMessage("Congratulations! You now have your first 3 heroes to fight with you", TOP_Panel);
        finishedButton.gameObject.transform.localScale = Vector3.zero;   
        yield return new WaitForSeconds(2f);

        tapToExitScreen.SetActive(true);


        //finishedButton.gameObject.SetActive(true);
        
        //finishedButton.gameObject.transform.DOScale(1f,1f);

        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));

        Sequence s = DOTween.Sequence();
        s.AppendInterval(1f);
        s.OnComplete(() =>
        {
            
            StopCoroutine("ShowCardsFromPack");
        }); 

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

    public void IsFinished()
    {
        isFinished = true;

    }


}
