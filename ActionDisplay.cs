/*
	Brian Yich, 2015
	This script is for the Action Display that animates down upon any unit attack or skill.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class ActionDisplay : MonoBehaviour {
		
		//Singleton
		private static ActionDisplay _instance;
		public static ActionDisplay instance
		{
			get
			{
				if (_instance == null)
					_instance = GameObject.FindObjectOfType<ActionDisplay>();
				return _instance;
			}
		}
		
		
		public Image actionDisplayBG;
		public Text actionText;
		public Animator actionAnimator;
		
		//sprites to swap in, different displays for different types of abilities
		public Sprite attack;
		public Sprite heal;
		public Sprite boost;
		
		//swaps the action display based on type of ability
		public void SetActionDisplay(CardType type, string cardName){
			if(type == CardType.Attack){
				actionDisplayBG.sprite = attack;
			}else if(type == CardType.Boost){
				actionDisplayBG.sprite = boost;
			}else if(type == CardType.Heal){
				actionDisplayBG.sprite = heal; 
			}
			actionText.text = cardName;
		}
		
		//action display slide in animation on true, slide out on false
		public void ShowActionDisplay(bool b){
			if(b){
				actionAnimator.SetBool("ActionSlideBool", true);
			}else{
				actionAnimator.SetBool("ActionSlideBool", false);
			}
		}
	}
}