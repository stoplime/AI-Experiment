using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;

namespace GameAlpha
{
	public class Explosions
	{
		private int time,delay,slide;
		private const int slideMax = 5;
		private Sprite explode;
		private bool kill;
		
		public bool Kill
		{
			get{return kill;}
		}
		
		public Explosions (Vector3 pos)
		{
			time = 0;
			delay = 5;//frames till next slide
			slide = 0;
			
			explode = new Sprite(Global.Graphics,Global.Textures[3]);
			explode.Center = new Vector2(0.5f,0.5f);
			explode.SetTextureUV(0,0,1/slideMax,1);
		}
		
		public void Update()
		{
			time++;
			if(time >= delay){
				time = 0;
				slide++;
			}
			if(slide >= slideMax) kill = true;
		}
		
		public void Render()
		{
			int frame = 1/slideMax;
			explode.SetTextureUV(slide*frame,0,(slide+1)*frame,1);
			explode.Render();
		}
		
	}
}

