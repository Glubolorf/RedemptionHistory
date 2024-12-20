using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class XeniumStave : DruidStave
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/DruidDamageClass/" + base.GetType().Name + "_Glow");
				XeniumStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Xenium Stave");
			base.Tooltip.SetDefault("Shoots homing Xenium Bolts");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 115;
			base.item.height = 78;
			base.item.width = 78;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.mana = 4;
			base.item.crit = 4;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 11;
			base.item.UseSound = SoundID.Item117;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<XeniumStavePro>();
			base.item.shootSpeed = 19f;
			base.item.glowMask = XeniumStave.customGlowMask;
			this.defaultShoot = ModContent.ProjectileType<XeniumStavePro>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 90.2f;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(5f));
			speedX = perturbedSpeed.X;
			speedY = perturbedSpeed.Y;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			return true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "XeniumBar", 17);
			modRecipe.AddTile(null, "XenoTank1");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
