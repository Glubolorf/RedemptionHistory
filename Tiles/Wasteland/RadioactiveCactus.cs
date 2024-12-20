using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	public class RadioactiveCactus : ModCactus
	{
		private Mod mod
		{
			get
			{
				return ModLoader.GetMod("Redemption");
			}
		}

		public override Texture2D GetTexture()
		{
			return this.mod.GetTexture("Tiles/Wasteland/RadioactiveCactus");
		}
	}
}
