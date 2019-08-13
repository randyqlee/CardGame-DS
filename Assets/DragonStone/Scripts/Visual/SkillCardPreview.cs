using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SkillCardPreview : MonoBehaviour {

	// Use this for initialization

	public OneCardManager cm;

	public Vector3 endValue = new Vector3(-5,0,0);
	public Vector3 endValue2 = new Vector3(5,0,0);
	
	public float tweenDuration;
	public float moveDuration = 0.5f;

	public float delayBeforeDestroy;
	public float previewCardSize;


	// Use this for initialization

	void Awake()
	{
		cm = GetComponent<OneCardManager>();
	}
	void Start () {


		
	}
	
	// Update is called once per frame
	public void SetupPreview(CreatureEffect ce)
	{
		if (ce.Name != null)
		cm.NameText.text = ce.Name;

		cm.ManaCostText.text = ce.creatureEffectCooldown.ToString();
		
		if (ce.Name != null)
		cm.DescriptionText.text = ce.Name;

		cm.DescriptionText.text = ce.abilityDescription.ToString();

		if (ce.abilityPreviewSprite != null)
		cm.CardGraphicImage.sprite = ce.abilityPreviewSprite;
	}

	public void Move ()
	{



		//transform.DOMove(new Vector3(transform.position.x + endValue.x, transform.position.y + endValue.y, transform.position.z + endValue.z), tweenDuration, false);
		transform.DOLocalMove(endValue,moveDuration);
		transform.DOScale(previewCardSize, tweenDuration);
		//Sequence s = DOTween.Sequence();
                //s.AppendInterval(delay);
                //s.OnComplete(Command.CommandExecutionComplete);
				//s.OnComplete(Destroy(gameObject));

		Destroy(gameObject,delayBeforeDestroy);
		
	}

	void OnDestroy()
	{
		Command.CommandExecutionComplete();
	}

}
