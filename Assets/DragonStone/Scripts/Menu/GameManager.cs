using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject titleScreen;

    public GameObject tutorialSplashScreen;

    public GameObject tutorialMenu;

    public GameObject sceneReloader;

    public TutorialState tutorialState = TutorialState.ZERO;

       
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {

        //Start game sequence using coroutines
        //1. Load Playerprefs data / CHeck online profile
        //2. Check if all components are complete
        //3. Check if player has completed tutorial
        //  -- go to tutorial scene
        //  --  -- go to MenuScene only if tutorial is complete




        StartCoroutine(StartGameSequence());
        
    }


    IEnumerator StartGameSequence()
    {

        yield return StartCoroutine(StartAnimation());

        //yield return StartCoroutine(LoadPlayerData());




        yield return null;

    }



    IEnumerator StartAnimation()
    {

        //call return after Dotween oncomplete

        GameObject go = titleScreen.GetComponent<TitleScreen>().titleText;

        go.transform.DOMove(titleScreen.GetComponentInChildren<Canvas>().transform.position, 1f);

            Sequence s = DOTween.Sequence();
            s.AppendInterval(3f);
            s.OnComplete(() =>
            {
                StartCoroutine(LoadPlayerData());
            }
            );

            yield return null;
    }

    IEnumerator LoadPlayerData()
    {
        if (PlayerPrefs.HasKey("IsTutorialFinished"))
        {
            //go straight to MenuScene

            sceneReloader.GetComponent<SceneReloader>().LoadScene("MenuScene");
        }

        else
        {

            //check status of Tutorial or progress, then start from there

            Debug.Log ("Start Tutorial Scene");
            //proceed with tutorial scene

            StartCoroutine(StartTutorialScreen());

        }

        yield return null;

    }

    IEnumerator StartTutorialScreen()
    
    {
        titleScreen.SetActive(false);
        tutorialSplashScreen.SetActive(true);

        GameObject go = tutorialSplashScreen.GetComponent<TutorialSplashScreen>().text;

        go.GetComponent<Text>().text = "Welcome to this game!";
        

        Sequence s = DOTween.Sequence();
        

        s.Append(go.transform.DOScaleY(1,1f));

        s.AppendInterval(1f);

        s.Append(go.transform.DOScaleY(0,1f));

        s.AppendInterval(1f);

        s.AppendCallback(() =>
        {
            go.GetComponent<Text>().text = "Your journey begins NOW!";
            
            

        }
        );

        s.Append(go.transform.DOScaleY(1,1f));

        s.AppendInterval(1f);



        s.OnComplete(() =>
        {
            Debug.Log("FINISHED!");

            sceneReloader.GetComponent<SceneReloader>().LoadScene("MenuScene");
        }
        );



        

        yield return null;

    }

    
}
