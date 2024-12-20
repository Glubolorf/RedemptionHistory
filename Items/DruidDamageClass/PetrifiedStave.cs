using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PetrifiedStave : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Stave");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nShoots a Xeno Bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 37;
			base.item.width = 58;
			base.item.height = 62;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 8, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("XenoBolt");
			base.item.shootSpeed = 9f;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).burnStaves)
			{
				target.AddBuff(24, 180, false);
			}
		}

		public override float UseTimeMultiplier(Player player)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).fasterStaves)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.45f;
				}
				return 1.15f;
			}
			else
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).rapidStave)
				{
					return 1.35f;
				}
				return 1f;
			}
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveStreamShot && Main.rand.Next(5) == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX * 1.25f, speedY * 1.25f, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(position.X, position.Y, speedX * 0.75f, speedY * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveTripleShot)
			{
				float num = 3f;
				float num2 = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int num3 = 0;
				while ((float)num3 < num)
				{
					Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					num3++;
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveScatterShot && Main.rand.Next(5) == 0)
			{
				int num4 = 2 + Main.rand.Next(2);
				for (int i = 0; i < num4; i++)
				{
					Vector2 vector2 = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
					float num5 = 1f - Utils.NextFloat(Main.rand) * 0.3f;
					vector2 *= num5;
					Projectile.NewProjectile(position.X, position.Y, vector2.X, vector2.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
				}
				return false;
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).staveQuadShot)
			{
				float num6 = 5f;
				float num7 = MathHelper.ToRadians(15f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int num8 = 0;
				while ((float)num8 < num6)
				{
					Vector2 vector3 = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num7, num7, (float)num8 / (num6 - 1f)), default(Vector2)) * 0.8f;
					Projectile.NewProjectile(position.X, position.Y, vector3.X, vector3.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
					num8++;
				}
				return false;
			}
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 8);
			modRecipe.AddIngredient(27, 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
