using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Incandescence : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Incandescence");
			base.Tooltip.SetDefault("'A glorious flame...'\nCasts a pillar of holy flames upon hitting an enemy\nRight-clicking casts holy flames at cursor position");
		}

		public override void SetDefaults()
		{
			base.item.damage = 100;
			base.item.melee = true;
			base.item.width = 100;
			base.item.height = 100;
			base.item.useTime = 15;
			base.item.useAnimation = 15;
			base.item.useStyle = 1;
			base.item.knockBack = 6.5f;
			base.item.value = Item.sellPrice(0, 25, 50, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item71;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = 0;
			base.item.shootSpeed = 0f;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.shoot = base.mod.ProjectileType("HolyFirePro1");
				base.item.autoReuse = false;
			}
			else
			{
				base.item.shoot = 0;
				base.item.autoReuse = true;
			}
			return base.CanUseItem(player);
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (player.altFunctionUse <= 1)
			{
				Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, -6f, base.mod.ProjectileType("HolyFirePro1"), damage * 3, knockBack, Main.myPlayer, 0f, 0f);
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				position = Main.MouseWorld;
				int num = 20;
				for (int i = 0; i < num; i++)
				{
					int num2 = Projectile.NewProjectile(position.X, position.Y, 0f, 0f, base.mod.ProjectileType("HolyFirePro1"), damage * 3, knockBack, Main.myPlayer, 0f, 0f);
					Main.projectile[num2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(6f, 0f), (float)i / (float)num * 6.28f);
				}
				return false;
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Bindeklinge", 1);
			modRecipe.AddIngredient(null, "ForgottenSword", 1);
			modRecipe.AddIngredient(3467, 15);
			modRecipe.AddIngredient(3458, 20);
			modRecipe.AddIngredient(182, 5);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
