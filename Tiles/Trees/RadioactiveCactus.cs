using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Redemption.Tiles.Trees
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
			return this.mod.GetTexture("Tiles/Trees/RadioactiveCactus");
		}
	}
}
