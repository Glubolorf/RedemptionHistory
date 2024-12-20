using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Parthius : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Parthius");
			base.Tooltip.SetDefault("'True aim comes from a strong heart that doesn't waver in the face of obstacles.'\nShoots two arrows, a normal arrow and a Parthian Arrow\nParthian Arrows explode into shards upon impact and inflicts Ichor and On Fire!");
		}

		public override void SetDefaults()
		{
			base.item.damage = 20;
			base.item.ranged = true;
			base.item.width = 26;
			base.item.height = 60;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 2.5f;
			base.item.value = Item.sellPrice(0, 9, 0, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 14f;
			base.item.useAmmo = AmmoID.Arrow;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-2f, 0f));
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(2f));
			speedX = vector.X;
			speedY = vector.Y;
			Projectile.NewProjectile(position.X, position.Y, speedX * 1.2f, speedY * 1.2f, base.mod.ProjectileType("GoldArrow"), 25, 5f, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 12);
			modRecipe.AddIngredient(520, 10);
			modRecipe.AddIngredient(521, 10);
			modRecipe.AddIngredient(178, 5);
			modRecipe.AddIngredient(19, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
