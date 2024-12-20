using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class ScarletChestguard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlet Chestguard");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n10% increased druidic damage\n10% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.value = Item.sellPrice(0, 4, 80, 0);
			base.item.rare = 6;
			base.item.defense = 26;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 10;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ScarletBar", 24);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
