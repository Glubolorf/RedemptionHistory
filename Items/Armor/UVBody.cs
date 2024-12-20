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
	public class UVBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("UV Chestplate");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n11% increased druidic damage\n8% increased druidic critical strike chance\nThrows seedbags faster\nIncreased life regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 28;
			base.item.value = 57000;
			base.item.rare = 8;
			base.item.defense = 12;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.11f;
			druidDamagePlayer.druidCrit += 8;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSeedbags = true;
			player.lifeRegen += 5;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(181, 7);
			modRecipe.AddIngredient(null, "UltraVioletPlating", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
