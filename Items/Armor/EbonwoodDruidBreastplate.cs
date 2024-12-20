using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class EbonwoodDruidBreastplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ebondruid's Chestplate");
			base.Tooltip.SetDefault("4% increased druidic damage\n2% increased druidic critical strike chance\nStaves cast faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 28;
			base.item.value = 1075;
			base.item.rare = 1;
			base.item.defense = 5;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.04f;
			druidDamagePlayer.druidCrit += 2;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(619, 35);
			modRecipe.AddIngredient(86, 18);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
