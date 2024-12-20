using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class PlanterasStave1 : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Plantera's Stave");
			base.Tooltip.SetDefault("Shoots pink petals.");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 44;
			base.item.mana = 5;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 11;
			base.item.useAnimation = 16;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 4, 50, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item17;
			base.item.autoReuse = true;
			base.item.noMelee = true;
			base.item.shoot = base.mod.ProjectileType("MiniPlanteraSeed1");
			base.item.shootSpeed = 17f;
			this.defaultShoot = base.mod.ProjectileType("MiniPlanteraSeed1");
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 64.2f;
		}
	}
}
