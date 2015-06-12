using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class UnitDisplay : MonoBehaviour {
	
		public static UnitDisplay instance;
		
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
		
		public Text attBoost;
		public Text defBoost;
		public Text dodBoost;
		public Text movBoost;
		public Image hpBar;
		public GameObject hoverBox;
		public Text boxDescription;
		public GameObject questionMarks;
		
		private int temp;
		private Unit tempUnit;
		
		public void Initialize(){
			instance = this;
		}
		
		public void SetDisplay(Unit u){
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
			}else{
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
				if(u.GetStatAttack() != u.GetStatBaseAttack ()){
					temp = u.GetStatAttack () - u.GetStatBaseAttack ();
					if(temp > 0){
						attBoost.color = Color.green;
						attBoost.text = "+" + temp.ToString ();
					}else{
						attBoost.color = Color.red;
						attBoost.text = temp.ToString ();
					}
				}else{
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
			tempUnit = u;
		}
		
		public void OnItemEnter(string type){
			boxDescription.text = type;
		}
		public void OnItemExit(){
			if(tempUnit.GetUnitType() == UnitType.Bellbotula){
				boxDescription.text = "???";
			}else{
				boxDescription.text = tempUnit.GetCurrentHealth().ToString() + "/" + tempUnit.GetStatMaxHealth().ToString() + " HP";
			}
		}
		
	}
}