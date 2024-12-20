using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class ForestShacklesDawn : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Shackles");
			base.Tooltip.SetDefault("'Dawn'\n5% increased druidic damage\nStaves cast faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 46;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.defense = 2;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<ForestShacklesDusk>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<ForestShacklesMidnight>())
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
			modRecipe.AddIngredient(216, 2);
			modRecipe.AddIngredient(318, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
		}
	}
}
