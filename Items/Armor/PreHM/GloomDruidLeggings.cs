using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class GloomDruidLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gloom Leggings");
			base.Tooltip.SetDefault("3% increased druid damage and critical chance \n8% increased movement speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.value = 7000;
			base.item.rare = 2;
			base.item.defense = 4;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.03f;
			druidDamagePlayer.druidCrit += 3;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			player.moveSpeed += 0.08f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "GloomMushroom", 15);
			modRecipe.AddIngredient(154, 25);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
