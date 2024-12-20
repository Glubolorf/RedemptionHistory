using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Cooldowns;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class SoulScroll : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Soul Scroll");
			base.Tooltip.SetDefault("'It's blank...'\nConverts all soulless enemies on screen into a normal lost soul\n1 minute cooldown");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item1;
			base.item.useStyle = 4;
			base.item.useTurn = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 80;
			base.item.useTime = 80;
			base.item.width = 38;
			base.item.height = 22;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
			base.item.shoot = ModContent.ProjectileType<SoulScroll_Pro>();
			base.item.shootSpeed = 0f;
		}

		public override bool CanUseItem(Player player)
		{
			return !player.HasBuff(ModContent.BuffType<SoulScrollCooldown>());
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(ModContent.BuffType<SoulScrollCooldown>(), 3600, true);
			return true;
		}
	}
}
