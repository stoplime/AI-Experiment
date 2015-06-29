//********************************************** Name: Steffen Lim *********************************************

using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Sce.PlayStation.HighLevel.UI;

namespace GameAlpha
{
	public class AppMain
	{
		public static void Main (string[] args)
		{
			Initialize ();

			while (Global.Run) {
				SystemEvents.CheckEvents ();
				BasicUpdate();
				if(!Global.StopGame){
					Update ();
				}else{
					UpdateEndGame();
				}
				Render ();
			}
		}

		public static void Initialize ()
		{
			// Set up the graphics system
			Global.Graphics = new GraphicsContext ();
			
			Global.AI = new AIPlayer();
			
			Global.BG = new Background[2];
			Global.BG[0] = new Background(Global.Textures[0]);
			Global.BG[1] = new Background(Global.Textures[1]);
			
			Global.EndGameSprite = new Sprite(Global.Graphics,Global.Textures[2]);
			
			Global.Run = true;
			Global.StopGame = false;
			Global.Score = 0;
			Global.ScoreDisplay = new Text(10,10,"Score: "+Global.Score);
			
			Global.Player = new Player();
			Global.Enemies = new List<Enemy>();
			
			Global.EnemyBullets = new List<Bullets>();
			Global.PlayerBullets = new List<Bullets>();
			
			UISystem.Initialize(Global.Graphics);
		}
		
		public static void NewGame () 
		{
			
		}
		
		public static void initTextures () 
		{
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll.png",false));		//	0	BG 1
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll3.png",false));	//	1	BG 2
			
			Global.Textures.Add(new Texture2D("/Application/assets/White.png",false));					//	2	White texture
			Global.Textures.Add(new Texture2D("/Application/assets/explosion.png",false));				//	3	explode 1
			
		}

		public static void BasicUpdate ()
		{
			// Query gamepad for current state
			Global.GPD = GamePad.GetData (0);
			//kills game when pressed z
			if((Global.GPD.Buttons & GamePadButtons.Select) != 0){
				Global.Run = false;
			}
			
			Global.ScoreDisplay.Update("Score: "+Global.Score);
			
		}

		public static void Update ()
		{
			if((Global.GPD.Buttons & GamePadButtons.L) != 0){
				Global.AIMode = false;
			}
			
			//character
			if(Global.AIMode){
				Global.Player.Update();
				
			}else{
				Global.Player.Update();
				
			}
			
			ai.Update(scoreValue,Global.Player,turrent);
			
			if(Global.Player.Hit){
				Global.ExplodeEffects.Add(new Explosions(Global.Player.Pos));
				Global.Player.Hit = false;
				if(Global.Player.Hp <= 0){
					stopGame = true;
				}
			}
			
			//enemy
			turrent.Update(gamePadData,player.X,player.Y,ref playerBulletList, ref enemyBulletList);
			if(turrent.Hit){
				expos = new Vector3(turrent.X,turrent.Y,0);
				explode = new Explosions(Global.Graphics,ex0,ex1,ex2,expos);
				
				turrent.Hit = false;
				turrent.Y = 1000;
			}
			
			scoreValue = turrent.Score;
			
			//Score display
			score.Text = "Press s to shoot:"+scoreValue+"                                   "+iteration+"                                           "+"Health:"+player.Hp;
			
			//background
			Global.BG[0].Update(0.5f);
			Global.BG[1].Update(1f);
			
			if(explode != null){
				explode.Update();
				if(explode.Kill){
					explode = null;
				}
			}
			if(explodePlayer != null){
				explodePlayer.Update();
				if(explodePlayer.Kill){
					explodePlayer = null;
				}
			}
			
			if((gamePadData.Buttons & GamePadButtons.Start) != 0 && (gamePadData.ButtonsPrev & GamePadButtons.Start) == 0){
				ai.ScoreAndReset(scoreValue);
				iteration++;
				explodePlayer = null;
				explode = null;
				Initialize();
				stopGame = false;
			}
			
		}
		
		public static void UpdateEndGame()
		{
			var gamePadData = GamePad.GetData (0);
			if((gamePadData.Buttons & GamePadButtons.Select) != 0){
				run = false;
			}
			IP.Dispose();
			score.X = 300;
			score.Y = 260;
			score.Text = "Score:"+scoreValue+"  Press Z to End Game. X to restart";
			
			if((gamePadData.Buttons & GamePadButtons.Start) != 0){
				ai.ScoreAndReset(scoreValue);
				iteration++;
				explodePlayer = null;
				explode = null;
				Initialize();// init again
				stopGame = false;
			}
			
			endGame.SetColor(0f,0f,0f,alpha);
			alpha+=0.01f;
		}

		public static void Render ()
		{
			// Clear the screen
			Global.Graphics.SetClearColor (0.0f, 0.0f, 0.0f, 0.0f);
			Global.Graphics.Clear ();
			
			bg.Render();
			bg2.Render();
			
			turrent.Render();
			for(var i=playerBulletList.Count-1;i>0;i--){
				playerBulletList[i].Render();
			}
			if(explode != null){
				explode.Render();
			}
			
			player.Render();
			
			if(explodePlayer != null){
				explodePlayer.Render();
			}
			
			if(stopGame){
				endGame.Render();
				
			}
			
			UISystem.Render();
			
			// Present the screen
			Global.Graphics.SwapBuffers ();
		}
	}
}
