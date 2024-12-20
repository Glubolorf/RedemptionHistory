using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class CircletOfBrambles : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Circlet of Brambles");
			base.Tooltip.SetDefault("Taking damage unleashes thorns in random directions");
		}

		public override void SetDefaults()
		{
			base.item.width = 36;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).thornCirclet = true;
		}
	}
}
