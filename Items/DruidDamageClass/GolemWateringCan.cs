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
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'The Can itself is the sun, it still needs the water to make the seeds grow.'\nTaking damage unleashes a cluster of exploding seeds around you\nHaving a Large Seed Pouch will unleash more seeds\n25% increased druidic damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 50;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2766, 20);
			modRecipe.AddIngredient(base.mod.ItemType("SoulOfBloom"), 20);
			modRecipe.AddIngredient(209, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.25f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.golemWateringCan = true;
		}
	}
}
