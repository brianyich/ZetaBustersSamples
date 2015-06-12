/*
	This script is for the Pause Menu functionality.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{

	public class PauseMenu : MonoBehaviour {
		
		//pause menu objectives
		public VictoryPauseConditions conditionsScript;
		
		//different displays
		public GameObject pauseMenu;
		public GameObject optionsMenu;
		public GameObject restartConfirm;
		public GameObject quitConfirm;
		
		//buttons for music player
		public CanvasGroup prevButton;
		public CanvasGroup nextButton;
		public Text currentSong;
		
		public void Initialize(){
			conditionsScript.Initialize ();
		}
		
		//resumes the game
		public void OnResumeClick(){
			UIManager.instance.SetPauseMenu (false);
			if(UnitManager.instance.GetCurrent() != null)
				UIManager.instance.SetCanvasActive (true);
			Time.timeScale = 1;
			AudioManager.instance.CancelSound ();
		}
		
		//brings up the restart confirm menu
		public void OnRestartClick(){
			restartConfirm.SetActive (true);
			pauseMenu.SetActive (false);
			AudioManager.instance.PauseClickSound ();
		}
		
		//restarts the game
		public void OnRestartConfirmClick(){
			BeginningScreenCanvas.instance.loadPanel.SetActive(true);
			AkSoundEngine.StopAll ();
			Application.LoadLevel (Application.loadedLevel);
		}
		
		//brings up the options menu
		public void OnOptionClick(){
			pauseMenu.SetActive (false);
			optionsMenu.SetActive (true);
			AudioManager.instance.PauseClickSound ();
		}
		
		//back to the pause menu
		public void OnBackClick(){
			optionsMenu.SetActive (false);
			quitConfirm.SetActive (false);
			restartConfirm.SetActive (false);
			pauseMenu.SetActive (true);
			AudioManager.instance.CancelSound ();
		}
		
		//brings up exit confirm menu
		public void OnExitClick(){
			quitConfirm.SetActive (true);
			pauseMenu.SetActive (false);
			AudioManager.instance.PauseClickSound ();
		}
		
		//quits the game
		public void OnQuitConfirm(){
			AudioManager.instance.DiscardSound ();
			
			//load screen
			BeginningScreenCanvas.instance.loadPanel.SetActive(true);
			
			AkSoundEngine.StopAll ();
			Time.timeScale = 1;
			
			//destroys the game objects that shouldn't carry over to next scene
			Destroy(GameObject.Find("Card_Library"));
			Destroy(GameObject.Find("_SceneTransfer"));
			Destroy (GameObject.Find("WwiseGlobal"));
			Application.LoadLevel (0);
		}
		public void SetTrackButtons(bool b){
			prevButton.interactable = b;
			nextButton.interactable = b;
		}
		public void SetCurrentSong(string s){
			currentSong.text = s;
		}
	}
}