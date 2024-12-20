using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class IceDruidBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Coldblooded Druid's Chestplate");
			base.Tooltip.SetDefault("3% increased druidic damage\n2% increased druidic critical strike chance\nThrows seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 30;
			base.item.value = 875;
			base.item.rare = 1;
			base.item.defense = 4;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.03f;
			druidDamagePlayer.druidCrit += 2;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterSeedbags = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(664, 35);
			modRecipe.AddIngredient(57, 18);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(664, 35);
			modRecipe2.AddIngredient(1257, 18);
			modRecipe2.AddTile(null, "DruidicAltarTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
