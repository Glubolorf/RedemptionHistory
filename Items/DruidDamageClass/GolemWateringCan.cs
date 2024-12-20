using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class GolemWateringCan : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Golem Watering Can");
			base.Tooltip.SetDefault("Taking damage unleashes a cluster of exploding seeds around you\nHaving a Large Seed Pouch will unleash more seeds");
		}

		public override void SetDefaults()
		{
			base.item.width = 50;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<SkeletonCan>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2766, 20);
			modRecipe.AddIngredient(ModContent.ItemType<SoulOfBloom>(), 20);
			modRecipe.AddIngredient(209, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.25f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).golemWateringCan = true;
		}
	}
}
