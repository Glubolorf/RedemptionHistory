using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class LargeSeedPouch : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Large Seed Pouch");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nHas a chance to throw an extra seed");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 0, 80, 0);
			base.item.rare = 2;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int num = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + num; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("DruidsCharm"))
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
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(309, 2);
			modRecipe.AddIngredient(307, 2);
			modRecipe.AddIngredient(310, 2);
			modRecipe.AddIngredient(312, 2);
			modRecipe.AddIngredient(308, 2);
			modRecipe.AddIngredient(2357, 2);
			modRecipe.AddIngredient(311, 2);
			modRecipe.AddTile(114);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.extraSeed = true;
		}
	}
}
