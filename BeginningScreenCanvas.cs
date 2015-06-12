using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ZetaBusters{
	public class BeginningScreenCanvas : MonoBehaviour {
	
		public static BeginningScreenCanvas instance;

		
		public bool b_LevelComplete = false;
		
		public Text boostsRemaining;
		public int i_BoostCount;
		
		public Unit ami;
		public Text amiEPLabel;
		public Text amiHPLabel;
		public Image amiHPBar;
		public Unit nico;
		public Text nicoEPLabel;
		public Text nicoHPLabel;
		public Image nicoHPBar;
		public Unit bell;
		public Text bellHPLabel;
		public Text bellEPLabel;
		public Image bellHPBar;
		public Unit tora;
		public Text toraHPLabel;
		public Text toraEPLabel;
		public Image toraHPBar;
		public Image amiEPBar;
		public Image nicoEPBar;
		public Image toraEPBar;
		public Image bellEPBar;
		
		private bool b_Reshuffled = false;
		public bool b_DebugOverride = false;
		private bool energyHeal;
		private bool hpHeal;
		private bool vicStatsSet = false;
		public Animator portraitAnim;
		public Button amiButton;
		public Button nicoButton;
		public Button toraButton;
		public Button bellButton;
		public GameObject nextLevelButton;
		public Text nextLevelText;

		public Color nextZoneTextDisabled;
		public Color nextZoneTextEnabled;
		public Color nextZoneButtonDisabled;
		public Color nextZoneButtonEnabled;
		public CanvasGroup boostButtons;
		
		private Animator begAnimator;
		
		public GameObject loadPanel;
		
		public void Initialize()
		{
			instance = this;

			boostsRemaining.text = i_BoostCount.ToString ();
			begAnimator = this.GetComponent<Animator>();
		}
		
		void FixedUpdate()
		{
			if(InitializationSystem.instance.Initialized() && vicStatsSet == true)
			{
				amiHPLabel.text = ami.GetCurrentHealth ().ToString () + "/" + ami.GetStatMaxHealth().ToString();
				nicoHPLabel.text = nico.GetCurrentHealth ().ToString () + "/" + nico.GetStatMaxHealth ().ToString ();
				bellHPLabel.text = bell.GetCurrentHealth ().ToString () + "/" + bell.GetStatMaxHealth ().ToString ();
				toraHPLabel.text = tora.GetCurrentHealth ().ToString () + "/" + tora.GetStatMaxHealth ().ToString ();
				
				amiEPLabel.text = ami.GetCurrentEnergy ().ToString () + "/" + ami.GetStatMaxEnergy ().ToString ();
				nicoEPLabel.text = nico.GetCurrentEnergy ().ToString () + "/" + nico.GetStatMaxEnergy ().ToString ();
				bellEPLabel.text = bell.GetCurrentEnergy ().ToString () + "/" + bell.GetStatMaxEnergy ().ToString ();
				toraEPLabel.text = tora.GetCurrentEnergy ().ToString () + "/" + tora.GetStatMaxEnergy ().ToString ();
				
				amiHPBar.fillAmount = ((float)ami.GetCurrentHealth ()/(float)ami.GetStatMaxHealth ());
				nicoHPBar.fillAmount = ((float)nico.GetCurrentHealth ()/(float)nico.GetStatMaxHealth ());
				bellHPBar.fillAmount = ((float)bell.GetCurrentHealth ()/(float)bell.GetStatMaxHealth ());
				toraHPBar.fillAmount = ((float)tora.GetCurrentHealth ()/(float)tora.GetStatMaxHealth ());
				
				amiEPBar.fillAmount = ((float)ami.GetCurrentEnergy ()/(float)ami.GetStatMaxEnergy ());
				nicoEPBar.fillAmount = ((float)nico.GetCurrentEnergy ()/(float)nico.GetStatMaxEnergy ());
				bellEPBar.fillAmount = ((float)bell.GetCurrentEnergy ()/(float)bell.GetStatMaxEnergy ());
				toraEPBar.fillAmount = ((float)tora.GetCurrentEnergy ()/(float)tora.GetStatMaxEnergy ());
			}
		}

		public void SetVictoryStats(){
			StartCoroutine (SetVicStats ());
		}

		IEnumerator SetVicStats()
		{
			ami = UnitManager.instance.AMI;
			nico = UnitManager.instance.NICO;
			tora = UnitManager.instance.TORA;
			bell = UnitManager.instance.BELL;
			
			b_LevelComplete = true;
			
			foreach (MissionObjective mObjective in BattleManager.instance.objectivesFolder.GetComponentsInChildren<MissionObjective>())
			{
				mObjective.BeginFinalCheck();

				yield return new WaitForSeconds(0.2f);

				if (mObjective.GetStatus() == MissionStatus.Success)
				{
					i_BoostCount++;
				}
			}
			
			
			boostsRemaining.text = i_BoostCount.ToString ();

			vicStatsSet = true;
			CheckBoost();
		}
		
		public void OnHealClick()
		{
			hpHeal = false;
			energyHeal = false;
			
			if (i_BoostCount > 0)
			{
				portraitAnim.SetBool ("Selectable", false);
				AudioManager.instance.PauseClickSound();
				foreach (Unit unit in UnitManager.instance.GetAliveUnits())
				{
					unit.TakeHeal(20, true);
				}
				
				ReduceBoost();
			}else{
				AudioManager.instance.CardErrorSound();
			}
		}
		
		public void OnReviveClick()
		{
			hpHeal = false;
			energyHeal = false;
			
			if (i_BoostCount > 0)
			{
				portraitAnim.SetBool ("Selectable", false);
				AudioManager.instance.PauseClickSound();
				foreach (Unit unit in UnitManager.instance.GetDeadUnits())
				{
					int reviveHP = Mathf.RoundToInt (unit.GetStatMaxHealth() * 0.5f);
					
					unit.ReviveUnit (reviveHP);
				}
				ReduceBoost();
			}else{
				AudioManager.instance.CardErrorSound();
			}
		}
		
		public void OnEnergyClick()
		{
			hpHeal = false;
			energyHeal = false;

			if (i_BoostCount > 0)
			{
				portraitAnim.SetBool ("Selectable", false);
				AudioManager.instance.PauseClickSound();
				foreach (Unit unit in UnitManager.instance.GetAliveUnits())
				{
					unit.TakeEnergyChange(4);
				}
				
				ReduceBoost();
			}else{
				AudioManager.instance.CardErrorSound();
			}
		}
		
		public void OnHealSingleClick()
		{
			hpHeal = false;
			energyHeal = false;

			if (i_BoostCount > 0)
			{
				AudioManager.instance.PauseClickSound();
				portraitAnim.SetBool ("Selectable", true);
				amiButton.enabled = true;
				nicoButton.enabled = true;
				toraButton.enabled = true;
				bellButton.enabled = true;
				hpHeal = true;
				
			}else{
				AudioManager.instance.CardErrorSound();
			}
		}
		
		public void OnEnergySingleClick()
		{
			hpHeal = false;
			energyHeal = false;
			
			if (i_BoostCount > 0)
			{
				AudioManager.instance.PauseClickSound();
				portraitAnim.SetBool ("Selectable", true);
				amiButton.enabled = true;
				nicoButton.enabled = true;
				toraButton.enabled = true;
				bellButton.enabled = true;
				energyHeal = true;
				
			}else{
				AudioManager.instance.CardErrorSound();
			}
		}

		IEnumerator DelayBeginNewLevel(int i)
		{
			yield return new WaitForSeconds (2.0f);

			//BattleManager.instance.EndMapScreen (i_SceneLoad);

			yield break;
		}

		public void ReduceBoost()
		{
			i_BoostCount--;
			boostsRemaining.text = i_BoostCount.ToString ();
			CheckBoost();
		}

		//loads next level
		public void OnFinishLevelClick()
		{
			if (i_BoostCount <= 0)
			{
				StartCoroutine(FinishedLevel());
			}else{
				AudioManager.instance.CardErrorSound();
			}
		}
		
		private IEnumerator FinishedLevel(){
			yield return new WaitForSeconds(0.25f);
			AudioManager.instance.PauseClickSound();
			yield return new WaitForSeconds(0.25f);
			loadPanel.SetActive(true);
			//Code to fade out, then begin new level
			
			//deinitialize / loads next level
			SceneTransfer.SaveState ();
			InitializationSystem.instance.Deinitialize();
			AkSoundEngine.StopAll ();
			Destroy (GameObject.Find("WwiseGlobal"));
			Application.LoadLevel (Application.loadedLevel + 1);
		}
		
		public void OnAmiClick()
		{
			if(hpHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				ami.TakeHeal(50, true);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				hpHeal = false;
				ReduceBoost();
			}
			if(energyHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				ami.TakeEnergyChange(10);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				energyHeal = false;
				ReduceBoost();
			}
		}
		public void OnNicoClick()
		{
			if(hpHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				nico.TakeHeal(50, true);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				hpHeal = false;
				ReduceBoost();
			}
			if(energyHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				nico.TakeEnergyChange(10);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				energyHeal = false;
				ReduceBoost();
			}
		}
		public void OnBellClick()
		{
			if(hpHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				bell.TakeHeal(50, true);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				hpHeal = false;
				ReduceBoost();
			}
			if(energyHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				bell.TakeEnergyChange(10);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				energyHeal = false;
				ReduceBoost();
			}
		}
		public void OnToraClick()
		{
			if(hpHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				tora.TakeHeal(50, true);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				hpHeal = false;
				ReduceBoost();
			}
			if(energyHeal == true)
			{
				AudioManager.instance.PauseClickSound();
				tora.TakeEnergyChange(10);
				portraitAnim.SetBool ("Selectable", false);
				amiButton.enabled = false;
				nicoButton.enabled = false;
				toraButton.enabled = false;
				bellButton.enabled = false;
				energyHeal = false;
				ReduceBoost();
			}
		}
		
		private void CheckBoost(){
			if(i_BoostCount == 0){
				nextLevelText.text = "Continue";
				nextLevelButton.GetComponent<Image>().color = nextZoneButtonEnabled;
				nextLevelText.color = nextZoneTextEnabled;
				NextLevelPulse(true);
				boostButtons.interactable = false;
			}else{
				nextLevelText.text = "Use Your Boosts!";
				nextLevelButton.GetComponent<Image>().color = nextZoneButtonDisabled;
				nextLevelText.color = nextZoneTextDisabled;
				NextLevelPulse(false);
				boostButtons.interactable = true;
			}
		}
		public void NextLevelPulse(bool b){
			if(b){
				begAnimator.SetBool("PulseBool", true);
			}else{
				begAnimator.SetBool("PulseBool", false);
			}
		}
		
	}
}