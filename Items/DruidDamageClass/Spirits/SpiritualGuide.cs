using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class SpiritualGuide : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spiritual Guide");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n'You are filled with balance...'\nIncreases spirits summoned by 3\nSpirits home in on enemies\n[c/bdffff:Spirit Level +4]");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 56;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.rare = 7;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SacredCross", 1);
			modRecipe.AddIngredient(520, 15);
			modRecipe.AddIngredient(521, 15);
			modRecipe.AddIngredient(2766, 10);
			modRecipe.AddRecipeGroup("Redemption:Plant", 8);
			modRecipe.AddIngredient(null, "PowerCellWristband", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == base.mod.ItemType("TinCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SpiritualRelic"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SacredCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("CopperCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("LastBurden"))
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player);
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritLevel += 4;
			redePlayer.spiritExtras += 3;
			redePlayer.spiritHoming = true;
		}
	}
}
