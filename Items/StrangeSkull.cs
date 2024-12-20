using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class StrangeSkull : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mysterious Skull");
			base.Tooltip.SetDefault("Summons Tiedemies to light your way!");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1183);
			base.item.shoot = base.mod.ProjectileType("TiedemiesPet");
			base.item.buffType = base.mod.BuffType("TiedemiesBuff");
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "OldTophat", 1);
			modRecipe.AddIngredient(1183, 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
