using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Radiance : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Radiance");
			base.Tooltip.SetDefault("'A glorious explosion...'\nCasts 3 bolts of holy fire that explodes upon death");
			Item.staff[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.damage = 200;
			base.item.magic = true;
			base.item.mana = 20;
			base.item.width = 62;
			base.item.height = 62;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 5;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 25, 50, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item73;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("HolyFirePro2");
			base.item.shootSpeed = 20f;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			float num = 3f;
			float num2 = MathHelper.ToRadians(15f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			int num3 = 0;
			while ((float)num3 < num)
			{
				Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.4f;
				Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				num3++;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1445, 1);
			modRecipe.AddIngredient(null, "PureIronStaff", 1);
			modRecipe.AddIngredient(3467, 15);
			modRecipe.AddIngredient(3458, 20);
			modRecipe.AddIngredient(182, 5);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
