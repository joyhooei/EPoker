namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.MVVM;
	using uFrame.IOC;
	using uFrame.Kernel;
	using UniRx;
	using UnityEngine;
	using GameDataEditor;

	public class CommonDataStruct : CommonDataStructBase
	{

		public override void Setup ()
		{
			base.Setup ();

			GDEDataManager.Init ("gde_data");

			GDESConfigData cd = new GDESConfigData (GDEItemKeys.SConfig_DefaultConfig);
			string game_mode_str = cd.GameMode;

			GameService.GameMode = (GameMode)Enum.Parse (typeof(GameMode), game_mode_str);

			Debug.Log ("Game mode: " + GameService.GameMode.ToString ());
		}
	}
}
