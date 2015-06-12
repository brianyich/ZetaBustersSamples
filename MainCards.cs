using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class MainCards : MonoBehaviour {

		public static MainCards instance;
		public GameObject moveDescription;
		public GameObject attackDescription;
		//move + attack buttons
		public Image moveImage;
		public Image attackImage; 
		public Image APImage;
		private bool moveEnabled = true;
		private bool attackEnabled = true;
		public Button moveButtonComponent;
		public Button attackButtonComponent;

		public Text moveName;
		public Text moveDescriptionText;
		public Text attackName;
		public Text attackDescriptionText;
		
		private bool buttonClicked;
		private bool b_DisplayActive;
		
		public Text moveNotEnough;
		public Text attNotEnough;
		

		public void Initialize(){
			instance = this;
			if(UnitManager.instance.b_TutorialLevel == false){
				SetMovement(true);
				SetAttack(true);
			}
		}

		void FixedUpdate(){
			if(InitializationSystem.instance.Initialized())
			{
				if (UnitManager.instance.GetCurrent ().GetTeam () == Team.Player) {
					moveName.text = UnitManager.instance.GetCurrent ().GetMoveName();
					moveDescriptionText.text = UnitManager.instance.GetCurrent ().GetMoveDescription();
					attackName.text = UnitManager.instance.GetCurrent ().GetAttackName ();
					attackDescriptionText.text = UnitManager.instance.GetCurrent ().GetAttackDescription ();
				}
				if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() == 0){
					moveImage.color = Color.grey;
					moveButtonComponent.enabled = false;
					attackImage.color = Color.grey;
					attackButtonComponent.enabled = false;
					moveNotEnough.enabled = true;
					attNotEnough.enabled = true;
				}else{
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
			
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup> ().alpha == 1.0f && moveEnabled == true) {
				UIManager.instance.unitDisplay.SetActive (false);
				moveDescription.GetComponent<CanvasGroup> ().alpha = 1.0f;
				AudioManager.instance.MainHoverSound (2);
				if(!buttonClicked){
					//shows move grid
					UnitManager.instance.GetCurrent ().Move ();
				}
				if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() != 0){
					UIManager.instance.MoveHoverAnim(true);
					UIManager.instance.HoverActionPoints ();
				}
			}
		}

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
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup> ().alpha == 1.0f && attackEnabled == true) {
				UIManager.instance.unitDisplay.SetActive (false);
				AudioManager.instance.MainHoverSound (1);
				attackDescription.GetComponent<CanvasGroup> ().alpha = 1.0f;
				if(!buttonClicked){
					//shows attack grid
					UnitManager.instance.GetCurrent ().Attack ();
				}
				if(UnitManager.instance.GetCurrent ().GetCurrentActionPoints() != 0){
					UIManager.instance.HoverActionPoints ();
					UIManager.instance.AttackHoverAnim(true);
				}
			}
			
		}

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