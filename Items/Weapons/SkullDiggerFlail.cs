using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class SkullDiggerFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skull Digger's Skull Digger");
			base.Tooltip.SetDefault("'Yes, he did name his weapon after himself...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 56;
			base.item.height = 38;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 3;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 50;
			base.item.useTime = 50;
			base.item.knockBack = 9.5f;
			base.item.damage = 25;
			base.item.scale = 1f;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("SkullDiggerHeadPro");
			base.item.shootSpeed = 32f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.channel = true;
		}
	}
}
