using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class PlanterasStave2 : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plantera's Stave");
			base.Tooltip.SetDefault("Shoots bouncy thorn balls.");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 72;
			base.item.mana = 8;
			base.item.width = 74;
			base.item.height = 74;
			base.item.useTime = 35;
			base.item.useAnimation = 35;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item5;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("MiniThornBall1");
			base.item.shootSpeed = 12f;
			this.defaultShoot = base.mod.ProjectileType("MiniThornBall1");
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 74.2f;
		}
	}
}
