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
		private static Player player;
		private static Enemy turrent;
		private static Label score;
		private static bool run = true;
		private static int scoreValue;
		private static Explosions explode,explodePlayer;
		private static List<Bullets> playerBulletList, enemyBulletList;
		private static Texture2D ex0,ex1,ex2,whiteTexture;
		private static Vector3 expos,exposPlayer;
		private static bool stopGame,AIMode;
		private static float alpha;
		private static Sprite endGame;
		private static int iteration = 0;
		
		private static InputPlayer IP;
		
		private static AIPlayer ai;
		
		public static void Main (string[] args)
		{
			IP = new InputPlayer();
			Initialize ();

			while (run) {
				SystemEvents.CheckEvents ();
				if(stopGame == false){
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
			
			AIMode = true;
			stopGame = false;
			alpha = 0f;
			whiteTexture = new Texture2D("/Application/assets/White.png",false);
			endGame = new Sprite(Global.Graphics,whiteTexture);
			
			Texture2D bgTexture1 = new Texture2D("/Application/assets/Bg_vertical_scroll.png",false);
			bg = new Background(Global.Graphics,bgTexture1);
			Texture2D bgTexture2 = new Texture2D("/Application/assets/Bg_vertical_scroll3.png",false);
			bg2 = new Background(Global.Graphics,bgTexture2);
			
			//character
			player = new Player(Global.Graphics);
			playerBulletList = new List<Bullets>();
			
			//enemy
			turrent = new Enemy(Global.Graphics);
			enemyBulletList = new List<Bullets>();
			
			scoreValue = 0;
			
			ex0 = new Texture2D("/Application/assets/explosion0.png",false);
			ex1 = new Texture2D("/Application/assets/explosion1.png",false);
			ex2 = new Texture2D("/Application/assets/explosion2.png",false);
			
			UISystem.Initialize(Global.Graphics);
			Scene scene = new Scene();
			score = new Label();
			score.X = 10;
			score.Y = 10;
			score.Width = 940;
			score.Text = ""+scoreValue;
			scene.RootWidget.AddChildLast(score);
			UISystem.SetScene(scene, null);
			
		
			
		}
		
		public static void NewGame () 
		{
			
		}
		
		public static void initTextures () 
		{
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll.png",false));		//	0	BG 1
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll3.png",false));	//	1	BG 2
			
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll.png",false));	//	0	BG 1
			
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll.png",false));	//	0	BG 1
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll.png",false));	//	0	BG 1
			Global.Textures.Add(new Texture2D("/Application/assets/Bg_vertical_scroll.png",false));	//	0	BG 1
			
		}

		public static void Update ()
		{
			// Query gamepad for current state
			var gamePadData = GamePad.GetData (0);
			
			//kills game when pressed z
			if((gamePadData.Buttons & GamePadButtons.Select) != 0){
				run = false;
			}
			if((gamePadData.Buttons & GamePadButtons.L) != 0){
				AIMode = false;
			}
			//turrent.PlayerBulletList = player.PlayerBulletList;//syncs lists
			
			
			//character
			if(AIMode){
				player.Update(gamePadData,scoreValue,ref playerBulletList, ref enemyBulletList, ai.MoveLeft(),ai.MoveRight(),ai.MoveUp(),ai.MoveDown(),ai.FireRate());
				
			}else{
				player.Update(gamePadData,scoreValue,ref playerBulletList, ref enemyBulletList, false,false,false,false,false);
				IP.Update(gamePadData);
			}
			
			ai.Update(scoreValue,player,turrent);
			
			if(player.Hit){
				exposPlayer = new Vector3(player.X,player.Y,0);
				explodePlayer = new Explosions(Global.Graphics,ex0,ex1,ex2,exposPlayer);
				
				player.Hit = false;
				if(player.Hp == 0){
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
			bg.Update(0.5f);
			bg2.Update(1f);
			
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
