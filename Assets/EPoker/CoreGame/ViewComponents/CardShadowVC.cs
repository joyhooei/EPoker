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
	using Unity.Linq;
	using UnityEngine;

	public class CardShadowVC : CardShadowVCBase
	{

		void Update ()
		{
			GameObject main_body = gameObject.BeforeSelf ("main_body").Single ();
			transform.localPosition = main_body.transform.localPosition;
		}
	}
}
