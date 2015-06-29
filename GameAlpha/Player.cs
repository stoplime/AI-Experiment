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
		private Sprite player,playerShadow;
		private Vector3 pos;
		private float speed;
		private int hp;
		private int time;
		private int fireRate;
		private bool hit;
		
		public float Pos
		{
			get{return pos;}
			set{pos = value;}
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
		
		public Player ()
		{
			player = new Sprite(Global.Graphics,Global.Textures[4]);
			player.Center = new Vector2(0.5f,0.5f);
			
			pos.X = graphics.Screen.Rectangle.Width/2;
			pos.Y = graphics.Screen.Rectangle.Height/2+200;
			
			playerShadow = new Sprite(Global.Graphics,Global.Textures[4]);
			playerShadow.Center = new Vector2(0.5f,0.5f);
			playerShadow.Scale = new Vector2(0.7f,0.7f);
			
			playerShadow.SetColor(0f,0f,0f,0.4f);
			
			
			hp = 5;
			speed = 2f;
			fireRate = 10;
			
			time = 0;
			
		}
		
		public void Update(GamePadData gamePadData)
		{
			float hWidth = player.Width/2;
			float hHeight = player.Height/2;
			//movement
			if((gamePadData.Buttons & GamePadButtons.Left) != 0){
				if((pos.X-hWidth) > 0){
					pos.X -= speed;
				}
			}
			if((gamePadData.Buttons & GamePadButtons.Right) != 0){
				if((pos.X+hWidth) < Global.Graphics.Screen.Width){
					pos.X += speed;
				}
			}
			if((gamePadData.Buttons & GamePadButtons.Up) != 0){
				if((pos.Y-hHeight) > 0){
					pos.Y -= speed;
				}
			}
			if((gamePadData.Buttons & GamePadButtons.Down) != 0){
				if((pos.Y+hHeight) < Global.Graphics.Screen.Height){
					pos.Y += speed;
				}
			}
			
			playerShadow.Position = new Vector3(pos.X-50,pos.Y+50,0);
			
			//firing
			if((gamePadData.Buttons & GamePadButtons.Cross) != 0){
				if(time > fireRate){
					time = 0;
					bullet = new Bullets(pos,-FMath.PI/2);
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

