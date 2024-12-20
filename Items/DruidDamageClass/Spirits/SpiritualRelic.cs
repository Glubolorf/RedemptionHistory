using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.Spirits
{
	public class SpiritualRelic : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spiritual Relic");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n3% increased druidic damage\nIncreases spirits summoned by 1\n[c/bdffff:Spirit Level +2]");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 48;
			base.item.value = Item.sellPrice(0, 0, 60, 0);
			base.item.rare = 2;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CopperCross", 1);
			modRecipe.AddIngredient(225, 10);
			modRecipe.AddIngredient(null, "LostSoul", 2);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "TinCross", 1);
			modRecipe2.AddIngredient(225, 10);
			modRecipe2.AddIngredient(null, "LostSoul", 2);
			modRecipe2.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
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
					if (slot != i && player.armor[i].type == base.mod.ItemType("CopperCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SacredCross"))
					{
						return false;
					}
					if (slot != i && player.armor[i].type == base.mod.ItemType("SpiritualGuide"))
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
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.03f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritLevel += 2;
			redePlayer.spiritExtras++;
		}
	}
}
