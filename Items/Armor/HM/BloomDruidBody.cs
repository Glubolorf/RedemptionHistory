using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class BloomDruidBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blossom Druid's Leaf Chestplate");
			base.Tooltip.SetDefault("15% increased druidic damage\n15% increased druidic critical strike chance\nStaves cast faster\nThrows seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 38;
			base.item.value = 80000;
			base.item.rare = 7;
			base.item.defense = 19;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.15f;
			druidDamagePlayer.druidCrit += 15;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.staveSpeed += 0.05f;
			redePlayer.fasterSeedbags = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SoulOfBloom", 25);
			modRecipe.AddIngredient(null, "ForestCore", 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
