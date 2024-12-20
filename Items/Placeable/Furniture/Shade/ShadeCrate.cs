using System;
using Redemption.Items.Materials.PostML;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Redemption.Tiles.Furniture.Shade;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Placeable.Furniture.Shade
{
	public class ShadeCrate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shade Crate");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 32;
			base.item.useStyle = 1;
			base.item.createTile = ModContent.TileType<ShadeCrateTile>();
			base.item.maxStack = 999;
			base.item.autoReuse = true;
			base.item.useAnimation = 15;
			base.item.useTime = 10;
			base.item.consumable = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			int num = Main.rand.Next(2);
			if (num != 0)
			{
				if (num == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<VesselPickaxeAxe>(), 1);
				}
			}
			else
			{
				player.QuickSpawnItem(ModContent.ItemType<VesselHammer>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<Shadesoul>(), Main.rand.Next(1, 3));
			num = Main.rand.Next(2);
			if (num != 0)
			{
				if (num == 1)
				{
					player.QuickSpawnItem(ModContent.ItemType<VesselFrag>(), Main.rand.Next(8, 12));
				}
			}
			else
			{
				player.QuickSpawnItem(ModContent.ItemType<SmallShadesoul>(), Main.rand.Next(8, 12));
			}
			if (Main.rand.Next(2) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<ShadeKnife>(), Main.rand.Next(100, 600));
			}
			if (Main.rand.Next(6) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<BlackenedHeart>(), 1);
			}
		}
	}
}
