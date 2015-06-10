using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public class Explosions
	{
		private int time,delay,slide,slideMax;
		private GraphicsContext graphics;
		private Sprite exp0,exp1,exp2;
		private Texture2D expT0,expT1,expT2;
		private bool kill;
		
		public bool Kill
		{
			get{return kill;}
		}
		
		public Explosions (GraphicsContext gc,Texture2D T0,Texture2D T1,Texture2D T2,Vector3 pos)
		{
			graphics = gc;
			expT0 = T0;
			expT1 = T1;
			expT2 = T2;
			
			time = 0;
			delay = 5;//frames till next slide
			slide = 0;
			slideMax = 5;
			
			exp0 = new Sprite(graphics,expT0);
			exp1 = new Sprite(graphics,expT1);
			exp2 = new Sprite(graphics,expT2);
			
			exp0.Center.X = 0.5f;
			exp0.Center.Y = 0.5f;
			exp1.Center.X = 0.5f;
			exp1.Center.Y = 0.5f;
			exp2.Center.X = 0.5f;
			exp2.Center.Y = 0.5f;
			
			exp0.Position = pos;
			exp1.Position = pos;
			exp2.Position = pos;
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
			if(slide == 0){
				exp0.Render();
			}else if(slide == 1){
				exp0.Render();
				exp1.Render();
			}else if(slide == 2){
				exp0.Render();
				exp1.Render();
				exp2.Render();
			}else if(slide == 3){
				exp1.Render();
				exp2.Render();
			}else if(slide == 4){
				exp2.Render();
			}
			
		}
		
	}
}

