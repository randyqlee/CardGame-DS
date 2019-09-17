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

    public GameObject messagePanel;
    public Text messagePanelText;

    public Transform TOP_Panel;
    public Transform BOTTOM_Panel;

    public Button finishedButton;


    public GameObject ScreenContent;
    public GameObject PackPrefab;
    public Transform PacksParent;

    public Transform InitialPackSpot;
    // Start is called before the first frame update

    public GameObject CreatureCardPrefab;

    public List<CardAsset> creatures;

    public List<Transform> slots;

    public List<GameObject> CardsFromPackCreated;

    public bool isFinished = false;

    public GameObject Visual;
    public GameObject TurnManager;
    public GameObject Logic;

    public Player playerTOP;
    public Player playerLOW;



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
        TurnManager.SetActive(true);

        Destroy(finishedButton);
        ScreenContent.SetActive(false);
        messagePanel.SetActive(false);

        yield return new WaitForSeconds(3f);

        yield return StartCoroutine(StartBattleTutorial());



        
    }

    public IEnumerator StartBattleTutorial()
    {

        
        

        SetPanelMessage ("Use your heroes to attack Enemies", BOTTOM_Panel);
        yield return new WaitForSeconds(2f);
        messagePanel.transform.DOScaleY(0f,1f);
        yield return new WaitForSeconds(1f);
        messagePanel.SetActive(false);
        SetPanelMessage ("Deal enough damage to defeat all enemies and you win!", TOP_Panel);

        yield return null;
    }

    public IEnumerator GivePack()
    {
        GameObject newPack = Instantiate(PackPrefab, PacksParent);

        newPack.transform.localPosition = PacksParent.transform.localPosition;
        newPack.transform.localScale = Vector3.zero;

        newPack.transform.DOScale(1.5f,1f);
        yield return new WaitForSeconds(1f);
        SetPanelMessage("This is your Welcome Pack, open it and get your Reward!", TOP_Panel);
 
        newPack.GetComponent<ScriptToOpenOnePack>().AllowToOpenThisPack();

        do
        {
            yield return null;            
        }
        while (!isFinished);


        if(isFinished)
            {

                for(int i=CardsFromPackCreated.Count-1; i>=0; i--)
                {
                    Destroy(CardsFromPackCreated[i]);
                }


                
            }

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
        finishedButton.gameObject.SetActive(true);
        
        finishedButton.gameObject.transform.DOScale(1f,1f);

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
