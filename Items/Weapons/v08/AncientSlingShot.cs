using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class AncientSlingShot : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Sling Shot");
			base.Tooltip.SetDefault("Hold the use button to charge, unleashing pebbles of mass destruction!");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 22;
			base.item.ranged = true;
			base.item.damage = 4;
			base.item.shoot = ModContent.ProjectileType<AncientSlingShotPro>();
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.channel = true;
			Item.sellPrice(0, 0, 60, 0);
			base.item.noMelee = true;
			base.item.shootSpeed = 6f;
			base.item.noUseGraphic = true;
			base.item.rare = 3;
		}
	}
}
