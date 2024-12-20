using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class ObliterationDrive : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Obliteration Drive");
			base.Tooltip.SetDefault("Dealing damage to enemies has a chance to give the player a stack of Obliteration Motivation\nObliteration Motivation increases damage and defense, at the cost of decreased life regen\nStacks up to 5 times");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 8, 0, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).oblitDrive = true;
		}
	}
}
