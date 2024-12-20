using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
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
			base.Tooltip.SetDefault("4% increased druid damage and critical chance \nUse staves and seedbags faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.value = 7000;
			base.item.rare = 2;
			base.item.defense = 5;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.04f;
			druidDamagePlayer.druidCrit += 4;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSeedbags = true;
			redePlayer.staveSpeed += 0.05f;
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
