using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Magic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Magic
{
	public class LifeFruitStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shining Heart Scepter");
			base.Tooltip.SetDefault("Shoots 3 tiny life fruits in an arc, inflicting Ichor\nWhile holding this, life regen is greatly increased");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 68;
			base.item.magic = true;
			base.item.mana = 6;
			base.item.width = 78;
			base.item.height = 78;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 4f;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<TinyLifeFruitPro>();
			base.item.shootSpeed = 29f;
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(58, 4, true);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float numberProjectiles = 3f;
			float rotation = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int i = 0;
			while ((float)i < numberProjectiles)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				i++;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LifeCrystalStaff", 1);
			modRecipe.AddIngredient(1291, 1);
			modRecipe.AddIngredient(1006, 12);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LifeCrystalStaff2", 1);
			modRecipe2.AddIngredient(1291, 1);
			modRecipe2.AddIngredient(1006, 12);
			modRecipe2.AddTile(134);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
