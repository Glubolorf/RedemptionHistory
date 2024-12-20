using System;
using Terraria;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class EldritchRoot : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eldritch Root");
			base.Tooltip.SetDefault("'Nature can reap, too.'\nIncreased life regen after killing an enemy\nDruidic damage increased by 15% when below 50% health");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 0, 65, 0);
			base.item.rare = 2;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer modPlayer = DruidDamagePlayer.ModPlayer(player);
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).eldritchRoot = true;
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.5f)
			{
				modPlayer.druidDamage += 0.15f;
			}
		}
	}
}
