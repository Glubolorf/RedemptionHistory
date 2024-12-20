using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class MoonflareStave : DruidStave
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
				MoonflareStave.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Moonflare Stave");
			base.Tooltip.SetDefault("Shoots Moonflare Sparkles");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 10;
			base.item.width = 36;
			base.item.height = 40;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 5, 0);
			base.item.rare = 0;
			base.item.UseSound = SoundID.Item9;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<MoonflareSpark>();
			base.item.shootSpeed = 14f;
			base.item.glowMask = MoonflareStave.customGlowMask;
			this.defaultShoot = ModContent.ProjectileType<MoonflareSpark>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian6Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian6>();
			this.guardianTime = 1500;
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 36.2f;
			this.guardianName = "Moonflare";
			this.guardianType = "Other";
			this.guardianAbility = "Triple-Shot/Glow";
			this.guardianEffects = "Staves that shoot a single projectile will shoot 2 more in an arc, Improved vision";
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(2);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().staveStreamShot && Main.rand.Next(5) == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX * 1.25f, speedY * 1.25f, type, damage, knockBack, player.whoAmI, 0f, 0f);
				Projectile.NewProjectile(position.X, position.Y, speedX * 0.75f, speedY * 0.75f, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "MoonflareFragment", 7);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
