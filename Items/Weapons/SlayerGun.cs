using System;
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
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/" + base.GetType().Name + "_Glow");
				SlayerGun.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
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
			base.item.useTime = 17;
			base.item.useAnimation = 17;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 3f;
			base.item.value = Item.sellPrice(0, 15, 0, 0);
			base.item.rare = 9;
			base.item.UseSound = SoundID.Item91;
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
			int altFunctionUse = player.altFunctionUse;
			return base.CanUseItem(player);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.altFunctionUse == 2)
			{
				player.itemAnimationMax = base.item.useTime * 3;
				player.itemTime = base.item.useTime * 3;
				player.itemAnimation = base.item.useTime * 3;
				speedX *= 0.715f;
				speedY *= 0.715f;
				damage = (int)((float)damage * 1.333f);
				float numberProjectiles = 5f;
				float rotation = MathHelper.ToRadians(25f);
				position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
				int i = 0;
				while ((float)i < numberProjectiles)
				{
					Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(speedX, speedY), (double)MathHelper.Lerp(-rotation, rotation, (float)i / (numberProjectiles - 1f)), default(Vector2)) * 0.2f;
					int projectile = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, 462, damage, knockBack, player.whoAmI, 0f, 0f);
					Main.projectile[projectile].ranged = true;
					Main.projectile[projectile].hostile = false;
					Main.projectile[projectile].friendly = true;
					Main.projectile[projectile].tileCollide = true;
					i++;
				}
			}
			else
			{
				int projectile2 = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 462, damage, knockBack, player.whoAmI, 0f, 0f);
				Main.projectile[projectile2].ranged = true;
				Main.projectile[projectile2].hostile = false;
				Main.projectile[projectile2].friendly = true;
				Main.projectile[projectile2].tileCollide = true;
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
