using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Redemption.Backgrounds.Effects
{
	public class FogHandler : ModWorld
	{
		public override void PostDrawTiles()
		{
			this.darknessFog.Update(base.mod.GetTexture("Backgrounds/Effects/DarknessTex"));
			this.darknessFog.Draw(base.mod.GetTexture("Backgrounds/Effects/DarknessTex"), false, Color.White, true);
		}

		private ScreenDarkness darknessFog = new ScreenDarkness(false);
	}
}
