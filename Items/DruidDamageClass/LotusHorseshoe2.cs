using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class LotusHorseshoe2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lotus Horseshoe");
			base.Tooltip.SetDefault("10% increased druidic damage\nNegates fall damage\nGrants immunity to fire blocks\nHas a chance to throw an extra seed");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 2, 25, 0);
			base.item.rare = 4;
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
					if (slot != i && player.armor[i].type == ModContent.ItemType<LotusHorseshoe1>())
					{
						return false;
					}
					if (slot != i && player.armor[i].type == ModContent.ItemType<LotusHorseshoe3>())
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
			modRecipe.AddIngredient(396, 1);
			modRecipe.AddIngredient(null, "ForestShacklesDusk", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.1f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).extraSeed = true;
			player.noFallDmg = true;
			player.buffImmune[67] = true;
		}
	}
}
