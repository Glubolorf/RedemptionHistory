﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SlayerGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				SlayerGun.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
			}
			base.item.glowMask = SlayerGun.customGlowMask;
			base.DisplayName.SetDefault("Hyper-Tech Blaster");
			base.Tooltip.SetDefault("'Pewpewpewpewpewpewpew'\nReplaces normal bullets with Phantasmal Bolts\nRight-clicking fires 5 bolts in an arc");
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
			base.item.glowMask = SlayerGun.customGlowMask;
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
			if (player.altFunctionUse == 2)
			{
				float num = 5f;
				float num2 = MathHelper.ToRadians(25f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int num3 = 0;
				while ((float)num3 < num)
				{
					Vector2 vector = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-num2, num2, (float)num3 / (num - 1f)), default(Vector2)) * 0.2f;
					int num4 = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, 462, damage, knockBack, player.whoAmI, 0f, 0f);
					Main.projectile[num4].ranged = true;
					Main.projectile[num4].hostile = false;
					Main.projectile[num4].friendly = true;
					Main.projectile[num4].tileCollide = true;
					num3++;
				}
			}
			else
			{
				int num5 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 462, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[num5].ranged = true;
				Main.projectile[num5].hostile = false;
				Main.projectile[num5].friendly = true;
				Main.projectile[num5].tileCollide = true;
			}
			return false;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-5f, 0f));
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CyberPlating", 6);
			modRecipe.AddIngredient(null, "Mk2Capacitator", 2);
			modRecipe.AddIngredient(null, "KingCore", 1);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
