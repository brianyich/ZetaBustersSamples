/*
	Brian Yich 2015
	This script is for the popup stat display that comes up when middle clicking on a unit.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class UnitDisplay : MonoBehaviour {
	
		//Singleton
		private static UnitDisplay _instance;
		public static UnitDisplay instance
		{
			get
			{
				if (_instance == null)
					_instance = GameObject.FindObjectOfType<UnitDisplay>();
				return _instance;
			}
		}
		
		//UI elements for stat display
		public Image charPortrait;
		public Text charName;
		public Text charATT;
		public Text charDEF;
		public Text charDOD;
		public Text charMOV;
		public Text passiveName;
		public Text passiveDescription;
		public Text passiveName2;
		public Text passiveDescription2;
		public Image hpBar;
		
		//stat boost UI elements
		public Text attBoost;
		public Text defBoost;
		public Text dodBoost;
		public Text movBoost;
		
		//popup display if hovering on UI elements within stat display
		public GameObject hoverBox;
		public Text boxDescription;
		
		//bellbotula specific element
		public GameObject questionMarks;
		
		private int temp;
		private Unit tempUnit;
		
		public void SetDisplay(Unit u){
			//if the unit you're looking at is bellbotula, every stat is a mystery
			if(u.GetUnitType() == UnitType.Bellbotula){
				questionMarks.SetActive(true);
				charPortrait.sprite = u.GetPortrait ();
				charName.text = u.name;
				hpBar.fillAmount = ((float)u.GetStatMaxHealth()/(float)u.GetStatMaxHealth());
				charATT.text = "??";
				charDEF.text = "??";
				charMOV.text = "??";
				charDOD.text = "??";
				passiveName.text = u.GetAbility1Name();
				passiveDescription.text = u.GetAbility1Description();
				passiveName2.text = u.GetAbility2Name();
				passiveDescription2.text = u.GetAbility2Description ();
				attBoost.text = "";
				defBoost.text = "";
				movBoost.text = "";
				dodBoost.text = "";
				boxDescription.text = "???";
			}
			//for any other unit
			else{
				//updates all the stat display stuff
				questionMarks.SetActive(false);
				charPortrait.sprite = u.GetPortrait ();
				charName.text = u.name;
				hpBar.fillAmount = ((float)u.GetCurrentHealth()/(float)u.GetStatMaxHealth());
				charATT.text = u.GetStatBaseAttack().ToString ();
				charDEF.text = u.GetStatBaseDefense ().ToString ();
				charMOV.text = u.GetStatBaseMovement ().ToString ();
				passiveName.text = u.GetAbility1Name();
				passiveDescription.text = u.GetAbility1Description();
				passiveName2.text = u.GetAbility2Name();
				passiveDescription2.text = u.GetAbility2Description ();
				boxDescription.text = u.GetCurrentHealth().ToString() + "/" + u.GetStatMaxHealth().ToString() + " HP";
				
				//checks if boosts are negative or positive
				//red for negative, green for positive
				if(u.GetStatAttack() != u.GetStatBaseAttack ()){
					temp = u.GetStatAttack () - u.GetStatBaseAttack ();
					if(temp > 0){
						attBoost.color = Color.green;
						attBoost.text = "+" + temp.ToString ();
					}else{
						attBoost.color = Color.red;
						attBoost.text = temp.ToString ();
					}
				}
				//if no boost, it doesn't display anything
				else{
					attBoost.text = "";
				}
				if(u.GetStatDefense() != u.GetStatBaseDefense()){
					temp = u.GetStatDefense () - u.GetStatBaseDefense ();
					if(temp > 0){
						defBoost.color = Color.green;
						defBoost.text = "+" + temp.ToString ();
					}else{
						defBoost.color = Color.red;
						defBoost.text = temp.ToString ();
					}
				}else{
					defBoost.text = "";
				}
				
				if(u.GetStatMove() != u.GetStatBaseMovement ()){
					temp = u.GetStatMove () - u.GetStatBaseMovement ();
					if(temp > 0){
						movBoost.color = Color.green;
						movBoost.text = "+" + temp.ToString ();
					}else{
						movBoost.color = Color.red;
						movBoost.text = temp.ToString ();
					}
				}else{
					movBoost.text = "";
				}
				if(u.GetTeam () == Team.Player){
					charDOD.text = u.GetStatBaseDodge ().ToString () + "%";
					if(u.GetStatDodge() != u.GetStatBaseDodge ()){
						temp = u.GetStatDodge () - u.GetStatBaseDodge ();
						if(temp > 0){
							dodBoost.color = Color.green;
							dodBoost.text = "+" + temp.ToString () + "%";
						}else{
							dodBoost.color = Color.red;
							dodBoost.text = temp.ToString () + "%";
						}
					}else{
						dodBoost.text = "";
					}
				}else{
					charDOD.text = "0";
					dodBoost.text = "";
				}
				
			}
			//sets the temporary unit for your hover functions
			tempUnit = u;
		}
		
		//when hovering over UI element
		public void OnItemEnter(string type){
			boxDescription.text = type;
		}
		
		//when exiting UI element
		public void OnItemExit(){
			if(tempUnit.GetUnitType() == UnitType.Bellbotula){
				boxDescription.text = "???";
			}else{
				boxDescription.text = tempUnit.GetCurrentHealth().ToString() + "/" + tempUnit.GetStatMaxHealth().ToString() + " HP";
			}
		}
		
	}
}