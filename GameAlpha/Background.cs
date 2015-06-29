using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public class Background
	{
		private Sprite bg1,bg2;
		private int height;
		
		public Background (Texture2D tx1)
		{
			bg1 = new Sprite(Global.Graphics,tx1);
			bg2 = new Sprite(Global.Graphics,tx1);
			
			height = Global.Graphics.Screen.Height;
			
			bg2.Position.Y = -height;
		}
		
		public void Update(float speed)
		{
			//Background update
			bg1.Position.Y+=speed;
			bg2.Position.Y+=speed;
			if(bg1.Position.Y >= height){
				bg1.Position.Y = -height;
			}
			if(bg2.Position.Y >= height){
				bg2.Position.Y = -height;
			}
		}
		
		public void Render()
		{
			bg1.Render();
			bg2.Render();
		}
		
	}
}