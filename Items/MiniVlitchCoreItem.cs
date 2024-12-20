using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class MiniVlitchCoreItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mini-Vlitch Core");
			base.Tooltip.SetDefault("Summons a small Vlitch Core that occasionally shoots lasers at enemies\n80% damage reduction while the core is alive\n60% reduced damage while the core is alive");
		}

		public override void SetDefaults()
		{
			base.item.CloneDefaults(669);
			base.item.width = 18;
			base.item.height = 34;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.expert = true;
			base.item.shoot = base.mod.ProjectileType("MiniVlitchCore");
			base.item.buffType = base.mod.BuffType("VlitchCoreBuff");
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
