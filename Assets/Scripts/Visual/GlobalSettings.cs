﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GlobalSettings: MonoBehaviour 
{
    [Header("Players")]
    public Player TopPlayer;
    public Player LowPlayer;
    public int HeroesCount;

    public int HeroesEquipCount;

    [Header("Colors")]
    public Color32 CardBodyStandardColor;
    public Color32 CardRibbonsStandardColor;
    public Color32 CardGlowColor;
    [Header("Numbers and Values")]
    public float CardPreviewTime = 1f;
    public float CardTransitionTime= 1f;
    public float CardPreviewTimeFast = 0.2f;
    public float CardTransitionTimeFast = 0.5f;
    public float MessageTime = 0.2f;


    [Header("Prefabs and Assets")]
    public GameObject NoTargetSpellCardPrefab;
    public GameObject TargetedSpellCardPrefab;
    public GameObject CreatureCardPrefab;
    public GameObject CreaturePrefab;
    public GameObject DamageEffectPrefab;
    public GameObject SpecialEffectPrefab;
    public GameObject ExplosionPrefab;

    public GameObject AbilityPreviewPrefab;

    public GameObject PortraitWSkillsPreviewPrefab;

    public GameObject MulliganCreaturePrefab;
    public GameObject TOP_MulliganCreaturePrefab;

//DS
//    public GameObject SfxExplosion_1_Prefab;

//    public GameObject SfxIMplosion_1_Prefab;

    public GameObject FloatingTweenText_Prefab;

    public GameObject SkillCardPreview_Prefab;
    public GameObject AbilityCardPreviewPrefab;


    public List<GameObject> SFX_Attack_Prefab;
    public List<GameObject> SFX_TakeDamage_Prefab;
    public List<GameObject> SFX_UseSkill_Prefab;


    [Header("Other")]
    public Button EndTurnButton;
    //public CardAsset CoinCard;
    public GameObject GameOverPanel;
    //public Sprite HeroPowerCrossMark;

    public Dictionary<AreaPosition, Player> Players = new Dictionary<AreaPosition, Player>();


    // SINGLETON
    public static GlobalSettings Instance;

    void Awake()
    {
        Players.Add(AreaPosition.Top, TopPlayer);
        Players.Add(AreaPosition.Low, LowPlayer);
        Instance = this;
    }

    public bool CanControlThisPlayer(AreaPosition owner)
    {
        bool PlayersTurn = (TurnManager.Instance.whoseTurn == Players[owner]);
        bool NotDrawingAnyCards = !Command.CardDrawPending();
        return Players[owner].PArea.AllowedToControlThisPlayer && Players[owner].PArea.ControlsON && PlayersTurn && NotDrawingAnyCards;
    }

    public bool CanControlThisPlayer(Player ownerPlayer)
    {
        bool PlayersTurn = (TurnManager.Instance.whoseTurn == ownerPlayer);
        bool NotDrawingAnyCards = !Command.CardDrawPending();
        return ownerPlayer.PArea.AllowedToControlThisPlayer && ownerPlayer.PArea.ControlsON && PlayersTurn && NotDrawingAnyCards;
    }

    public void EnableEndTurnButtonOnStart(Player P)
    {
//DS        //if (P == LowPlayer && CanControlThisPlayer(AreaPosition.Low) ||
        //    P == TopPlayer && CanControlThisPlayer(AreaPosition.Top))
            //EndTurnButton.interactable = true;
        //else
            //EndTurnButton.interactable = false;
            
    }
}
