using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CorruptedXenomiteScepter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Scepter");
			base.Tooltip.SetDefault("Shoots a volley of red lasers");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 210;
			base.item.magic = true;
			base.item.mana = 8;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useAnimation = 6;
			base.item.useTime = 2;
			base.item.reuseDelay = 9;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 9, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item15;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("CorruptedXenomiteLaser");
			base.item.shootSpeed = 32f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 25);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 5);
			modRecipe.AddIngredient(null, "VlitchBattery", 1);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
