using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class Soul2Leggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Wandering Soul's Leggings");
			base.Tooltip.SetDefault("[c/bdffff:---Druid Class---]\n2% increased druidic damage\n3% damage reduction\nDecreased falling speed\nIncreased movement speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = 500;
			base.item.rare = 4;
			base.item.defense = 7;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.02f;
			player.endurance += 0.03f;
			player.slowFall = true;
			player.moveSpeed += 0.5f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LostSoul", 5);
			modRecipe.AddIngredient(501, 15);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
