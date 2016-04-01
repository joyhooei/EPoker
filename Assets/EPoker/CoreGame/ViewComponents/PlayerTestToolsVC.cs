namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using yigame.epoker;
	using UnityEngine;

	public class PlayerTestToolsVC : PlayerTestToolsVCBase
	{

		public void Update ()
		{
			if (Input.GetKeyDown ("=")) {
				
				List<CardInfo> cil = GameService.GetDeck (true);

				Player.Execute (new AddCardsCommand () {
					CardInfos = new List<CardInfo> () {
						cil.First ()
					}
				});
			}

			if (Input.GetKeyDown ("-")) {
				if (Player.HandCards.Count > 0) {
					CardInfo ci = Player.HandCards.LastOrDefault ().Info;
					Player.Execute (new RemoveCardsCommand () {
						CardInfos = new List<CardInfo> () {
							ci
						}
					});
				}
			}

		}
	}
}
