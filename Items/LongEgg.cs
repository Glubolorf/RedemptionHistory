using System;
using Redemption.Projectiles.v08;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LongEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("L o n g  Chicken Egg");
			base.Tooltip.SetDefault("'It takes an awfully  l o n g  c h i c k e n  to make a long egg'");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 20;
			base.item.damage = 6;
			base.item.maxStack = 99;
			base.item.value = 500;
			base.item.rare = 0;
			base.item.useStyle = 1;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.thrown = true;
			base.item.shootSpeed = 18f;
			base.item.shoot = ModContent.ProjectileType<LongEggPro>();
		}
	}
}
