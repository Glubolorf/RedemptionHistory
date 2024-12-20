using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class Xenomite : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite");
			base.Tooltip.SetDefault("'Infects living things...'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 7));
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 24;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 7;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XenomiteShard", 4);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}
	}
}
