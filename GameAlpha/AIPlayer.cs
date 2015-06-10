using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace GameAlpha
{
	public class AIPlayer
	{
		private GraphicsContext graphics;
		//private Player playerState0;
		//private Enemy enemyState0;
		private Vector3[] optimalPos;
		private Vector3 currentOptimalPos;
		private int score0, currentRun;
		private AIstates s;
		//private bool[] decision0,decision1;
		
		//Non-universal coordinates
		private const int goalMinY = -250;
		private const int goalMinX = -16;
		private const int goalMaxY = -100;
		private const int goalMaxX = 16;
		
		private float distY,distX,preDistY,preDistX;
		
		//private int[] featuresScore;
		private int[] featureValue;
		private Random rand;
		//private int current;
		
		public AIPlayer (GraphicsContext graphics)
		{
			this.graphics = graphics;
			rand = new Random();
			currentRun = 0;
			currentOptimalPos = new Vector3(rand.Next(0,graphics.Screen.Width),rand.Next(0,graphics.Screen.Height),0);
			optimalPos = new Vector3[10];
			//featuresScore = new int[5];
			featureValue = new int[5];
			for(var i=0;i<5;i++){
				featureValue[i] = (int)(100*rand.NextDouble());
			}
			
		}
		
		public void ScoreAndReset(int score)//when AI dies and resets
		{
			s = new AIstates();
			
			//sets random as for control
//			for(var i=0;i<5;i++){
//				featureValue[i] = (int)(100*rand.NextDouble());
//			}
			currentOptimalPos.Z = score;
			optimalPos[currentRun] = currentOptimalPos;
			
			Vector3 highestScorePos = currentOptimalPos;
			for(var i=0;i<optimalPos.Length;i++){
				if(optimalPos[i] != null){
					//for checking highest optimal pos score
					if(optimalPos[i].Z > highestScorePos.Z){
						highestScorePos = optimalPos[i];
						
						
					}
				}
			}
			
			if(currentOptimalPos.Equals(highestScorePos)){
				//take the second highest score and project a better optimal pos
				//or random for the time being
				currentOptimalPos.X = rand.Next(0,graphics.Screen.Width);
				currentOptimalPos.Y = rand.Next(0,graphics.Screen.Height);
				currentOptimalPos.Z = 0;
			}else{
				//revert back the the highest score
				currentOptimalPos = highestScorePos;
			}
			
			if(currentRun<optimalPos.Length-1){
				currentRun++;
			}else{
				currentRun = 0;
			}
		}
		
		public void Update(int score,Player playerState, Enemy enemyState)
		{
			for(var i=0;i<5;i++){
				featureValue[i] = (int)(100*rand.NextDouble());
			}
			
			//playerState0 = playerState;
			//enemyState0 = enemyState;
			score0 = score;
			//decision0 = decision1;
			
			
//			distX = enemyState.X-playerState.X;
//			distY = enemyState.Y-playerState.Y;
//			
//			if(distX>goalMaxX){
//				featureValue[1] = 100;
//			}
//			if(distX<goalMinX){
//				featureValue[0] = 100;
//			}
//			if(distY>goalMaxY){
//				featureValue[3] = 100;
//			}
//			if(distY<goalMinY){
//				featureValue[2] = 100;
//			}
			if(playerState.X>currentOptimalPos.X){
				featureValue[0] = 100;
			}
			if(playerState.X<currentOptimalPos.X){
				featureValue[1] = 100;
			}
			if(playerState.Y>currentOptimalPos.Y){
				featureValue[3] = 100;
			}
			if(playerState.Y<currentOptimalPos.Y){
				featureValue[2] = 100;
			}
			
		}
		
		public bool MoveLeft()
		{
			if(rand.Next(0,100)<featureValue[0]){
				return true;
			}else{
				return false;
			}
		}
		public bool MoveRight()
		{
			if(rand.Next(0,100)<featureValue[1]){
				return true;
			}else{
				return false;
			}
		}
		public bool MoveUp()
		{
			if(rand.Next(0,100)<featureValue[2]){
				return true;
			}else{
				return false;
			}
		}
		public bool MoveDown()
		{
			if(rand.Next(0,100)<featureValue[3]){
				return true;
			}else{
				return false;
			}
		}
		public bool FireRate()
		{
			return true;
		}
		
	}
}

