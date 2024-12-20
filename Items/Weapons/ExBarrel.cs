using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ExBarrel : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Explosive Barrel");
			base.Tooltip.SetDefault("'Kaboom!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.damage = 75;
			base.item.maxStack = 999;
			base.item.value = 100;
			base.item.rare = 4;
			base.item.useStyle = 1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.UseSound = SoundID.Item7;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.thrown = true;
			base.item.shootSpeed = 13f;
			base.item.shoot = base.mod.ProjectileType("ExBarrelPro");
			base.item.ammo = base.item.type;
		}
	}
}
