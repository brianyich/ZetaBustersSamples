/*
	Brian Yich 2015
	This script is for the Move / Attack disk sections on the HUD.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class MainCards : MonoBehaviour {
		
		//Singleton
		private static MainCards _instance;
		public static MainCards instance
		{
			get
			{
				if (_instance == null)
					_instance = GameObject.FindObjectOfType<MainCards>();
				return _instance;
			}
		}
		
		//for move / attack hover descriptions
		public GameObject moveDescription;
		public GameObject attackDescription;
		public Text moveName;
		public Text moveDescriptionText;
		public Text attackName;
		public Text attackDescriptionText;
		
		//pops up if not enough action points
		public Text moveNotEnough;
		public Text attNotEnough;
		
		//move + attack buttons
		public Image moveImage;
		public Image attackImage; 
		public Button moveButtonComponent;
		public Button attackButtonComponent;

		private bool buttonClicked;
		private bool b_DisplayActive;
		private bool moveEnabled = true;
		private bool attackEnabled = true;
		public Image APImage;
		
		
		public void Initialize(){
			if(UnitManager.instance.b_TutorialLevel == false){
				SetMovement(true);
				SetAttack(true);
			}
		}

		void FixedUpdate(){
			if(InitializationSystem.instance.Initialized())
			{
				//updates your move / attack description hovers each turn
				if (UnitManager.instance.GetCurrent ().GetTeam () == Team.Player) {
					moveName.text = UnitManager.instance.GetCurrent ().GetMoveName();
					moveDescriptionText.text = UnitManager.instance.GetCurrent ().GetMoveDescription();
					attackName.text = UnitManager.instance.GetCurrent ().GetAttackName ();
					attackDescriptionText.text = UnitManager.instance.GetCurrent ().GetAttackDescription ();
				}
				//disable buttons if out of action points
				if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() == 0){
					moveImage.color = Color.grey;
					moveButtonComponent.enabled = false;
					attackImage.color = Color.grey;
					attackButtonComponent.enabled = false;
					moveNotEnough.enabled = true;
					attNotEnough.enabled = true;
				}
				//enable buttons if there are action points
				else{
					moveImage.color = Color.white;
					moveButtonComponent.enabled = true;
					attackImage.color = Color.white;
					attackButtonComponent.enabled = true;
					moveNotEnough.enabled = false;
					attNotEnough.enabled = false;
				}
			}
		}

		public void OnMoveEnter(){
			//if user is currently able to move
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup> ().alpha == 1.0f && moveEnabled == true) {
				UIManager.instance.unitDisplay.SetActive (false);
				
				//show hover description 
				moveDescription.GetComponent<CanvasGroup> ().alpha = 1.0f;
				
				AudioManager.instance.MainHoverSound (2);
				if(!buttonClicked){
					//previews where user can move
					UnitManager.instance.GetCurrent ().Move ();
				}
				//if user has enough action points
				if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() != 0){
					//hover animation
					UIManager.instance.MoveHoverAnim(true);
					
					//action points-- preview
					UIManager.instance.HoverActionPoints ();
				}
			}
		}

		//resets hover stuff if exiting from hover
		public void OnMoveExit(){
			UIManager.instance.MoveHoverAnim(false);
			moveDescription.GetComponent<CanvasGroup> ().alpha = 0f;
			if(!buttonClicked){
				UnitManager.instance.GetCurrent ().SetState (UnitState.Idle);
				Tile.ClearAll();
			}
			
			if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() != 0){
				UIManager.instance.ExitActionPoints ();
			}
		}

		public void OnAttackEnter(){
			//if user is currently able to attack
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup> ().alpha == 1.0f && attackEnabled == true) {
				UIManager.instance.unitDisplay.SetActive (false);
				
				//show attack description
				attackDescription.GetComponent<CanvasGroup> ().alpha = 1.0f;
				
				if(!buttonClicked){
					//shows attack grid
					UnitManager.instance.GetCurrent ().Attack ();
				}
				//if user has enough action points
				if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() != 0){
					//button hover animation
					UIManager.instance.HoverActionPoints ();
					
					//action points-- preview
					UIManager.instance.AttackHoverAnim(true);
				}
			}
			
		}

		//resets hover stuff if exiting from hover
		public void OnAttackExit(){
			UIManager.instance.AttackHoverAnim(false);
			attackDescription.GetComponent<CanvasGroup> ().alpha = 0f;
			if(!buttonClicked){
				UnitManager.instance.GetCurrent ().SetState (UnitState.Idle);
				Tile.ClearAll();
			}
			if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() != 0){
				UIManager.instance.ExitActionPoints ();
			}
		}
		
		//button animations
		public void OnMoveDown(){
			UIManager.instance.mainAnimator.SetBool ("MoveBool", true);
		}
		public void OnMoveUp(){
			UIManager.instance.mainAnimator.SetBool ("MoveBool", false);
		}
		public void OnAttackDown(){
			UIManager.instance.mainAnimator.SetBool ("AttackBool", true);
		}
		public void OnAttackUp(){
			UIManager.instance.mainAnimator.SetBool ("AttackBool", false);
		}
		
		//setters / getters
		public void SetClicked(bool b){
			buttonClicked = b;
		}
		public void SetMovement(bool b)
		{
			moveEnabled = b;
			moveImage.enabled = b;
		}
		public void SetAttack(bool b)
		{
			attackEnabled = b;
			attackImage.enabled = b;
		}
		public bool MovementButtonActive()
		{
			return moveEnabled;
		}
		public bool AttackButtonActive()
		{
			return attackEnabled;
		}
		public void SetAPPointer(bool b)
		{
			APImage.gameObject.SetActive(b);
		}
		public void SetDisplayActive(bool b)
		{
			b_DisplayActive = b;
			if(b)
				GetComponent<CanvasGroup>().alpha = 1.0f;
			else
				GetComponent<CanvasGroup>().alpha = 0.0f;
		}
		public bool DisplayActive()
		{
			return b_DisplayActive;
		}
	}
}