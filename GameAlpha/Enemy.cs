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
		private Sprite enemy, 	enemyShadow;
		private Vector3 pos,	posShadow;
		private float rot;
		
		private int timer;
		
		#region parameters
		private const float range = 200;
		private const int fireRate = 60;
		#endregion
		
		#region properties
		private bool dead;
		public bool Dead
		{
			get{return dead;}
			set{dead = value;}
		}
		#endregion
		
		#region constructor
		public Enemy (Vector3 pos)
		{
			this.pos = pos;
			posShadow = pos + new Vector3(-5,5,0);
			dead = false;
			
			enemy = new Sprite(Global.Graphics,Global.Textures[]);
			enemy.Center = new Vector2(0.5f,0.5f);
			enemy.Position = pos;
			
			enemyShadow = new Sprite(Global.Graphics,Global.Textures[]);
			enemyShadow.Center = enemy.Center();
			enemyShadow.Position = posShadow;
			
			timer = 0;
		}
		#endregion
		
		public void Update(GamePadData gamePadData,float playerX,float playerY,ref List<Bullets> pbl,ref List<Bullets> ebl)
		{
			timer++;
			
			pos.Y++;
			rot = pos.Angle(Global.Player.Pos);
			
			if(pos.Y > Global.Graphics.Screen.Height+20){
				dead = true;
			}
			
			posShadow = pos + new Vector3(-5,5,0);
			
			if(timer > fireRate){
				if(pos.DistanceSquared(Global.Player.Pos) < range*range ){
					time = 0;
					//****************************************** Change Bullets ************************************
					bullet = new Bullets(bulletTexture,graphics,eTurrent.Position.X,eTurrent.Position.Y,2f*distToPlayer,true);
					bullet.X = eTurrent.Position.X;
					bullet.Y = eTurrent.Position.Y;
					bullet.Rot = eTurrent.Rotation;
					enemyBulletList.Add(bullet);
				}
			}
			
			enemy.Position = pos;
			enemyShadow.Position = posShadow;
			enemy.Rotation = rot;
			enemyShadow.Rotation = rot;
		}
		
		public void Render()
		{
			enemyShadow.Render();
			
			if(Global.EnemyBullets != null){
				foreach (Bullets b in Global.EnemyBullets) {
					b.Render();
				}
			}
			
			eTurrent.Render();
		}
		
		
	}
}

