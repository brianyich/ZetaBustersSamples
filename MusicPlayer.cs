/*
	Brian Yich, 2015
	This script is for the Music Player that allows the user to navigate through their songs.
*/

using UnityEngine;
using System.Collections;

namespace ZetaBusters
{
	public class MusicPlayer : MonoBehaviour
	{
				
		//Singleton
		private static MusicPlayer _instance;
		public static MusicPlayer instance
		{
			get
			{
				if (_instance == null)
					_instance = GameObject.FindObjectOfType<MusicPlayer>();
				return _instance;
			}
		}
		
		//used for wwise function
		uint bankID;
		
		//index for the music player
		private int audioIndex;
				
				
		//activates after system initialization
		public void Activate()
		{
			//loads the music soundbank from wwise
			AkSoundEngine.LoadBank("MusicSoundBank", AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
			
			//plays tutorial theme if tutorial level
			if(UnitManager.instance.b_TutorialLevel == true){
				//plays the song
				AudioManager.instance.PlayEvent("Play_SS_Tutorial");
				
				//sets the title in pause menu
				PauseMenu.instance.SetCurrentSong("Current Song: Knowledge is Power");
				
				//disable track previous and next buttons
				PauseMenu.instance.SetTrackButtons(false);
			}
			//plays boss theme if it's the final level
			else if(UnitManager.instance.b_FinalLevel == true){
				AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Dr_B_Theme");
				PauseMenu.instance.SetCurrentSong("Current Song: Bellbotula's Takeover");
				PauseMenu.instance.SetTrackButtons(false);
			}
			//plays random song at the start of the level
			else{
				audioIndex = Random.Range(0, 4);
				PlayNextSong();
			}
		}
		
		//plays the previous song on the music player
		public void PlayPrevSong(){
			//makes sure we're not in a tutorial level
			if(UnitManager.instance.b_FinalLevel == false && UnitManager.instance.b_TutorialLevel == false){
				//index to previous song
				audioIndex--;
				
				//loops to the front of the playlist if at the end
				if(audioIndex < 0){
					audioIndex = 3;
				}
				
				switch(audioIndex){
					case 0:
						//stop event for previous song
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_2");
						
						//play event for next song
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_1");
						
						//sets title in pause menu
						PauseMenu.instance.SetCurrentSong("Current Song: My Neighbor ToTora");
						break;
					case 1:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_4");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_2");
						PauseMenu.instance.SetCurrentSong("Current Song: Hell's Bells");
						break;
					case 2:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_5");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_4");
						PauseMenu.instance.SetCurrentSong("Current Song: SalAmi Sandwich");
						break;
					case 3:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_1");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_5");
						PauseMenu.instance.SetCurrentSong("Current Song: Nicotine Addiction");
						break;
				}
			}
		}
		
		//plays the next song on the music player
		public void PlayNextSong(){
			if(UnitManager.instance.b_FinalLevel == false && UnitManager.instance.b_TutorialLevel == false){
				//index to next song
				audioIndex++;
				
				//loops to start of playlist if at the end
				if(audioIndex > 3){
					audioIndex = 0;
				}
				
				switch(audioIndex){
				case 0:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_5");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_1");
						currentSong.text = "Current Song: My Neighbor ToTora";
				break;
				case 1:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_1");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_2");
						currentSong.text = "Current Song: Hell's Bells";
				break;
				case 2:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_2");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_4");
						currentSong.text = "Current Song: SalAmi Sandwich";
				break;
				case 3:
						AudioManager.instance.PlayEvent("Stop_VFS_SS_RAG_Mx_Battle_4");
						AudioManager.instance.PlayEvent("Play_VFS_SS_RAG_Mx_Battle_5");
						currentSong.text = "Current Song: Nicotine Addiction";
				break;
				}
			}
		}
		
	}

}
