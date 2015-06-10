using System;

namespace GameAlpha
{
	public class AIstates
	{
		public enum enumState{
			//0
			Under,
			Above,
			//1
			TooLeft,
			TooRight,
			//2
			NoERange,
			//3
			TooClose,
			//4
			BinRange,
			//5
			BtooClose,
			//6
			WallLeft,
			WallRight,
			//7
			WallAbove,
			WallUnder,
			
			Null
		}
		
		public enumState[] states;
		
		public AIstates()
		{
			states = new enumState[8];
			states[0] = enumState.Under;
			states[1] = enumState.Null;
			states[2] = enumState.NoERange;
			states[3] = enumState.Null;
			states[4] = enumState.Null;
			states[5] = enumState.Null;
			states[6] = enumState.Null;
			states[7] = enumState.Null;
		}
	}
}

