/*
	Brian Yich 2015
	This script is for controlling the real time parameter values in Wwise.
	The only real time parameter values used were for music volume and sound effect volume.
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace ZetaBusters{
	public class RTPCController : MonoBehaviour {
		
		public float musicValue;
		public float sfxValue;
		
		//ui sliders for volume
		public Slider musicSlider;
		public Slider sfxSlider;
		
		public int refValue = 1;
		
		public Text musicValText;
		public Text sfxValText;
		
		public void Initialize(){
			//loads the music and sfx values into the floats
			AkSoundEngine.GetRTPCValue ("Music", gameObject, out musicValue, ref refValue);
			AkSoundEngine.GetRTPCValue ("SFX", gameObject, out sfxValue, ref refValue);
			
			//sets text and slider values to what was pulled from wwise
			musicValText.text = ((int)musicValue).ToString ();
			sfxValText.text = ((int)sfxValue).ToString ();
			musicSlider.value = (int)musicValue;
			sfxSlider.value = (int)sfxValue;
		}
		
		//updates music volume based on slider value
		public void onMusicValueChanged(){
			musicValue = musicSlider.value;
			AkSoundEngine.SetRTPCValue ("Music", musicValue);
			musicValText.text = ((int)musicValue).ToString ();
		}
		
		//updates sfx volume based on slider value
		public void onSfxValueChanged(){
			sfxValue = sfxSlider.value;
			AkSoundEngine.SetRTPCValue ("SFX", sfxValue);
			sfxValText.text = ((int)sfxValue).ToString ();
		}
	}
}