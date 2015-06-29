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
		public static bool StopGame;
		public static bool AIMode;
		public static Random Rand;
		public static GraphicsContext Graphics;
		public static GamePadData GPD;
		
		public static Player Player;
		public static Background[] BG;
		public static Sprite EndGameSprite;
		public static int Score;
		public static Text ScoreDisplay;
		
		public static List<Texture2D> Textures;
		public static List<Enemy> Enemies;
		public static List<Explosions> ExplodeEffects;
		public static List<Bullets> PlayerBullets;
		public static List<Bullets> EnemyBullets;
		
		public static AIPlayer AI;
		public static InputPlayer IP;
	}
}

