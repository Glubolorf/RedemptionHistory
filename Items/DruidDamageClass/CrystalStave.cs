using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CrystalStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Stave");
			base.Tooltip.SetDefault("Shoots clusters of crystal shards");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 30;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 29;
			base.item.useAnimation = 29;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 10, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item9;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = 94;
			base.item.shootSpeed = 19f;
			this.defaultShoot = 94;
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 46.2f;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = 3 + Main.rand.Next(3);
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedByRandom(new Vector2(speedX, speedY), (double)MathHelper.ToRadians(10f));
				float scale = 1f - Utils.NextFloat(Main.rand) * 0.3f;
				perturbedSpeed *= scale;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(20) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 69, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(20) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 68, 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.rand.Next(20) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 70, 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(520, 4);
			modRecipe.AddIngredient(521, 4);
			modRecipe.AddIngredient(502, 25);
			modRecipe.AddIngredient(null, "ForestCore", 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
