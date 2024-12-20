using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Projectiles.Druid.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.HM.Druid.Staves
{
	public class Belrose1 : DruidStave
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
				Belrose1.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("The Belrose");
			base.Tooltip.SetDefault("'Shifting from bones to dust'\nThrows 3 Belroses out like boomerangs");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 75;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 33;
			base.item.useAnimation = 33;
			base.item.useStyle = 1;
			base.item.crit = 4;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 3, 50, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<BelrosePro1>();
			base.item.shootSpeed = 22f;
			base.item.glowMask = Belrose1.customGlowMask;
			this.defaultShoot = ModContent.ProjectileType<BelrosePro1>();
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 50.2f;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(15f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			target.AddBuff(39, 300, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(522, 25);
			modRecipe.AddIngredient(208, 1);
			modRecipe.AddIngredient(1006, 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public static short customGlowMask;
	}
}
