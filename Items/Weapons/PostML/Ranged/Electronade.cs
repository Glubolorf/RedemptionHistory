using System;
using Redemption.Projectiles.Ranged;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class Electronade : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Electronade");
			base.Tooltip.SetDefault("Throw an energy-filled grenade");
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 18;
			base.item.damage = 250;
			base.item.maxStack = 999;
			base.item.value = 150;
			base.item.rare = 11;
			base.item.useStyle = 1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.UseSound = SoundID.Item7;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.thrown = true;
			base.item.autoReuse = true;
			base.item.shootSpeed = 12f;
			base.item.shoot = ModContent.ProjectileType<ElectronadeFPro>();
		}
	}
}
