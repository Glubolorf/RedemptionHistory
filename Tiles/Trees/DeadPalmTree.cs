﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Tiles;
using Terraria.ModLoader;

namespace Redemption.Tiles.Trees
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
			return ModContent.ItemType<DeadWood>();
		}

		public override Texture2D GetTexture()
		{
			return this.mod.GetTexture("Tiles/Trees/DeadPalmTree");
		}

		public override Texture2D GetTopTextures()
		{
			return this.mod.GetTexture("Tiles/Trees/DeadPalmTreeTops");
		}
	}
}
