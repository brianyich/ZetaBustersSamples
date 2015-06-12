using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class MainMenu : MonoBehaviour {
		
		public static MainMenu instance;
		public CanvasGroup mainMenu;
		public CanvasGroup optionsMenu;
		public CanvasGroup confirmMenu;
		public CanvasGroup creditsMenu;
		public CanvasGroup startGameMenu;
		public CanvasGroup levelSelectMenu;
		
		public GameObject mainPanel;
		public GameObject titlePanel;
		public GameObject loadPanel;
		
		public Animator mainMenuAnimator;
		
		public Text rafLink;
		
		void Awake()
		{
			instance = this;
		}
		
		//for audio collaborator credits
		public void OnRafClick(){
			Application.OpenURL("https://www.facebook.com/mrgirlband");
		}
		public void OnRafHover(){
			rafLink.color = Color.white;
		}
		public void OnRafExit(){
			rafLink.color = new Color32 (13,165,255,255);
		}
		
		//buttons and animations
		public void OnStartDown(bool b){
			mainMenuAnimator.SetBool("StartDown", b);
			if(!b){
				StartCoroutine(StartMenu());
			}
		}
		private IEnumerator StartMenu(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			startGameMenu.alpha = 1;
			startGameMenu.interactable = true;
			startGameMenu.blocksRaycasts = true;
			mainMenu.alpha = 0;
			mainMenu.interactable = false;
			mainMenu.blocksRaycasts = false;
			confirmMenu.alpha = 0;
			confirmMenu.interactable = false;
			confirmMenu.blocksRaycasts = false;
		}
		public void OnYesDown(bool b){
			mainMenuAnimator.SetBool("YesBool", b);
			if(!b){
				StartCoroutine(StartTutorial());
			}
		}
		public void OnNoDown(bool b){
			mainMenuAnimator.SetBool("NoBool", b);
			if(!b){
				StartCoroutine(StartGame());
			}
		}
		private IEnumerator StartTutorial(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			//change loaded level to tutorial
			Application.LoadLevel ("TutorialStage");
		}
		private IEnumerator StartGame(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			//change loaded level to generators stage
			Application.LoadLevel (2);
		}
		
		public void OnStartCancelDown(bool b){
			mainMenuAnimator.SetBool("StartCancelBool", b);
			if(!b){
				StartCoroutine(StartCancel());
			}
		}
		private IEnumerator StartCancel(){
			MainMenuAudio.instance.OnBackClickSound();
			yield return new WaitForSeconds(0.25f);
			startGameMenu.alpha = 0;
			startGameMenu.interactable = false;
			startGameMenu.blocksRaycasts = false;
			mainMenu.alpha = 1;
			mainMenu.interactable = true;
			mainMenu.blocksRaycasts = true;
		}
		
		public void OnOptionsDown(bool b){
			mainMenuAnimator.SetBool("OptionsDown", b);
			if(!b){
				StopAllCoroutines();
				StartCoroutine(Options());
			}
		}
		private IEnumerator Options(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnButtonClickSound ();
			mainMenu.alpha = 0;
			mainMenu.interactable = false;
			mainMenu.blocksRaycasts = false;
			optionsMenu.alpha = 1;
			optionsMenu.interactable = true;
			optionsMenu.blocksRaycasts = true;
			creditsMenu.alpha = 0;
			creditsMenu.interactable = false;
			creditsMenu.blocksRaycasts = false;
			startGameMenu.alpha = 0;
			startGameMenu.interactable = false;
			startGameMenu.blocksRaycasts = false;
			confirmMenu.alpha = 0;
			confirmMenu.interactable = false;
			confirmMenu.blocksRaycasts = false;
			levelSelectMenu.alpha = 0;
			levelSelectMenu.interactable = false;
			levelSelectMenu.blocksRaycasts = false;
		}
		
		public void OnCreditsDown(bool b){
			mainMenuAnimator.SetBool("CreditsDown", b);
			if(!b){
				StartCoroutine(Credits());
			}
		}
		private IEnumerator Credits(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnButtonClickSound ();
			mainMenu.alpha = 0;
			mainMenu.interactable = false;
			mainMenu.blocksRaycasts = false;
			optionsMenu.alpha = 0;
			optionsMenu.interactable = false;
			optionsMenu.blocksRaycasts = false;
			creditsMenu.alpha = 1;
			creditsMenu.interactable = true;
			creditsMenu.blocksRaycasts = true;
			startGameMenu.alpha = 0;
			startGameMenu.interactable = false;
			startGameMenu.blocksRaycasts = false;
			confirmMenu.alpha = 0;
			confirmMenu.interactable = false;
			confirmMenu.blocksRaycasts = false;
			levelSelectMenu.alpha = 0;
			levelSelectMenu.interactable = false;
			levelSelectMenu.blocksRaycasts = false;
		}
		
		public void OnQuitDown(bool b){
			mainMenuAnimator.SetBool("QuitDown", b);
			if(!b){
				StartCoroutine(Quit());
			}
		}
		private IEnumerator Quit(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnButtonClickSound ();
			confirmMenu.alpha = 1;
			confirmMenu.interactable = true;
			confirmMenu.blocksRaycasts = true;
			mainMenu.alpha = 0;
			mainMenu.interactable = false;
			mainMenu.blocksRaycasts = false;
			startGameMenu.alpha = 0;
			startGameMenu.interactable = false;
			startGameMenu.blocksRaycasts = false;
		}
		
		public void OnBackDown(bool b){
			mainMenuAnimator.SetBool("BackBool", b);
			if(!b){
				StartCoroutine(Back());
			}
		}
		private IEnumerator Back(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnBackClickSound();
			mainMenu.alpha = 1;
			mainMenu.interactable = true;
			mainMenu.blocksRaycasts = true;
			optionsMenu.alpha = 0;
			optionsMenu.interactable = false;
			optionsMenu.blocksRaycasts = false;
		}
		
		public void CreditsBackDown(bool b){
			mainMenuAnimator.SetBool("CreditsBackBool", b);
			if(!b){
				StartCoroutine(CreditsBack());
			}
		}
		private IEnumerator CreditsBack(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnBackClickSound();
			mainMenu.alpha = 1;
			mainMenu.interactable = true;
			mainMenu.blocksRaycasts = true;
			creditsMenu.alpha = 0;
			creditsMenu.interactable = false;
			creditsMenu.blocksRaycasts = false;
		}
		
		public void QuitConfirmDown(bool b){
			mainMenuAnimator.SetBool("QuitConfirmBool", b);
			if(!b){
				StartCoroutine(QuitConfirm());
			}
		}
		private IEnumerator QuitConfirm(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnButtonClickSound ();
			AkSoundEngine.StopAll ();
			Application.Quit ();
		}
		
		public void CancelBool(bool b){
			mainMenuAnimator.SetBool("CancelBool", b);
			if(!b){
				StartCoroutine(Cancel());
			}
		}
		private IEnumerator Cancel(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnBackClickSound();
			mainMenu.alpha = 1;
			mainMenu.interactable = true;
			mainMenu.blocksRaycasts = true;
			confirmMenu.alpha = 0;
			confirmMenu.interactable = false;
			confirmMenu.blocksRaycasts = false;
		}
		
		public void LevelSelect(bool b){
			mainMenuAnimator.SetBool("LevelSelectDown", b);
			if(!b){
				StartCoroutine(SelectLevel());
			}
		}
		private IEnumerator SelectLevel(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnButtonClickSound ();
			mainMenu.alpha = 0;
			mainMenu.interactable = false;
			mainMenu.blocksRaycasts = false;
			optionsMenu.alpha = 0;
			optionsMenu.interactable = false;
			optionsMenu.blocksRaycasts = false;
			creditsMenu.alpha = 0;
			creditsMenu.interactable = false;
			creditsMenu.blocksRaycasts = false;
			startGameMenu.alpha = 0;
			startGameMenu.interactable = false;
			startGameMenu.blocksRaycasts = false;
			confirmMenu.alpha = 0;
			confirmMenu.interactable = false;
			confirmMenu.blocksRaycasts = false;
			levelSelectMenu.alpha = 1;
			levelSelectMenu.interactable = true;
			levelSelectMenu.blocksRaycasts = true;
		}
		
		public void AlleywaysClick(bool b){
			mainMenuAnimator.SetBool("AlleywaysDown", b);
			if(!b){
				StartCoroutine(Alleyways());
			}
		}
		private IEnumerator Alleyways(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			//change loaded level to generators stage
			Application.LoadLevel ("MicroLevel");
		}
		
		public void GeneratorsClick(bool b){
			mainMenuAnimator.SetBool("GeneratorsDown", b);
			if(!b){
				StartCoroutine(Generators());
			}
		}
		private IEnumerator Generators(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			//change loaded level to generators stage
			Application.LoadLevel ("DefendGenerators");
		}
		
		public void StairwaysClick(bool b){
			mainMenuAnimator.SetBool("StairwaysDown", b);
			if(!b){
				StartCoroutine(Stairways());
			}
		}
		private IEnumerator Stairways(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			//change loaded level to generators stage
			Application.LoadLevel ("Stairways");
		}
		
		public void ShowdownClick(bool b){
			mainMenuAnimator.SetBool("ShowdownDown", b);
			if(!b){
				StartCoroutine(Showdown());
			}
		}
		private IEnumerator Showdown(){
			MainMenuAudio.instance.OnButtonClickSound ();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			yield return new WaitForSeconds(3.0f);
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			//change loaded level to generators stage
			Application.LoadLevel ("Showdown");
		}
		
		public void LevelCancel(bool b){
			mainMenuAnimator.SetBool("LevelCancel", b);
			if(!b){
				StartCoroutine(CancelLevelSelect());
			}
		}
		private IEnumerator CancelLevelSelect(){
			yield return new WaitForSeconds(0.25f);
			MainMenuAudio.instance.OnBackClickSound();
			mainMenu.alpha = 1;
			mainMenu.interactable = true;
			mainMenu.blocksRaycasts = true;
			levelSelectMenu.alpha = 0;
			levelSelectMenu.interactable = false;
			levelSelectMenu.blocksRaycasts = false;
		}
		
		public void OnTitleClick(){
			mainPanel.SetActive(true);
			titlePanel.SetActive(false);
			MainMenuAudio.instance.PlayEvent("Play_VFS_SS_DC_SFX_UI_SELECT_SPECIAL1_");
			StartCoroutine(TitleCutIn());
		}
		private IEnumerator TitleCutIn(){
			yield return new WaitForSeconds(0.2f);
			AkSoundEngine.PostEvent ("Play_TitleAmiCut", gameObject);
			yield break;
		}
		
		
	}
}