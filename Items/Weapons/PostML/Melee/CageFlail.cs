using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class CageFlail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cage Crusher");
			base.Tooltip.SetDefault("Hitting an enemy once per use will cause echos to appear and fight for you\nThe cage deals increased damage to enemies with less knockback resistance");
		}

		public override void SetDefaults()
		{
			base.item.damage = 450;
			base.item.width = 46;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 14, 0, 0);
			base.item.rare = 11;
			base.item.noMelee = true;
			base.item.useStyle = 5;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.knockBack = 8f;
			base.item.noUseGraphic = true;
			base.item.shoot = ModContent.ProjectileType<CageFlail_Ball>();
			base.item.shootSpeed = 23f;
			base.item.UseSound = SoundID.Item1;
			base.item.melee = true;
			base.item.channel = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}
	}
}
