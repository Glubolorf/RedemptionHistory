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
	public class GloomDruidCapplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Capplate");
			base.Tooltip.SetDefault("5% increased druidic damage\n8% increased druidic critical strike chance\nThrows seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.value = 7000;
			base.item.rare = 2;
			base.item.defense = 6;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			druidDamagePlayer.druidCrit += 8;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).fasterSeedbags = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GloomMushroom", 20);
			modRecipe.AddIngredient(154, 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
