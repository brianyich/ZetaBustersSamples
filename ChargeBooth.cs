using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ZetaBusters
{
	public class ChargeBooth : Dummy
	{
		
		private List<Tile> adjacentTiles;
		public GameObject restoreButtons;
		private int turnCounter;
		private bool restoreCharged;
		public CanvasGroup[] buttonLabels;

		public override void Initialize()
		{
			base.Initialize ();
			SetUnitType (UnitType.Dummy);

			if (!b_OverrideTeam)
			{
				SetTeam(Team.Neutral);
			}
			adjacentTiles = new List<Tile> ();
			restoreCharged = true;
			turnCounter = 0;
		}
		public override void Death()
		{
			if(b_Explodes && b_alive)
			{
				b_alive = false;
				AudioManager.instance.CarExplosion();
			}
			if(b_SpreadWater && b_alive)
			{
				b_alive = false;
			}
			else
				base.Death();
		}
		void Update(){
			if(InitializationSystem.instance.Initialized())
			{
				adjacentTiles = myTile.GetNeighbors();
				if(restoreCharged && UnitManager.instance.GetCurrent().GetTeam() == Team.Player && GetCurrentHealth() > 0){
					foreach (Tile tile in adjacentTiles)
					{	
						if (tile.GetOwner())
						{
							if (tile.GetOwner() == UnitManager.instance.GetCurrent())
							{
								restoreButtons.SetActive(true);
								break;
							}
						}else{
							restoreButtons.SetActive(false);
						}
					}
				}else{
					restoreButtons.SetActive(false);
				}
			}
		}
		
		//buttons for heal / recharge, adjust the numbers if you guys wanna
		public void OnBoothHP(){
			UIManager.instance.SetUndoMoveButton(false);
			if (UnitManager.instance.GetCurrent().GetCurrentHealth() < UnitManager.instance.GetCurrent().GetStatMaxHealth())
			{
				UnitManager.instance.GetCurrent().TakeHeal(Mathf.RoundToInt(UnitManager.instance.GetCurrent().GetStatMaxHealth() * 0.75f));
				GetHPBar().SetActive(false);
				restoreCharged = false;
				GetComponentInChildren<Animator>().SetBool ("Active",false);
			}
			else
			{
				DisplayStack.instance.AddUnitEvent(UnitManager.instance.GetCurrent(), "Health already full", DisplayStackTypes.Info);
			}
		}
		
		public void OnBoothEP(){
			UIManager.instance.SetUndoMoveButton(false);
			if (UnitManager.instance.GetCurrent().GetCurrentEnergy() < UnitManager.instance.GetCurrent().GetStatMaxEnergy())
			{
				AudioManager.instance.BoostSound();
				UnitManager.instance.GetCurrent().TakeEnergyChange(UnitManager.instance.GetCurrent().GetStatMaxEnergy());
				GetHPBar().SetActive(false);
				restoreCharged = false;
				GetComponentInChildren<Animator>().SetBool ("Active",false);
			}
			else
			{
				DisplayStack.instance.AddUnitEvent(UnitManager.instance.GetCurrent(), "Energy already full", DisplayStackTypes.Info);
			}

		}
		
		public void OnBoothBoth(){
			UIManager.instance.SetUndoMoveButton(false);
			if (UnitManager.instance.GetCurrent().GetCurrentHealth() < UnitManager.instance.GetCurrent().GetStatMaxHealth() || 
			    UnitManager.instance.GetCurrent().GetCurrentEnergy() < UnitManager.instance.GetCurrent().GetStatMaxEnergy())
			{
				UnitManager.instance.GetCurrent().TakeHeal(Mathf.RoundToInt(UnitManager.instance.GetCurrent().GetStatMaxHealth() * 0.5f));
				AudioManager.instance.BoostSound();
				UnitManager.instance.GetCurrent().TakeEnergyChange(Mathf.RoundToInt(UnitManager.instance.GetCurrent().GetStatMaxEnergy() * 0.5f));
				GetHPBar().SetActive(false);
				restoreCharged = false;
				GetComponentInChildren<Animator>().SetBool ("Active",false);
			}
			else
			{
				DisplayStack.instance.AddUnitEvent(UnitManager.instance.GetCurrent(), "Health & Energy already full", DisplayStackTypes.Info);
				
			}
		}
		
		//if charger is inactive, it counts the rounds and reactivates it based on how many rounds there are
		public void TurnCounter(){
			if(restoreCharged == false){
				turnCounter++;
				if(turnCounter == 3){
					restoreCharged = true;
					GetComponentInChildren<Animator>().SetBool ("Active",true);
					turnCounter = 0;
				}
			}
		}
	}
}






