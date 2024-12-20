using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PowerCellWristband : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Power Cell Wristband");
			base.Tooltip.SetDefault("'Fueled with the sun itself'\n10% increased druidic damage\n10% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 12;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(259, 5);
			modRecipe.AddIngredient(1293, 3);
			modRecipe.AddIngredient(ModContent.ItemType<SoulOfBloom>(), 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 10;
		}
	}
}
