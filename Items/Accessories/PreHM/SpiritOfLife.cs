using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PreHM
{
	public class SpiritOfLife : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Spirit of Life");
			base.Tooltip.SetDefault("'Better than nothing...'\n+1 druidic damage\nGetting attacked unleashes a weak Spirit Chicken to attack foes");
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
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			DruidDamagePlayer.ModPlayer(player).druidDamageFlat += 1f;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).spiritChicken = true;
		}
	}
}
