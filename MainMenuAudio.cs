using UnityEngine;
using System.Collections;

namespace ZetaBusters{
	public class MainMenuAudio : MonoBehaviour {
		
		public static MainMenuAudio instance;
		private uint bankID;
		
		// Use this for initialization
		void Start () {
			instance = this;
			//loads ui soundbank
			AkSoundEngine.LoadBank ("UISoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads music soundbank
			AkSoundEngine.LoadBank ("MusicSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			//loads misc soundbank
			AkSoundEngine.LoadBank ("MiscSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			
			AkSoundEngine.PostEvent ("Play_VFS_SS_RAG_Mx_Battle_3_MainMenu", gameObject);
			StartCoroutine(Intro());
			
		}
		
		private IEnumerator Intro(){
			yield return new WaitForSeconds(4.0f);
			yield return new WaitForSeconds(1.425f);
			AkSoundEngine.PostEvent ("Play_VFS_SS_DP_SFX_UI_ROUNDTITLE_V01", gameObject);
			yield break;
		}
		
		public void PlayEvent(string s){
			AkSoundEngine.PostEvent(s, gameObject);
		}
		
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