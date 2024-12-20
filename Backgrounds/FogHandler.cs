using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Redemption.Backgrounds
{
	public class FogHandler : ModWorld
	{
		public override void PostDrawTiles()
		{
			this.wastelandFog.Update(base.mod.GetTexture("Backgrounds/fog"));
			this.wastelandFog.Draw(base.mod.GetTexture("Backgrounds/fog"), false, Color.White, true);
		}

		private ScreenFog wastelandFog = new ScreenFog(false);
	}
}
