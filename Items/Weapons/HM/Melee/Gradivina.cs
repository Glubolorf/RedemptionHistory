using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Melee
{
	public class Gradivina : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gradivina");
			base.Tooltip.SetDefault("'Devotion to one's motherland is the highest calling... And the loneliest one.'\nHitting an enemy with Left-click will heal the player\nRight-clicking throws an image of the lance, which pierces through enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 45;
			base.item.useStyle = 5;
			base.item.useAnimation = 11;
			base.item.useTime = 11;
			base.item.shootSpeed = 6.5f;
			base.item.knockBack = 7f;
			base.item.width = 62;
			base.item.height = 62;
			base.item.crit = 10;
			base.item.scale = 1f;
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.shoot = ModContent.ProjectileType<GradivinaPro>();
			base.item.value = Item.sellPrice(0, 7, 0, 0);
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.melee = true;
			base.item.autoReuse = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX * 2f, speedY * 2f, ModContent.ProjectileType<GradivinaPro2>(), damage / 3, knockBack, player.whoAmI, 0f, 0f);
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(550, 1);
			modRecipe.AddIngredient(520, 10);
			modRecipe.AddIngredient(521, 10);
			modRecipe.AddIngredient(178, 5);
			modRecipe.AddIngredient(19, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(550, 1);
			modRecipe2.AddIngredient(520, 10);
			modRecipe2.AddIngredient(521, 10);
			modRecipe2.AddIngredient(178, 5);
			modRecipe2.AddIngredient(706, 10);
			modRecipe2.AddTile(134);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.shoot = ModContent.ProjectileType<GradivinaPro3>();
			}
			else
			{
				base.item.shoot = ModContent.ProjectileType<GradivinaPro>();
			}
			return player.ownedProjectileCounts[base.item.shoot] < 1;
		}
	}
}
