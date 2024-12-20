using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class CrystalDaggerBall : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Dagger Cluster");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\nDaggers split in two after a small distance\nNot consumable\nREPLACED WITH CRYSTAL SHURIKEN");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 19f;
			base.item.crit = 4;
			base.item.damage = 38;
			base.item.knockBack = 5f;
			base.item.useStyle = 1;
			base.item.useAnimation = 23;
			base.item.useTime = 23;
			base.item.width = 14;
			base.item.height = 56;
			base.item.maxStack = 999;
			base.item.rare = 4;
			base.item.consumable = false;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = true;
			base.item.UseSound = SoundID.Item9;
			base.item.value = Item.sellPrice(0, 0, 1, 5);
			base.item.shoot = base.mod.ProjectileType<CrystalDaggerPro>();
		}
	}
}
