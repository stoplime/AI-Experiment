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
		private GraphicsContext graphics;
		private Sprite bg1,bg2;
		private Texture2D BgTexture;
		private int height;
		
		public Background (GraphicsContext gc, Texture2D tx1)
		{
			graphics = gc;
			BgTexture = tx1;
			bg1 = new Sprite(graphics,BgTexture);
			bg2 = new Sprite(graphics,BgTexture);
			
			height = graphics.Screen.Height;
			
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