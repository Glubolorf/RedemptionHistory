using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PouchBelt : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pouch Belt");
			base.Tooltip.SetDefault("8% increased druidic damage\n2% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 0, 15, 0);
			base.item.rare = 1;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(259, 4);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.08f;
			druidDamagePlayer.druidCrit += 2;
		}
	}
}
