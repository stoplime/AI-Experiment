using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public class Bullets
	{
		private Texture2D bulletTexture;
		private GraphicsContext graphics;
		private Sprite bullet,bulletShadow;
		private float speed;
		private float rot,heightPer;
		private float initX,initY;
		private float bulletDistMax,bulletDist;
		private bool groundToAir,hit;
		
		public float X
		{
			get{return bullet.Position.X;}
			set{bullet.Position.X = value;}
		}
		public float Y
		{
			get{return bullet.Position.Y;}
			set{bullet.Position.Y = value;}
		}
		public float Rot
		{
			get{return rot;}
			set{rot = value;}
		}
		public bool Hit
		{
			get{return hit;}
		}
		public bool NearHit
		{
			get
			{
				if(bulletDistMax-bulletDist <=150){
					return true;
				}else{
					return false;
				}
			}
		}
		public bool NoRender
		{
			get{if(X > graphics.Screen.Width){
					return true;
				}else if(Y > graphics.Screen.Height){
					return true;
				}else if(X < 0){
					return true;
				}else if(Y < 0){
					return true;
				}else{
					return false;
				}
			}
		}
		//****************** Constructors ***********************
		public Bullets (Texture2D tx, GraphicsContext gc,float x,float y)
		{
			Initialize(tx,gc,x,y,250,false);
			
		}
		public Bullets (Texture2D tx, GraphicsContext gc,float x,float y,float dist,bool ground_to_air)
		{
			Initialize(tx,gc,x,y,dist,ground_to_air);
		}
		public void Initialize(Texture2D tx, GraphicsContext gc,float x,float y,float dist,bool ground_to_air)
		{
			graphics = gc;
			bulletTexture = tx;
			initX = x;
			initY = y;
			bulletDistMax = dist;
			groundToAir = ground_to_air;
			
			speed = 4f;
			rot = 0f;
			bulletDist = 0;
			hit = false;
			
			bullet = new Sprite(graphics,bulletTexture);
			bullet.Center.X = 0.5f;
			bullet.Center.Y = 0.5f;
			bullet.SetColor(0.8f, 0.8f, 0.8f, 1f);
			
			bullet.Position.X=initX;
			bullet.Position.Y=initY;
			
			bulletShadow = new Sprite(graphics,bulletTexture);
			bulletShadow.Center.X = 0.5f;
			bulletShadow.Center.Y = 0.5f;
			bulletShadow.SetColor(0f, 0f, 0f, 0.4f);
		}
		
		public void Update()
		{
			bullet.Rotation = rot-FMath.PI/2;
			bullet.Position.Y += FMath.Sin(rot)*speed;
			bullet.Position.X += FMath.Cos(rot)*speed;
			
			bulletDist = FMath.Sqrt(FMath.Pow(bullet.Position.Y-initY,2f)+FMath.Pow(bullet.Position.X-initX,2f));
			
			bulletShadow.Rotation = bullet.Rotation;
			
			if(groundToAir){
				heightPer = -4f*bulletDist*bulletDist/bulletDistMax/bulletDistMax+4f*bulletDist/bulletDistMax;
				bulletShadow.Position.X = bullet.Position.X-50*heightPer;
				bulletShadow.Position.Y = bullet.Position.Y+50*heightPer;
				bulletShadow.Scale.X = 1f-(0.3f*heightPer);
				bulletShadow.Scale.Y = 1f-(0.3f*heightPer);
			}else{
				heightPer = 1f-(1f*bulletDist/bulletDistMax);
				bulletShadow.Position.X = bullet.Position.X-50*heightPer;
				bulletShadow.Position.Y = bullet.Position.Y+50*heightPer;
				bulletShadow.Scale.X = 1f-(0.3f*heightPer);
				bulletShadow.Scale.Y = 1f-(0.3f*heightPer);
			}
			
			if(bulletDist >= bulletDistMax){
				hit = true;
			}
			
		}
		public void Render()
		{
			bulletShadow.Render();
			bullet.Render();
		}
		
	}
}

