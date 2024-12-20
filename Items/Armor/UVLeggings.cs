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
	public class UVLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("UV Leggings");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n11% increased druidic damage\nSpirits shoot faster\n40+ max life\nIncreased movement speed at daytime\nIncreased jump height at nighttime");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.value = 57000;
			base.item.rare = 8;
			base.item.defense = 9;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.11f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.fasterSpirits = true;
			player.statLifeMax2 += 40;
			if (Main.dayTime)
			{
				player.moveSpeed += 80f;
				return;
			}
			player.jumpBoost = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(181, 5);
			modRecipe.AddIngredient(2860, 25);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
