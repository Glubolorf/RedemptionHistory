using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class SpiritOfLife : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit of Life");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'Better than nothing...'\n10% increased druidic damage\nGetting attacked unleashes a weak Spirit Chicken to attack foes");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SetDefaults()
		{
			base.item.width = 46;
			base.item.height = 42;
			base.item.value = Item.sellPrice(0, 1, 0, 0);
			base.item.rare = 0;
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritChicken = true;
		}
	}
}
