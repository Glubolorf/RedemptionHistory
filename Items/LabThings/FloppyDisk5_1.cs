using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk5_1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("'A very old floppy disk. A T-Bot might be able to decode the data...'\n(2/4)");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 6;
		}
	}
}
