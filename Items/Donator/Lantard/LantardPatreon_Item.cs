using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Donator.Lantard
{
	public class LantardPatreon_Item : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Fluffy Scarf");
			base.Tooltip.SetDefault("Summons a chibi Ralsei");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.width = 30;
			base.item.height = 24;
			base.item.rare = -12;
			base.item.shoot = ModContent.ProjectileType<LantardPatreon_Pet>();
			base.item.buffType = ModContent.BuffType<LantardPetBuff>();
			base.item.GetGlobalItem<RedeItem>().redeRarity = 9;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(225, 15);
			modRecipe.AddIngredient(1018, 1);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}
	}
}
