using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class EmptyMutagen : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Empty Mutagen");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 36;
			base.item.maxStack = 1;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 11;
		}
	}
}
