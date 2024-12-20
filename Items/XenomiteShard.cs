using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class XenomiteShard : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Shard");
			base.Tooltip.SetDefault("'Holding this may infect you...'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 5));
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 14;
			base.item.maxStack = 999;
			base.item.value = Item.sellPrice(0, 0, 0, 75);
			base.item.rare = 2;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(10, 20), true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 1);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 4);
			modRecipe.AddRecipe();
		}
	}
}
