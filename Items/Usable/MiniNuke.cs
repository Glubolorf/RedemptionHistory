using System;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class MiniNuke : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mini-Nuke");
			base.Tooltip.SetDefault("'I don't want to set the world on fire'\nBlows up tiles! A lot of tiles!");
		}

		public override void SetDefaults()
		{
			base.item.damage = 0;
			base.item.width = 22;
			base.item.height = 34;
			base.item.maxStack = 99;
			base.item.consumable = true;
			base.item.useStyle = 1;
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item1;
			base.item.useAnimation = 30;
			base.item.useTime = 30;
			base.item.value = Item.buyPrice(0, 50, 0, 0);
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.shoot = ModContent.ProjectileType<MiniNukePro>();
			base.item.shootSpeed = 5f;
		}
	}
}
