using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class RingStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dancing Ring Stave");
			base.Tooltip.SetDefault("Shoots hallowed rings that bounce on tiles\nHallowed rings leave a trail of sight, might, or fright soul rings\nSoul rings have a chance to give a player a buff when touched");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 62;
			base.item.width = 50;
			base.item.height = 50;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 10, 50, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item109;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<RingStavePro>();
			base.item.shootSpeed = 10f;
			this.defaultShoot = ModContent.ProjectileType<RingStavePro>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 50.2f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1225, 20);
			modRecipe.AddIngredient(549, 10);
			modRecipe.AddIngredient(548, 10);
			modRecipe.AddIngredient(547, 10);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
