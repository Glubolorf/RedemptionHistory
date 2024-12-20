using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class BloodYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ashrune Yoyo");
			base.Tooltip.SetDefault("Shoots Blood Orbs\nThe orbs drain life, but if one kills an enemy, it will multiply into 2 Blood Orbs\nThe orbs created by the multiply won't multiply themselves");
		}

		public override void SetDefaults()
		{
			base.item.damage = 510;
			base.item.melee = true;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<BloodYoyoPro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteBlood", 8);
			modRecipe.AddIngredient(172, 15);
			modRecipe.AddIngredient(3291, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
