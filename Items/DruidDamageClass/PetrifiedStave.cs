using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class PetrifiedStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Stave");
			base.Tooltip.SetDefault("Shoots a Xeno Bolt");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 37;
			base.item.width = 58;
			base.item.height = 62;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 8, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<XenoBolt>();
			base.item.shootSpeed = 9f;
			this.defaultShoot = ModContent.ProjectileType<XenoBolt>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 58.2f;
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
