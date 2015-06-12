using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class RTPCController : MonoBehaviour {
		
		public static RTPCController instance;
		public float musicValue;
		public float sfxValue;
		public Slider musicSlider;
		public Slider sfxSlider;
		public int refValue = 1;
		
		public Text musicValText;
		public Text sfxValText;
		
		public void Initialize(){
			instance = this;
			AkSoundEngine.GetRTPCValue ("Music", gameObject, out musicValue, ref refValue);
			AkSoundEngine.GetRTPCValue ("SFX", gameObject, out sfxValue, ref refValue);
			musicValText.text = ((int)musicValue).ToString ();
			sfxValText.text = ((int)sfxValue).ToString ();
			musicSlider.value = (int)musicValue;
			sfxSlider.value = (int)sfxValue;
		}
		
		public void onMusicValueChanged(){
			musicValue = musicSlider.value;
			AkSoundEngine.SetRTPCValue ("Music", musicValue);
			musicValText.text = ((int)musicValue).ToString ();
		}
		
		public void onSfxValueChanged(){
			sfxValue = sfxSlider.value;
			AkSoundEngine.SetRTPCValue ("SFX", sfxValue);
			sfxValText.text = ((int)sfxValue).ToString ();
		}
	}
}