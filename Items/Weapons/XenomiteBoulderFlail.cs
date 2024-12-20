using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomiteBoulderFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Boulder");
		}

		public override void SetDefaults()
		{
			base.item.width = 46;
			base.item.height = 38;
			base.item.value = Item.sellPrice(0, 2, 0, 0);
			base.item.rare = 7;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 50;
			base.item.useTime = 50;
			base.item.knockBack = 7.5f;
			base.item.damage = 85;
			base.item.scale = 1f;
			base.item.noUseGraphic = true;
			base.item.shoot = base.mod.ProjectileType("XenomiteFlailHead");
			base.item.shootSpeed = 28.5f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.channel = true;
		}
	}
}
