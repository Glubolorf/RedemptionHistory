using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ChiliSpray : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chilli Powder Spray");
			base.Tooltip.SetDefault("Burn your enemies with spicy spice");
		}

		public override void SetDefaults()
		{
			base.item.damage = 43;
			base.item.magic = true;
			base.item.mana = 3;
			base.item.width = 16;
			base.item.height = 30;
			base.item.useAnimation = 4;
			base.item.useTime = 4;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 1f;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = SoundID.Item13;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<ChiliPowder>();
			base.item.shootSpeed = 18f;
		}
	}
}
