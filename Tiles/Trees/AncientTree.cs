using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Tiles;
using Terraria.ModLoader;

namespace Redemption.Tiles.Trees
{
	public class AncientTree : ModTree
	{
		private Mod mod
		{
			get
			{
				return ModLoader.GetMod("Redemption");
			}
		}

		public override int CreateDust()
		{
			return 78;
		}

		public override int GrowthFXGore()
		{
			return this.mod.GetGoreSlot("Gores/Misc/AncientTreeFX");
		}

		public override int DropWood()
		{
			return ModContent.ItemType<AncientWood>();
		}

		public override Texture2D GetTexture()
		{
			return this.mod.GetTexture("Tiles/Trees/AncientTree");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
		{
			return this.mod.GetTexture("Tiles/Trees/AncientTree_Tops");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return this.mod.GetTexture("Tiles/Trees/AncientTree_Branches");
		}
	}
}
