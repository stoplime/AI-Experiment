using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public class Player
	{
		private Texture2D bulletTexture,playerTexture;
		private GraphicsContext graphics;
		private Sprite player,playerShadow;
		private List<Bullets> enemyBulletList,playerBulletList;
		private string id = "player";
		private float speed;
		private int hp;
		private Bullets bullet;
		private int time,delay;
		private bool hit;
		
		public string Id
		{
			get{return id;}
		}
		public float X
		{
			get{return player.Position.X;}
			set{player.Position.X = value;}
		}
		public float Y
		{
			get{return player.Position.Y;}
			set{player.Position.Y = value;}
		}
		public float Rot
		{
			get{return player.Rotation;}
			set{player.Rotation = value;}
		}
		public bool Hit
		{
			get{return hit;}
			set{hit = value;}
		}
		public int Hp
		{
			get{return hp;}
			set{
				if(value >= 0){
					hp = (int)value;
				}else{
					hp = 0;
				}
			}
		}
		
		public Player (GraphicsContext gc)
		{
			graphics = gc;
			playerTexture = new Texture2D("/Application/assets/Aircraft.png",false);
			bulletTexture = new Texture2D("/Application/assets/bullet.png",false);
			
			player = new Sprite(graphics,playerTexture);
			player.Center.X = 0.5f;
			player.Center.Y = 0.5f;
			
			player.Position.X = graphics.Screen.Rectangle.Width/2;
			player.Position.Y = graphics.Screen.Rectangle.Height/2+200;
			
			playerShadow = new Sprite(graphics,playerTexture);
			playerShadow.Center.X = 0.5f;
			playerShadow.Center.Y = 0.5f;
			playerShadow.Scale.X = 0.7f;
			playerShadow.Scale.Y = 0.7f;
			
			playerShadow.SetColor(0f,0f,0f,0.4f);
			
			
			hp = 5;
			speed = 2f;
			delay = 10;
			
			time = 0;
			
		}
		
		public void Update(GamePadData gamePadData, int v, ref List<Bullets> pbl,ref List<Bullets> ebl,bool l,bool r,bool u, bool d, bool f)
		{
			playerBulletList = pbl;
			enemyBulletList = ebl;
			//movement
			if((gamePadData.Buttons & GamePadButtons.Left) != 0 || l){
				if((player.Position.X-player.Width/2) > 0){
					player.Position.X -= speed;
				}
			}
			if((gamePadData.Buttons & GamePadButtons.Right) != 0 || r){
				if((player.Position.X+player.Width/2) < graphics.Screen.Rectangle.Width){
					player.Position.X += speed;
				}
			}
			if((gamePadData.Buttons & GamePadButtons.Up) != 0 || u){
				if((player.Position.Y-player.Height/2) > 0){
					player.Position.Y -= speed;
				}
			}
			if((gamePadData.Buttons & GamePadButtons.Down) != 0 || d){
				if((player.Position.Y+player.Height/2) < graphics.Screen.Rectangle.Height){
					player.Position.Y += speed;
				}
			}
			
			playerShadow.Position.X = player.Position.X-50;
			playerShadow.Position.Y = player.Position.Y+50;
			
			//firing
			if((gamePadData.Buttons & GamePadButtons.Cross) != 0 || f){
				if(time > delay){
					time = 0;
					bullet = new Bullets(bulletTexture,graphics,player.Position.X,player.Position.Y);
					bullet.X = player.Position.X;
					bullet.Y = player.Position.Y;
					bullet.Rot = -FMath.PI/2;
					playerBulletList.Add(bullet);
				}
			}
			//bullet updates
			for(var i=playerBulletList.Count-1;i>0;i--){
				playerBulletList[i].Update();
				if(playerBulletList[i].Hit){
					playerBulletList.RemoveAt(i);
				}
			}
			
			for(var i=enemyBulletList.Count-1;i>0;i--){
				if(FMath.Pow(enemyBulletList[i].Y-player.Position.Y,2f)+FMath.Pow(enemyBulletList[i].X-player.Position.X,2f) <= FMath.Pow(player.Width/4,2)){
					enemyBulletList.RemoveAt(i);
					hp--;
					hit = true;
				}
			}
			
			if((gamePadData.Buttons & GamePadButtons.L) != 0){
				v++;
			}
			
			time++;
			
		}
		
		public void Render()
		{
			playerShadow.Render();
			
			
			
			player.Render();
			
			//view ^^^^
		}
		
		
	}
}

