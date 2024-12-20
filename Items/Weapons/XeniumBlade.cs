using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XeniumBlade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenium Blade");
			base.Tooltip.SetDefault("Shoots Xeno Blasts");
		}

		public override void SetDefaults()
		{
			base.item.width = 62;
			base.item.height = 68;
			base.item.damage = 255;
			base.item.melee = true;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.useTurn = true;
			base.item.useStyle = 1;
			base.item.rare = 11;
			base.item.knockBack = 7f;
			base.item.UseSound = SoundID.Item60;
			base.item.autoReuse = true;
			base.item.value = Item.buyPrice(0, 40, 0, 0);
			base.item.shoot = base.mod.ProjectileType("XenoShard4");
			base.item.shootSpeed = 24f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, 500, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 22);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
