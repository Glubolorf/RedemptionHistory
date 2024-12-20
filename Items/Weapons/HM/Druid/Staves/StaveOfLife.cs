using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Staves
{
	public class StaveOfLife : DruidStave
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Weapons/HM/Druid/Staves/" + base.GetType().Name + "_Glow");
				StaveOfLife.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Stave of Life");
			base.Tooltip.SetDefault("Rapidly shoots barrages of ancient herbs");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 44;
			base.item.width = 50;
			base.item.height = 54;
			base.item.useTime = 6;
			base.item.useAnimation = 6;
			base.item.crit = 4;
			base.item.knockBack = 2f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item17;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<HerbOfLifePro>();
			base.item.shootSpeed = 18f;
			base.item.glowMask = StaveOfLife.customGlowMask;
			this.defaultShoot = ModContent.ProjectileType<HerbOfLifePro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian18Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian18>();
			this.guardianTime = 600;
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 50.2f;
			this.guardianName = "Tree of Creation";
			this.guardianType = "Mystic";
			this.guardianAbility = "Scatter-Shot/Quad-Shot/Creation's Embrace";
			this.guardianEffects = "Staves that shoot a single projectile will instead shoot a cluster,\nStaves that shoot a single projectile will shoot 4 more in an arc, Staves cast a lot faster,\nDefence Enhancement+/Mobility Enhancement+/Life & Mana Enhancement++";
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 2 + Main.rand.Next(4);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(30f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.5f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CreationFragment", 18);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
