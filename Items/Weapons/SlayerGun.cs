using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SlayerGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hyper-Tech Blaster");
			base.Tooltip.SetDefault("'Pewpewpewpewpewpewpew'\nReplaces normal bullets with Luminite Bullets\nRight-clicking fires 5 bullets in an arc");
		}

		public override void SetDefaults()
		{
			base.item.damage = 120;
			base.item.ranged = true;
			base.item.width = 72;
			base.item.height = 28;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item125;
			base.item.autoReuse = true;
			base.item.shoot = 10;
			base.item.shootSpeed = 90f;
			base.item.useAmmo = AmmoID.Bullet;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				base.item.damage = 160;
				base.item.useTime = 50;
				base.item.useAnimation = 50;
				base.item.shootSpeed = 50f;
				base.item.UseSound = SoundID.Item91;
			}
			else
			{
				base.item.damage = 120;
				base.item.useTime = 20;
				base.item.UseSound = SoundID.Item125;
				base.item.useAnimation = 20;
				base.item.shootSpeed = 70f;
			}
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 14)
			{
				type = 638;
			}
			if (player.altFunctionUse == 2)
			{
				float num = 5f;
				float num2 = MathHelper.ToRadians(25f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int num3 = 0;
				while ((float)num3 < num)
				{
					Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.2f;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					num3++;
				}
				return false;
			}
			return true;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CyberPlating", 8);
			modRecipe.AddIngredient(null, "KingCore", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
