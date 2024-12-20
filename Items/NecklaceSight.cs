using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class NecklaceSight : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Necklace of Sight");
			base.Tooltip.SetDefault("Increases sight\nIncreases crit by 4%");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 32;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 7;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.nightVision = true;
			player.magicCrit += 4;
			player.meleeCrit += 4;
			player.rangedCrit += 4;
			player.thrownCrit += 4;
		}
	}
}
