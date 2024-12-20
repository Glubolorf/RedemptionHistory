using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class SuspiciousFriedEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Suspicious Fried Egg");
			base.Tooltip.SetDefault("'...I'm not touching that.'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(3, 6));
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item2;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.maxStack = 30;
			base.item.consumable = true;
			base.item.width = 12;
			base.item.height = 38;
			base.item.value = 100;
			base.item.rare = 9;
			base.item.buffType = base.mod.BuffType("DisgustingDebuff");
			base.item.buffTime = 1800;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SuspEgg", 1);
			modRecipe.AddTile(96);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
