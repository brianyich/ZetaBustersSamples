/*
	This script is for the Action Display that animates down
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class ActionDisplay : MonoBehaviour {
	
		public static ActionDisplay instance;
		public Image actionDisplayBG;
		public Text actionText;
		public Animator actionAnimator;
		public Sprite attack;
		public Sprite heal;
		public Sprite boost;
		
		public void Initialize(){
			instance = this;
		}
		
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
		
		public void ShowActionDisplay(bool b){
			if(b){
				actionAnimator.SetBool("ActionSlideBool", true);
			}else{
				actionAnimator.SetBool("ActionSlideBool", false);
			}
		}
	}
}