using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class SuspEgg : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Suspicious Egg");
			base.Tooltip.SetDefault("'I wouldn't break it if I were you... ;)'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(3, 7));
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 20;
			base.item.damage = 3;
			base.item.maxStack = 99;
			base.item.value = 50;
			base.item.rare = 9;
			base.item.useStyle = 1;
			base.item.useAnimation = 10;
			base.item.useTime = 10;
			base.item.UseSound = SoundID.Item1;
			base.item.value = Item.buyPrice(0, 0, 0, 0);
			base.item.consumable = true;
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.thrown = true;
			base.item.shootSpeed = 18f;
			base.item.shoot = ModContent.ProjectileType<SuspEggPro>();
		}
	}
}
