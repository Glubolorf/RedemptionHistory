using System;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.v08
{
	public class NebulaStarFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebulous Starburst");
			base.Tooltip.SetDefault("Shoots out Nebula Sparkles");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 36;
			base.item.value = Item.buyPrice(1, 0, 0, 0);
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 40;
			base.item.useTime = 40;
			base.item.knockBack = 8.5f;
			base.item.damage = 500;
			base.item.noUseGraphic = true;
			base.item.shoot = ModContent.ProjectileType<NebulaStarFlailPro>();
			base.item.shootSpeed = 20f;
			base.item.UseSound = SoundID.Item20;
			base.item.melee = true;
			base.item.autoReuse = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}
	}
}
