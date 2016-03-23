namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

    
	public class CoreGameScene : CoreGameSceneBase
	{
		public override void KernelLoaded ()
		{
			base.KernelLoaded ();
			UnityEngine.Debug.Log ("CoreGameScene: KernelLoaded");
		}
	}
}
