using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class ObjectivesBegScreen : MonoBehaviour {
		//
		public static ObjectivesBegScreen instance;
		public GameObject[] objectives;
		public Text[] objectiveTitles;
		public Text[] objectiveDescriptions;
		public GameObject[] objectiveOptional;
		public Image[] progress;
		
		public void Initialize(){
			instance = this;
			//objectives = new GameObject[4];
			//objectiveTitles = new Text[4];
			//objectiveOptional = new GameObject[4];
			//progress = new Image[4];
		}
		
		public void SetEndScreenObjectives(int objectivesCount){
			switch(objectivesCount){
				case 1:
					ObjectivesBegScreen.instance.objectives[0].SetActive (true);
					ObjectivesBegScreen.instance.objectives[1].SetActive (false);
					ObjectivesBegScreen.instance.objectives[2].SetActive (false);
					ObjectivesBegScreen.instance.objectives[3].SetActive (false);
					if(VictoryPauseConditions.optionalBool1){
						objectiveOptional[0].SetActive (true);
					}else{
						objectiveOptional[0].SetActive (false);
					}
					objectives[0].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[0].sprite;
					objectiveTitles[0].text = VictoryPauseConditions.instance.objTitle1.text;
					objectiveDescriptions[0].text = VictoryPauseConditions.instance.objDesc1.text;
					progress[0].sprite = VictoryPauseConditions.instance.progress1.sprite;
				break;
				case 2:
					ObjectivesBegScreen.instance.objectives[0].SetActive (true);
					ObjectivesBegScreen.instance.objectives[1].SetActive (true);
					ObjectivesBegScreen.instance.objectives[2].SetActive (false);
					ObjectivesBegScreen.instance.objectives[3].SetActive (false);
					if(VictoryPauseConditions.optionalBool2){
						objectiveOptional[1].SetActive (true);
					}else{
						objectiveOptional[1].SetActive (false);
					}
					objectives[0].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[0].sprite;
					objectiveTitles[0].text = VictoryPauseConditions.instance.objTitle1.text;
					objectiveDescriptions[0].text = VictoryPauseConditions.instance.objDesc1.text;
					progress[0].sprite = VictoryPauseConditions.instance.progress1.sprite;
					objectives[1].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[1].sprite;
					objectiveTitles[1].text = VictoryPauseConditions.instance.objTitle2.text;
					objectiveDescriptions[1].text = VictoryPauseConditions.instance.objDesc2.text;
					progress[1].sprite = VictoryPauseConditions.instance.progress2.sprite;
				break;
				case 3:
					ObjectivesBegScreen.instance.objectives[0].SetActive (true);
					ObjectivesBegScreen.instance.objectives[1].SetActive (true);
					ObjectivesBegScreen.instance.objectives[2].SetActive (true);
					ObjectivesBegScreen.instance.objectives[3].SetActive (false);
					if(VictoryPauseConditions.optionalBool3){
						objectiveOptional[2].SetActive (true);
					}else{
						objectiveOptional[2].SetActive (false);
					}
					objectives[0].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[0].sprite;
					objectiveTitles[0].text = VictoryPauseConditions.instance.objTitle1.text;
					objectiveDescriptions[0].text = VictoryPauseConditions.instance.objDesc1.text;
					progress[0].sprite = VictoryPauseConditions.instance.progress1.sprite;
					objectives[1].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[1].sprite;
					objectiveTitles[1].text = VictoryPauseConditions.instance.objTitle2.text;
					objectiveDescriptions[1].text = VictoryPauseConditions.instance.objDesc2.text;
					progress[1].sprite = VictoryPauseConditions.instance.progress2.sprite;
					objectives[2].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[2].sprite;
					objectiveTitles[2].text = VictoryPauseConditions.instance.objTitle3.text;
					objectiveDescriptions[2].text = VictoryPauseConditions.instance.objDesc3.text;
					progress[2].sprite = VictoryPauseConditions.instance.progress3.sprite;
				break;
				case 4:
					ObjectivesBegScreen.instance.objectives[0].SetActive (true);
					ObjectivesBegScreen.instance.objectives[1].SetActive (true);
					ObjectivesBegScreen.instance.objectives[2].SetActive (true);
					ObjectivesBegScreen.instance.objectives[3].SetActive (true);
					if(VictoryPauseConditions.optionalBool4){
						objectiveOptional[3].SetActive (true);
					}else{
						objectiveOptional[3].SetActive (false);
					}
					objectives[0].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[0].sprite;
					objectiveTitles[0].text = VictoryPauseConditions.instance.objTitle1.text;
					objectiveDescriptions[0].text = VictoryPauseConditions.instance.objDesc1.text;
					progress[0].sprite = VictoryPauseConditions.instance.progress1.sprite;
					objectives[1].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[1].sprite;
					objectiveTitles[1].text = VictoryPauseConditions.instance.objTitle2.text;
					objectiveDescriptions[1].text = VictoryPauseConditions.instance.objDesc2.text;
					progress[1].sprite = VictoryPauseConditions.instance.progress2.sprite;
					objectives[2].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[2].sprite;
					objectiveTitles[2].text = VictoryPauseConditions.instance.objTitle3.text;
					objectiveDescriptions[2].text = VictoryPauseConditions.instance.objDesc3.text;
					progress[2].sprite = VictoryPauseConditions.instance.progress3.sprite;
					objectives[3].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[3].sprite;
					objectiveTitles[3].text = VictoryPauseConditions.instance.objTitle4.text;
					objectiveDescriptions[3].text = VictoryPauseConditions.instance.objDesc4.text;
					progress[3].sprite = VictoryPauseConditions.instance.progress4.sprite;
				break;
			}
		}
	}
}