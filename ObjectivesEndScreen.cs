/*
	Brian Yich 2015
	This script is for setting the end of battle objectives screen to match the pause screen objectives.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class ObjectivesEndScreen : MonoBehaviour {
		
		//Singleton
		private static ObjectivesEndScreen _instance;
		public static ObjectivesEndScreen instance
		{
			get
			{
				if (_instance == null)
					_instance = GameObject.FindObjectOfType<ObjectivesEndScreen>();
				return _instance;
			}
		}
		
		//objective UI elements
		public GameObject[] objectives;
		public Text[] objectiveTitles;
		public Text[] objectiveDescriptions;
		public GameObject[] objectiveOptional;
		public Image[] progress;
		
		
		public void SetEndScreenObjectives(int objectivesCount){
			//for each objective there is
			for(int i = 0; i < objectivesCount; i++){
				objectives[i].SetActive(true);
				
				//updates objective name, description, progress, and if the objective was optional
				objectives[i].GetComponent<Image>().sprite = VictoryPauseConditions.instance.objectiveBG[i].sprite;
				objectiveTitles[i].text = VictoryPauseConditions.instance.objTitle[i].text;
				objectiveDescriptions[i].text = VictoryPauseConditions.instance.objDesc[i].text;
				progress[i].sprite = VictoryPauseConditions.instance.progress[i].sprite;
				if(VictoryPauseConditions.optionalBool[i] == true){
					objectiveOptional[i].SetActive(true);
				}
			}
		}
	}
}