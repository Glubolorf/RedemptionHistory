using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class KanitePistol : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Pistol");
		}

		public override void SetDefaults()
		{
			base.item.damage = 9;
			base.item.ranged = true;
			base.item.width = 36;
			base.item.height = 22;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = 1550;
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item11;
			base.item.autoReuse = false;
			base.item.shoot = 10;
			base.item.shootSpeed = 5f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(null, "KaniteBar", 14);
			modRecipe.AddIngredient(9, 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
