using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk6 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/d883c1:'Too costly to finish... Bah!]\n[c/d883c1:This work of genius would be perfect to wipe out a large group of enemies by just pointing its fingers at them! ]\n[c/d883c1:The ever-growing threat of war requires us to have the best weaponry, and THIS is too costly?!']");
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
