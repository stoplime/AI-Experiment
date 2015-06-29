using System;

namespace GameAlpha
{
	public class Spawner
	{
		private int timer;
		
		public Spawner ()
		{
			timer = 0;
			
		}
		
		public void Update()
		{
			timer++;
			if (timer > 300) {
				timer = 0;
				//Spawn enemy
				Global.Enemies.Add(new Enemy());
			}
		}
		
	}
}

