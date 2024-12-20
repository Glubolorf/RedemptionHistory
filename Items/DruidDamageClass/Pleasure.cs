using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class Pleasure : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Pleasure (O-02-98)-W");
			base.Tooltip.SetDefault("'If you look for a pleasure that can not be tolerated, the end reaches the loss of self.'\nAttacks inflict Enjoyment, decreasing life of those hit until death");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 48;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 32;
			base.item.useAnimation = 32;
			base.item.crit = 4;
			base.item.knockBack = 1f;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<PleasurePro1>();
			base.item.shootSpeed = 15f;
			this.defaultShoot = ModContent.ProjectileType<PleasurePro1>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 60.2f;
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = -16; i <= 16; i++)
			{
				Projectile.NewProjectile(position, 4f * Utils.RotatedBy(Vector2.UnitX, 0.19634954084936207 * (double)i, default(Vector2)), type, damage, knockBack, Main.myPlayer, 0f, 0f);
			}
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(210, 20);
			modRecipe.AddIngredient(null, "LunarCrescentStave", 1);
			modRecipe.AddIngredient(292, 5);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
