using System;
using Redemption.Projectiles.Druid;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid
{
	public class AcornBomb : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Acorn Bomb");
		}

		public override void SafeSetDefaults()
		{
			base.item.shootSpeed = 9f;
			base.item.crit = 4;
			base.item.damage = 30;
			base.item.knockBack = 8f;
			base.item.useStyle = 1;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.width = 20;
			base.item.height = 16;
			base.item.maxStack = 99;
			base.item.rare = 1;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.sellPrice(0, 0, 0, 10);
			base.item.shoot = ModContent.ProjectileType<AcornBombPro>();
		}
	}
}
