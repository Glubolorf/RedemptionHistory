using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class DruidLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Druid's Leggings");
			base.Tooltip.SetDefault("6% increased druidic damage\nThrows seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.value = 7000;
			base.item.rare = 4;
			base.item.defense = 8;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.06f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterSeedbags = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(225, 15);
			modRecipe.AddIngredient(null, "ForestCore", 6);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
