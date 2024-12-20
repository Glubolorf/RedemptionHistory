using System;
using Redemption.Items.Placeable.Plants;
using Redemption.Tiles.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Tiles
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
			switch (Main.rand.Next(14))
			{
			case 0:
				if (Utils.NextBool(Main.rand, 200))
				{
					resultType = ModContent.ItemType<AnglonicMysticBlossom>();
					resultStack = 1;
					return;
				}
				resultType = 313;
				resultStack = 1;
				return;
			case 1:
				break;
			case 2:
				resultType = 314;
				resultStack = 1;
				return;
			case 3:
				resultType = 317;
				resultStack = 1;
				return;
			case 4:
				resultType = 318;
				resultStack = 1;
				return;
			case 5:
				resultType = 2358;
				resultStack = 1;
				return;
			case 6:
				if (RedeWorld.downedThorn)
				{
					resultType = ModContent.ItemType<Nightshade>();
					resultStack = 1;
					return;
				}
				break;
			case 7:
				goto IL_BC;
			case 8:
				resultType = 309;
				resultStack = 1;
				return;
			case 9:
				resultType = 308;
				resultStack = 1;
				return;
			case 10:
				resultType = 311;
				resultStack = 1;
				return;
			case 11:
				resultType = 312;
				resultStack = 1;
				return;
			case 12:
				resultType = 2357;
				resultStack = 1;
				return;
			case 13:
				if (RedeWorld.downedThorn)
				{
					resultType = ModContent.ItemType<NightshadeSeeds>();
					resultStack = 1;
					return;
				}
				goto IL_BC;
			default:
				return;
			}
			resultType = 315;
			resultStack = 1;
			return;
			IL_BC:
			resultType = 307;
			resultStack = 1;
		}
	}
}
