using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class LotusHorseshoe3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lotus Horseshoe");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n10% increased druidic damage\nNegates fall damage\nGrants immunity to fire blocks\nForms an spirit skull to aid you upon being attacked");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 2, 25, 0);
			base.item.rare = 4;
			base.item.accessory = true;
			base.item.defense = 2;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(396, 1);
			modRecipe.AddIngredient(null, "ForestShacklesMidnight", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritSkull1 = true;
			player.noFallDmg = true;
			player.buffImmune[67] = true;
		}
	}
}
