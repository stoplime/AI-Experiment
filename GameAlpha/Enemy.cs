using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public class Enemy
	{
		private Texture2D bulletTexture,enemy1Texture;
		private GraphicsContext graphics;
		private Sprite eTurrent,eTurrentShadow;
		private Bullets bullet;
		private List<Bullets> enemyBulletList,playerBulletList;
		private string id = "turrent";
		private int hp;
		private int range;
		private Random rand;
		private int time,delay,distToPlayer;
		private int score;
		private bool hit;
		
		public string Id
		{
			get{return id;}
		}
		public bool Hit
		{
			get{return hit;}
			set{hit = value;}
		}
		public float X
		{
			get{return eTurrent.Position.X;}
			set{eTurrent.Position.X = value;}
		}
		public float Y
		{
			get{return eTurrent.Position.Y;}
			set{eTurrent.Position.Y = value;}
		}
		public float Rot
		{
			get{return eTurrent.Rotation;}
			set{eTurrent.Rotation = value;}
		}
		public int Score
		{
			get{return score;}
		}
		public List<Bullets> PlayerBulletList
		{
			get{return playerBulletList;}
			set{playerBulletList = value;}
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
		
		public Enemy (GraphicsContext gc)
		{
			graphics = gc;
			rand = new Random();
			
			enemy1Texture = new Texture2D("/Application/assets/Enemy1.png",false);
			bulletTexture = new Texture2D("/Application/assets/bullet.png",false);
			
			eTurrent = new Sprite(graphics,enemy1Texture);
			eTurrent.Center.X = 0.5f;
			eTurrent.Center.Y = 0.5f;
			
			eTurrent.Position.X = rand.Next(16,graphics.Screen.Width-15);
			eTurrent.Position.Y = 0;
			
			eTurrentShadow = new Sprite(graphics,enemy1Texture);
			eTurrentShadow.Center.X = 0.5f;
			eTurrentShadow.Center.Y = 0.5f;
			
			eTurrentShadow.SetColor(0f,0f,0f,0.4f);
			
			
			hp = 100;
			range = 300;
			delay = 60;
			
			time = 0;
			score = 0;
		}
		
		public void Update(GamePadData gamePadData,float playerX,float playerY,ref List<Bullets> pbl,ref List<Bullets> ebl)
		{
			eTurrent.Rotation = FMath.Atan2((playerY-eTurrent.Position.Y),(playerX-eTurrent.Position.X));
			distToPlayer = (int)FMath.Sqrt(FMath.Pow(playerY-eTurrent.Position.Y,2f)+FMath.Pow(playerX-eTurrent.Position.X,2f));
			playerBulletList = pbl;
			enemyBulletList = ebl;
			
			eTurrent.Position.Y++;
			if(eTurrent.Position.Y > graphics.Screen.Height+20){
				eTurrent.Position.Y = -16;
				eTurrent.Position.X = rand.Next(16,graphics.Screen.Width-15);
			}
			
			eTurrentShadow.Position.X = eTurrent.Position.X-5;
			eTurrentShadow.Position.Y = eTurrent.Position.Y+5;
			
			if(time > delay){
				if(distToPlayer < range ){
					time = 0;
					bullet = new Bullets(bulletTexture,graphics,eTurrent.Position.X,eTurrent.Position.Y,2f*distToPlayer,true);
					bullet.X = eTurrent.Position.X;
					bullet.Y = eTurrent.Position.Y;
					bullet.Rot = eTurrent.Rotation;
					enemyBulletList.Add(bullet);
				}
			}
			//bullet updates
			for(var i=enemyBulletList.Count-1;i>0;i--){
				enemyBulletList[i].Update();
				if(enemyBulletList[i].Hit){
					enemyBulletList.RemoveAt(i);
				}
			}
			for(var i=playerBulletList.Count-1;i>0;i--){
				if(playerBulletList[i].NearHit){
					if(FMath.Pow(playerBulletList[i].Y-eTurrent.Position.Y,2f)+FMath.Pow(playerBulletList[i].X-eTurrent.Position.X,2f) <= FMath.Pow(eTurrent.Width/2,2)){
						playerBulletList.RemoveAt(i);
						score++;
						hit = true;
					}
				}
			}
			
			time ++;
			
		}
		
		public void Render()
		{
			eTurrentShadow.Render();
			
			if(enemyBulletList != null){
				for(var i=enemyBulletList.Count-1;i>0;i--){
					enemyBulletList[i].Render();
				}
			}
			eTurrent.Render();
		}
		
		
	}
}

