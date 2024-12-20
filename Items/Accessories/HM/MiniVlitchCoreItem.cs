using System;
using Redemption.Buffs.Minions;
using Redemption.Projectiles.Minions;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class MiniVlitchCoreItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mini-Vlitch Core");
			base.Tooltip.SetDefault("Summons a small Vlitch Core that occasionally shoots lasers at enemies");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(1183);
			base.item.width = 18;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.expert = true;
			base.item.shoot = ModContent.ProjectileType<MiniVlitchCore>();
			base.item.buffType = ModContent.BuffType<VlitchCoreBuff>();
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(base.item.buffType, 3600, true);
			}
		}
	}
}
