/*
	Brian Yich, 2015
	This script is for the Charge Booth neutral unit that allows the user unit to heal or recharge if they are standing on a tile beside the Charge Booth.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ZetaBusters
{
	public class ChargeBooth : Dummy
	{
		//heal / recharge UI display when user unit is beside the charge booth
		public GameObject restoreButtons;
		
		private List<Tile> adjacentTiles;
		private int turnCounter;
		private bool restoreCharged;

		public override void Initialize()
		{
			base.Initialize ();
			
			//sets unit type to neutral unit
			SetUnitType (UnitType.Dummy);
			if (!b_OverrideTeam)
			{
				SetTeam(Team.Neutral);
			}
			
			adjacentTiles = new List<Tile> ();
			restoreCharged = true;
			turnCounter = 0;
		}
		
		//if someone destroys booth
		public override void Death()
		{
			b_alive = false;
			AudioManager.instance.CarExplosion();
		}
		
		void Update(){
			//ensures update only runs when system is initialized
			if(InitializationSystem.instance.Initialized())
			{
				//checks for if there are any alive player units beside them
				adjacentTiles = myTile.GetNeighbors();
				if(restoreCharged && UnitManager.instance.GetCurrent().GetTeam() == Team.Player && GetCurrentHealth() > 0){
					foreach (Tile tile in adjacentTiles)
					{	
						if (tile.GetOwner())
						{
							//checks if the neighbor unit is the user's current unit
							if (tile.GetOwner() == UnitManager.instance.GetCurrent())
							{
								//sets UI display active if true
								restoreButtons.SetActive(true);
								break;
							}
						}else{
							restoreButtons.SetActive(false);
						}
					}
				}
				//sets UI display inactive if false
				else{
					restoreButtons.SetActive(false);
				}
			}
		}
		
		//for the button that heals HP
		public void OnBoothHP(){
			//user is unable to undo their move after healing
			UIManager.instance.SetUndoMoveButton(false);
			
			//checks if current unit is not at max HP
			if (UnitManager.instance.GetCurrent().GetCurrentHealth() < UnitManager.instance.GetCurrent().GetStatMaxHealth())
			{
				//heals current unit
				UnitManager.instance.GetCurrent().TakeHeal(Mathf.RoundToInt(UnitManager.instance.GetCurrent().GetStatMaxHealth() * 0.75f));
				
				//deactivates charge booth
				restoreCharged = false;
				GetComponentInChildren<Animator>().SetBool ("Active",false);
			}
			//if at max HP
			else
			{
				//feedback for user showing health is full
				DisplayStack.instance.AddUnitEvent(UnitManager.instance.GetCurrent(), "Health already full", DisplayStackTypes.Info);
			}
		}
		
		//for the button that heals only EP
		public void OnBoothEP(){
		
			UIManager.instance.SetUndoMoveButton(false);
			
			//checks if current unit is not at max EP
			if (UnitManager.instance.GetCurrent().GetCurrentEnergy() < UnitManager.instance.GetCurrent().GetStatMaxEnergy())
			{
				//recharges current unit's EP
				UnitManager.instance.GetCurrent().TakeEnergyChange(UnitManager.instance.GetCurrent().GetStatMaxEnergy());
				
				//deactivates charge booth
				restoreCharged = false;
				GetComponentInChildren<Animator>().SetBool ("Active",false);
			}
			//if at max EP
			else
			{
				DisplayStack.instance.AddUnitEvent(UnitManager.instance.GetCurrent(), "Energy already full", DisplayStackTypes.Info);
			}

		}
		
		//for the button that heals HP and EP
		public void OnBoothBoth(){
		
			UIManager.instance.SetUndoMoveButton(false);
			
			//checks if current unit is not at max HP and HP
			if (UnitManager.instance.GetCurrent().GetCurrentHealth() < UnitManager.instance.GetCurrent().GetStatMaxHealth() || 
			    UnitManager.instance.GetCurrent().GetCurrentEnergy() < UnitManager.instance.GetCurrent().GetStatMaxEnergy())
			{
				//heals current unit's HP / EP
				UnitManager.instance.GetCurrent().TakeHeal(Mathf.RoundToInt(UnitManager.instance.GetCurrent().GetStatMaxHealth() * 0.5f));
				UnitManager.instance.GetCurrent().TakeEnergyChange(Mathf.RoundToInt(UnitManager.instance.GetCurrent().GetStatMaxEnergy() * 0.5f));
				
				//deactivates charge booth
				restoreCharged = false;
				GetComponentInChildren<Animator>().SetBool ("Active",false);
			}
			else
			{
				DisplayStack.instance.AddUnitEvent(UnitManager.instance.GetCurrent(), "Health & Energy already full", DisplayStackTypes.Info);
				
			}
		}
		
		//if charger is inactive, it counts the rounds and reactivates it based on how many rounds there are
		//called at the end of each round
		public void TurnCounter(){
			//if charge booth is used / out of charge
			if(restoreCharged == false){
			
				turnCounter++;
				
				//if it's the third turn the charge booth is out of charge, it reactivates
				if(turnCounter == 3){
					restoreCharged = true;
					
					//reactivate animation
					GetComponentInChildren<Animator>().SetBool ("Active",true);
					turnCounter = 0;
				}
			}
		}
		
	}
}






