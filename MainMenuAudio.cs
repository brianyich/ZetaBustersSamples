/*
	Brian Yich, 2015
	This script is the Audio Manager for the main menu.
*/

using UnityEngine;
using System.Collections;

namespace ZetaBusters{
	public class MainMenuAudio : MonoBehaviour {
		
		//Singleton
		private static MainMenuAudio _instance;
		public static MainMenuAudio instance
		{
			get
			{
				if (_instance == null)
					_instance = GameObject.FindObjectOfType<MainMenuAudio>();
				return _instance;
			}
		}
		
		//for wwise
		private uint bankID;
		
		// Use this for initialization
		void Start () {
			//loads ui soundbank
			AkSoundEngine.LoadBank ("UISoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads music soundbank
			AkSoundEngine.LoadBank ("MusicSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads misc soundbank
			AkSoundEngine.LoadBank ("MiscSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			
			//plays main menu song
			AkSoundEngine.PostEvent ("Play_VFS_SS_RAG_Mx_Battle_3_MainMenu", gameObject);
			
			//main menu intro start
			StartCoroutine(Intro());
			
		}
		
		//zeta busters cut-in sound
		private IEnumerator Intro(){
			yield return new WaitForSeconds(5.5f);
			AkSoundEngine.PostEvent ("Play_VFS_SS_DP_SFX_UI_ROUNDTITLE_V01", gameObject);
			yield break;
		}
		
		//plays sound based on soundbank file name
		public void PlayEvent(string s){
			AkSoundEngine.PostEvent(s, gameObject);
		}
		
		//for button events
		public void OnButtonHoverSound(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_FA_SFX_UI_EXTRAHOVER", gameObject);
		}
		
		public void OnButtonClickSound(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_DC_SFX_UI_SELECT_SPECIAL3_", gameObject);
		}
		
		public void OnBackClickSound(){
			AkSoundEngine.PostEvent ("Play_VFS_SS_FA_SFX_UI_CANCEL", gameObject);
		}
	}
}