using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Redemption.Tiles.Wasteland
{
	internal class DeadPalmTree : ModPalmTree
	{
		private Mod mod
		{
			get
			{
				return ModLoader.GetMod("Redemption");
			}
		}

		public override int DropWood()
		{
			return this.mod.ItemType("DeadWood");
		}

		public override Texture2D GetTexture()
		{
			return this.mod.GetTexture("Tiles/Wasteland/DeadPalmTree");
		}

		public override Texture2D GetTopTextures()
		{
			return this.mod.GetTexture("Tiles/Wasteland/DeadPalmTreeTops");
		}
	}
}
