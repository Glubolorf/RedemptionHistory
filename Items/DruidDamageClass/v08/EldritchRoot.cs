using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class EldritchRoot : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eldritch Root");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Nature can reap, too.'\nIncreased life regen after killing an enemy\nDruidic damage increased by 15% when below 50% health");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 0, 65, 0);
			base.item.rare = 2;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.eldritchRoot = true;
			if ((float)player.statLife <= (float)player.statLifeMax2 * 0.5f)
			{
				druidDamagePlayer.druidDamage += 0.15f;
			}
		}
	}
}
