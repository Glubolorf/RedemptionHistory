using System;
using Terraria;
using Terraria.ID;

namespace Redemption.Items.DruidDamageClass
{
	public class SpiritOfLife : DruidDamageItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit of Life");
			base.Tooltip.SetDefault("'Better than nothing...'\n5% increased druidic damage\nGetting attacked unleashes a weak Spirit Chicken to attack foes");
			ItemID.Sets.ItemIconPulse[base.item.type] = true;
			ItemID.Sets.ItemNoGravity[base.item.type] = true;
		}

		public override void SafeSetDefaults()
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
			DruidDamagePlayer.ModPlayer(player).druidDamage += 0.05f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritChicken = true;
		}
	}
}
