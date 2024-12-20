using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	public class NoveltyMop : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Novelty Mop");
			base.Tooltip.SetDefault("'Not as lethal as Janitor's'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.value = Item.buyPrice(0, 5, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.rare = 1;
			base.item.autoReuse = true;
			base.item.channel = true;
			base.item.shoot = ModContent.ProjectileType<NoveltyMop_Proj>();
			base.item.shootSpeed = 8f;
		}
	}
}
