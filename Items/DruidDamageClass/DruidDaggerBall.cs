using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class DruidDaggerBall : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Druid Dagger Cluster");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nAny enemy that gets hit is inflicted with a pollen cloud\nNot consumable\nREPLACED WITH DRUID SHURIKEN");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 20f;
			base.item.crit = 4;
			base.item.damage = 7;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 26;
			base.item.useTime = 26;
			base.item.width = 26;
			base.item.height = 48;
			base.item.maxStack = 999;
			base.item.rare = 1;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 0, 75);
			base.item.shoot = base.mod.ProjectileType<DruidDaggerPro>();
		}
	}
}
