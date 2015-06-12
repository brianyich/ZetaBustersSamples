using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters
{
	public class AudioManager : MonoBehaviour
	{
				
		//initialize variables
		public static AudioManager instance;
		uint bankID;
		public AmiSFX amiSfxScript;
		public NicoSFX nicoSfxScript;
		public ToraSFX toraSfxScript;
		public BellSFX bellSfxScript;
		public EnemySFX enemySfxScript;
		public RTPCController rtpcScript;
		public Text currentSong;
		
		private int audioIndex;
				
		//initializes upon scene start
		public void Initialize()
		{
			instance = this;
			amiSfxScript.Initialize();
			nicoSfxScript.Initialize();
			toraSfxScript.Initialize();
			bellSfxScript.Initialize();
			enemySfxScript.Initialize();
			rtpcScript.Initialize();
		}
				
		//activates after initialization
		public void Activate()
		{
			//loads ui soundbank
			AkSoundEngine.LoadBank("UISoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads music soundbank
			AkSoundEngine.LoadBank("MusicSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads card soundbank
			AkSoundEngine.LoadBank("CardSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads misc soundbank
			AkSoundEngine.LoadBank("MiscSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//plays random song at start of scene
			if(UnitManager.instance.b_TutorialLevel == true){
				PlayEvent("Play_SS_Tutorial");
				PauseMenu.instance.SetCurrentSong("Current Song: Knowledge is Power");
				PauseMenu.instance.SetTrackButtons(false);
			}else if(UnitManager.instance.b_FinalLevel == true){
				PlayEvent("Play_VFS_SS_RAG_Mx_Dr_B_Theme");
				PauseMenu.instance.SetCurrentSong("Current Song: Bellbotula's Takeover");
				PauseMenu.instance.SetTrackButtons(false);
			}else{
				audioIndex = Random.Range(0, 4);
				PlayNextSong();
			}
		}
		
		public void PlayPrevSong(){
			if(UnitManager.instance.b_FinalLevel == false && UnitManager.instance.b_TutorialLevel == false){
				audioIndex--;
				if(audioIndex < 0){
					audioIndex = 3;
				}
				switch(audioIndex){
					case 0:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_2");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_1");
						currentSong.text = "Current Song: My Neighbor ToTora";
						break;
					case 1:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_4");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_2");
						currentSong.text = "Current Song: Hell's Bells";
						break;
					case 2:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_5");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_4");
						currentSong.text = "Current Song: SalAmi Sandwich";
						break;
					case 3:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_1");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_5");
						currentSong.text = "Current Song: Nicotine Addiction";
						break;
				}
			}
		}
		
		public void PlayNextSong(){
			if(UnitManager.instance.b_FinalLevel == false && UnitManager.instance.b_TutorialLevel == false){
				audioIndex++;
				if(audioIndex > 3){
					audioIndex = 0;
				}
				switch(audioIndex){
				case 0:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_5");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_1");
						currentSong.text = "Current Song: My Neighbor ToTora";
				break;
				case 1:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_1");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_2");
						currentSong.text = "Current Song: Hell's Bells";
				break;
				case 2:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_2");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_4");
						currentSong.text = "Current Song: SalAmi Sandwich";
				break;
				case 3:
						PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_4");
						PlayEvent("Play_VFS_SS_RAG_Mx_Battle_5");
						currentSong.text = "Current Song: Nicotine Addiction";
				break;
				}
			}
		}
				
		//plays sound event based on event name
		//takes string of event name
		public void PlayEvent(string eventName)
		{
			AkSoundEngine.PostEvent(eventName, gameObject);
		}
				
		//on move/attack disk hover sounds
		//takes hand placement index
		public void MainHoverSound(int i)
		{
			if (UIManager.instance.combatWindow.GetComponent<CanvasGroup>().alpha == 1.0f)
			{	
				switch (i)
				{
					case 1:
						AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						break;
					case 2:
						AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						break;
				}
			}
		}
				
		//on move/attack disk click sound
		//takes hand placement index
		public void MainClickSound(int i)
		{
			if (UIManager.instance.combatWindow.GetComponent<CanvasGroup>().alpha == 1.0f)
			{
				switch (i)
				{
					case 1:
						if (MainCards.instance.attackButtonComponent.enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_SELECT_SPECIAL1_", gameObject);
						} else
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_ERROR", gameObject);
						}
						break;
					case 2:
						if (MainCards.instance.moveButtonComponent.enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_SELECT_SPECIAL1_", gameObject);
						} else
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_ERROR", gameObject);
						}
						break;
				}
			}
		}
				
		//on card hover
		//takes hand placement index
		public void CardHoverSound(int hand)
		{
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup>().alpha == 1.0f)
			{
				switch (hand)
				{
					case 1:
						if (UIManager.instance.card1.GetComponentInParent<Button>().enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						}
						break;
					case 2:
						if (UIManager.instance.card2.GetComponentInParent<Button>().enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						}
						break;
					case 3:
						if (UIManager.instance.card3.GetComponentInParent<Button>().enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						}
						break;
					case 4:
						if (UIManager.instance.card4.GetComponentInParent<Button>().enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						}
						break;
					case 5:
						if (UIManager.instance.card5.GetComponentInParent<Button>().enabled == true)
						{
							AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CARDHOVER", gameObject);
						}
						break;
				}
			}
		}

		//plays card click sound
		public void CardClickSound()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_SELECT_SPECIAL1_", gameObject);
		}

		//plays error sound
		public void CardErrorSound()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_ERROR", gameObject);
		}
		
		//plays cancel sound
		public void CancelSound()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_CANCEL", gameObject);
		}
				
		//plays hover sound on draw/discard/end turn button 
		public void ExtraHoverSound()
		{
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup>().alpha == 1.0f)
			{
				AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_EXTRAHOVER", gameObject);
			}
		}
				
		public void DrawSound()
		{
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup>().alpha == 1.0f || UIManager.instance.drawButton.GetComponent<Button>().enabled == true)
			{
				if (UnitManager.instance.GetCurrent().GetCurrentEnergy() > 0 && UIManager.instance.GetHandCount() != 5)
				{
					AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_DRAW", gameObject);
				} else
				{
					AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_ERROR", gameObject);
				}
			}
		}

		public void DiscardSound()
		{
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup>().alpha == 1.0f)
			{
				AkSoundEngine.PostEvent("Play_VFS_SS_FA_SFX_UI_DISCARDED", gameObject);
			}

		}

		public void DiscEndSound()
		{
			if (UIManager.instance.cardDisplay.GetComponent<CanvasGroup>().alpha == 1.0f)
			{
				AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_DISCEND", gameObject);
			}
		}
		//end of card / bottom ui sounds
				
		//pause sounds
		public void PauseClickSound()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_UI_SELECT_SPECIAL3_", gameObject);
		}	
				
		//card sounds
		public void HealSound()
		{
			AkSoundEngine.PostEvent("Play_UI_slection_Ability_HealText", gameObject);
		}

		public void BoostSound()
		{
			AkSoundEngine.PostEvent("Play_UI_slection_Ability_Boost", gameObject);
		}

		public void ShockwaveStart(int random)
		{
			switch (random)
			{
				case 0:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_CARD_SHOCKWAVESTART_V1", gameObject);
					break;
				case 1:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_CARD_SHOCKWAVESTART_V2", gameObject);
					break;
				case 2:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_CARD_SHOCKWAVESTART_V3", gameObject);
					break;
			}
		}
		
		public void ShockwaveLoopStop(int random)
		{
			switch (random)
			{
				case 0:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_CARD_SHOCKWAVELOOP_V1", gameObject);
					break;
				case 1:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_CARD_SHOCKWAVELOOP_V2", gameObject);
					break;
				case 2:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_CARD_SHOCKWAVELOOP_V3", gameObject);
					break;
			}
		}

		public void CrossAttackStart(int random)
		{
			switch (random)
			{
				case 0:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_CARD_CROSSATTACKSTART_V1", gameObject);
					break;
				case 1:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_CARD_CROSSATTACKSTART_V2", gameObject);
					break;
				case 2:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_CARD_CROSSATTACKSTART_V3", gameObject);
					break;
			}
		}
		public void CrossAttackLoopStop(int random)
		{
			switch (random)
			{
				case 0:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_CARD_CROSSATTACKLOOP_V1", gameObject);
					break;
				case 1:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_CARD_CROSSATTACKLOOP_V2", gameObject);
					break;
				case 2:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_CARD_CROSSATTACKLOOP_V3", gameObject);
					break;
			}
		}
		public void EMPBlast(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_DP_SFX_CARD_EMPGRENADE", gameObject);
		}
		public void StopEMP(){
			AkSoundEngine.PostEvent ("Stop_VFS_SS_DP_SFX_CARD_EMPGRENADE", gameObject);
		}
		public void DoubleStrike(){
			AkSoundEngine.PostEvent("Play_DoubleStrike", gameObject);
		}
		public void Firebomb(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_DC_SFX_BATTLE_GRENADE_EXPLOSION", gameObject);
		}
		public void HydroBlast(){
			AkSoundEngine.PostEvent("Play_HydroBlast", gameObject);
		}
		public void StopHydro(){
			AkSoundEngine.PostEvent("Stop_HydroBlast", gameObject);
		}
		public void IonStrike(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_DP_SFX_CARD_IONSTRIKE", gameObject);
		}
		public void TechBomb(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_DC_SFX_BATTLE_TBOMB", gameObject);
		}
		public void Whirlwind(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_DP_SFX_CARD_WHIRLWIND", gameObject);
		}
		public void StopWhirlwind(){
			AkSoundEngine.PostEvent ("Stop_VFS_SS_DP_SFX_CARD_WHIRLWIND", gameObject);
		}
		//timed hit sounds
		public void TimedHitSuccess()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_BATTLE_MiniGame_Success", gameObject);
		}

		public void TimedHitFailure()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_BATTLE_MiniGame_Failure_2", gameObject);
		}

		public void CountdownBeep()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_BATTLE_MiniGame_Countdown_3__beep_", gameObject);
		}

		public void CountdownChime()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_BATTLE_MiniGame_Countdown_3__chime_", gameObject);
		}
				
		//misc
		public void FriendlyFireWarning()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_BATTLE_FriendlyFire_Warning_1", gameObject);
		}

		public void StopFriendlyFire()
		{
			AkSoundEngine.PostEvent("Stop_VFS_SS_DC_SFX_BATTLE_FriendlyFire_Warning_1", gameObject);
		}

		public void VictorySong()
		{
			AkSoundEngine.StopAll();
			AkSoundEngine.PostEvent("Play_VFS_SS_RAG_Mx_Win", gameObject);
		}

		public void DefeatSong()
		{
			AkSoundEngine.StopAll();
			AkSoundEngine.PostEvent("Play_VFS_SS_RAG_Mx_Win_Lose", gameObject);
		}

		public void DodgeSound()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DC_SFX_BATTLE_Dodge_1", gameObject);
		}

		public void CarExplosion()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_RAG_SFX_Battle_CarExp", gameObject);
		}

		public void FireHydrantBurst(int i)
		{
			switch (i)
			{
				case 0:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_HYDRANT_BURST_V1", gameObject);
					break;
				case 1:
					AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_HYDRANT_BURST_V2", gameObject);
					break;
			}
		}
		public void FireHydrantEnd(int i)
		{
			switch (i)
			{
				case 0:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_HYDRANT_WATERLOOP_V1", gameObject);
					break;
				case 1:
					AkSoundEngine.PostEvent("Stop_VFS_SS_DP_SFX_HYDRANT_WATERLOOP_V2", gameObject);
					break;
			}
		}
		public void RoundCutIn(){
			AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_UI_ROUNDTITLE", gameObject);
		}
		
		public void DummyOnHit()
		{
			AkSoundEngine.PostEvent("Play_VFS_SS_DP_SFX_BATTLE_PLAYERINJURY_V1", gameObject);
		}
		public void FireDamage(){
			PlayEvent("Play_FireDamage");
		}
		public void CameraRotate(){
			PlayEvent("Play_CameraRotate");
		}
		public void StunnedSound(){
			PlayEvent("Play_VFS_SS_DP_SFX_STATUS_PARALYZED_V3");
		}
				
	}

}
