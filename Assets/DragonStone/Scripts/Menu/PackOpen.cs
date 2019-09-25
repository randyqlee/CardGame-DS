using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PackOpen : MonoBehaviour
{
    public static PackOpen Instance;


    public GameObject PackPrefab;
    public Transform PacksParent;

    public Transform initialPackPosition;


    public GameObject CreatureCardPrefab;

    public List<CardAsset> creatures;

    public List<Transform> slots;

    public List<GameObject> CardsFromPackCreated;
    public bool packOpened = false;

    public bool isFinished = false;

    public string message;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {

        StartCoroutine(PackOpenSequence());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PackOpenSequence()
    {

        yield return (StartCoroutine(GivePack()));

    }

    IEnumerator GivePack()
    {
        GameObject newPack = Instantiate(PackPrefab, PacksParent);

        newPack.transform.localPosition = initialPackPosition.transform.localPosition;
        //newPack.transform.localScale = Vector3.zero;

        newPack.transform.DOMove(PacksParent.transform.localPosition, 1f).SetEase(Ease.InQuad);

        newPack.transform.DOScale(1.5f,1f);
        yield return new WaitForSeconds(1f);

        newPack.transform.DOPunchScale(new Vector3(0.3f,0.3f,0f), 0.3f, 1, 0f);
         
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

                isFinished = true;


    }

    public void OpenPack()
    {
        StartCoroutine(ShowCardsFromPack());
        packOpened = true;
    }

    IEnumerator ShowCardsFromPack()
    {
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

            card.transform.position = PacksParent.position;
            card.transform.DOMove(slots[i].position, 0.5f);
            card.transform.DOScale(1.5f, 0.5f);
            i++;
        }
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(2f);


        yield return StartCoroutine(TapToExit.Instance.ListenForTap(true));

        Sequence s = DOTween.Sequence();
        s.AppendInterval(1f);
        s.OnComplete(() =>
        {
            
            StopCoroutine("ShowCardsFromPack");
        }); 

    }
}
