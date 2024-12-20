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
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Dawn'\n5% increased druidic damage\nStaves swing faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 46;
			base.item.value = Item.sellPrice(0, 0, 50, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.defense = 2;
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
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterStaves = true;
		}
	}
}
