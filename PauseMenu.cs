using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{

	public class PauseMenu : MonoBehaviour {
		
		public static PauseMenu instance;
		public VictoryPauseConditions conditionsScript;
		public GameObject pauseMenu;
		public GameObject optionsMenu;
		public GameObject restartConfirm;
		public GameObject quitConfirm;
		
		public CanvasGroup prevButton;
		public CanvasGroup nextButton;
		public Text currentSong;
		
		
		public void Initialize(){
			instance = this;
			conditionsScript.Initialize ();
		}
		
		public void OnResumeClick(){
			UIManager.instance.SetPauseMenu (false);
			if(UnitManager.instance.GetCurrent() != null)
				UIManager.instance.SetCanvasActive (true);
			Time.timeScale = 1;
			AudioManager.instance.CancelSound ();
		}
		
		public void OnRestartClick(){
			restartConfirm.SetActive (true);
			pauseMenu.SetActive (false);
			AudioManager.instance.PauseClickSound ();
		}
		
		public void OnRestartConfirmClick(){
			BeginningScreenCanvas.instance.loadPanel.SetActive(true);
			AkSoundEngine.StopAll ();
			Application.LoadLevel (Application.loadedLevel);
		}
		
		public void OnOptionClick(){
			pauseMenu.SetActive (false);
			optionsMenu.SetActive (true);
			AudioManager.instance.PauseClickSound ();
		}
		
		public void OnBackClick(){
			optionsMenu.SetActive (false);
			quitConfirm.SetActive (false);
			restartConfirm.SetActive (false);
			pauseMenu.SetActive (true);
			AudioManager.instance.CancelSound ();
		}
		
		public void OnExitClick(){
			quitConfirm.SetActive (true);
			pauseMenu.SetActive (false);
			AudioManager.instance.PauseClickSound ();
		}
		
		public void OnQuitConfirm(){
			AudioManager.instance.DiscardSound ();
			BeginningScreenCanvas.instance.loadPanel.SetActive(true);
			AkSoundEngine.StopAll ();
			//destroy scenetransfer and card library
			Time.timeScale = 1;
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