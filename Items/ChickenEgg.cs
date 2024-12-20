using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ChickenEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chicken Egg");
			base.Tooltip.SetDefault("'Which came first...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 20;
			base.item.damage = 3;
			base.item.maxStack = 99;
			base.item.value = 100;
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
			base.item.shoot = base.mod.ProjectileType("ChickenEggPro");
		}
	}
}
