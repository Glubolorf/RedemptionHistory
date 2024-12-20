using System;
using Redemption.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable
{
	public class PlantMatter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plant Matter");
			base.Tooltip.SetDefault("Use at an Extractinator");
			ItemID.Sets.ExtractinatorMode[base.item.type] = base.item.type;
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 16;
			base.item.maxStack = 999;
			base.item.useTurn = true;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.useStyle = 1;
			base.item.consumable = true;
			base.item.createTile = ModContent.TileType<PlantMatterTile>();
		}

		public override void ExtractinatorUse(ref int resultType, ref int resultStack)
		{
			int num = Main.rand.Next(14);
			if (num == 0)
			{
				if (Main.rand.Next(200) == 0)
				{
					resultType = ModContent.ItemType<AnglonicMysticBlossom>();
					resultStack = 1;
				}
				else
				{
					resultType = 313;
					resultStack = 1;
				}
			}
			if (num == 1)
			{
				resultType = 315;
				resultStack = 1;
			}
			if (num == 2)
			{
				resultType = 314;
				resultStack = 1;
			}
			if (num == 3)
			{
				resultType = 317;
				resultStack = 1;
			}
			if (num == 4)
			{
				resultType = 318;
				resultStack = 1;
			}
			if (num == 5)
			{
				resultType = 2358;
				resultStack = 1;
			}
			if (num == 6)
			{
				resultType = ModContent.ItemType<Nightshade>();
				resultStack = 1;
			}
			if (num == 7)
			{
				resultType = 307;
				resultStack = 1;
			}
			if (num == 8)
			{
				resultType = 309;
				resultStack = 1;
			}
			if (num == 9)
			{
				resultType = 308;
				resultStack = 1;
			}
			if (num == 10)
			{
				resultType = 311;
				resultStack = 1;
			}
			if (num == 11)
			{
				resultType = 312;
				resultStack = 1;
			}
			if (num == 12)
			{
				resultType = 2357;
				resultStack = 1;
			}
			if (num == 13)
			{
				resultType = ModContent.ItemType<NightshadeSeeds>();
				resultStack = 1;
			}
		}
	}
}
