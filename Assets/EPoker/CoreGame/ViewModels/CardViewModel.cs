namespace yigame.epoker
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using uFrame.IOC;
	using uFrame.Kernel;
	using uFrame.MVVM;
	using uFrame.MVVM.Bindings;
	using uFrame.Serialization;
	using UnityEngine;
	using UniRx;

    
	public partial class CardViewModel : CardViewModelBase
	{
		public static Vector3 PositionUnitDiff = new Vector3 (.3f, 0f, -.01f);

		public override string ComputeCardInfoStr ()
		{
			return string.Format ("{0} : v({1}) : p({2})", Info.ToString (), CardInfo.NumericalValueToCardSize (Info.NumericalValue), Info.GetCardFrontPrefabName ());
		}

		public override Vector3 ComputeLocalPosition ()
		{
			if (TotalCount == 0) {
				return Vector3.zero;
			} else {
				return new Vector3 (
					PosIdx * PositionUnitDiff.x - (TotalCount - 1) * PositionUnitDiff.x / 2f,
					PositionUnitDiff.y,
					PosIdx * PositionUnitDiff.z
				);
			}
		}
	}
}
