using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Tiles;
using Terraria.ModLoader;

namespace Redemption.Tiles.Trees
{
	public class DeadTree : ModTree
	{
		private Mod mod
		{
			get
			{
				return ModLoader.GetMod("Redemption");
			}
		}

		public override int GrowthFXGore()
		{
			return this.mod.GetGoreSlot("Gores/Misc/DeadTreeFX");
		}

		public override int CreateDust()
		{
			return 214;
		}

		public override int DropWood()
		{
			return ModContent.ItemType<DeadWood>();
		}

		public override Texture2D GetTexture()
		{
			return this.mod.GetTexture("Tiles/Trees/DeadTree");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
		{
			return this.mod.GetTexture("Tiles/Trees/DeadTree_Tops");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return this.mod.GetTexture("Tiles/Trees/DeadTree_Branches");
		}
	}
}
