using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class BowOfBlindS : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bow of the Blind Seamstress");
			base.Tooltip.SetDefault("When shooting, you have a chance to summon a damaging Forest Soul around you");
		}

		public override void SetDefaults()
		{
			base.item.damage = 38;
			base.item.noMelee = true;
			base.item.ranged = true;
			base.item.width = 16;
			base.item.height = 36;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.useStyle = 5;
			base.item.shoot = 10;
			base.item.useAmmo = AmmoID.Arrow;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 1, 35, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item5;
			base.item.shootSpeed = 14f;
			base.item.autoReuse = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.rand.Next(6) == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<ForestSoul>()] <= 5)
			{
				Projectile.NewProjectile(player.position, Vector2.Zero, ModContent.ProjectileType<ForestSoul>(), 45, 0f, player.whoAmI, 0f, 0f);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ForestCore", 8);
			modRecipe.AddIngredient(null, "LostSoul", 2);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
