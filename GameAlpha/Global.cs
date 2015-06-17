using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;

namespace GameAlpha
{
	public static class Global
	{
		public static bool Run;
		public static GraphicsContext Graphics;
		
		public static Player Player;
		public static Background[] BG;
		
		public static List<Texture2D> Textures;
		public static List<Enemy> Enemies;
		public static List<Explosions> ExplodeEffects;
		
		public static AIPlayer AI;
		public static InputPlayer IP;
	}
}

