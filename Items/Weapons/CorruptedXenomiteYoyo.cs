using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedXenomiteYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Yoyo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 110;
			base.item.melee = true;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 7, 50, 0);
			base.item.rare = 10;
			base.item.autoReuse = false;
			base.item.shoot = base.mod.ProjectileType("CorruptedXenomiteYoyoPro");
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 35);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
