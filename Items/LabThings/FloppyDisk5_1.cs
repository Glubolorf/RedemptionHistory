using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk5_1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/d88383:'Eve has grown much more intelligent over the months. It's like watching your own child grow,]\n[c/d88383:I can't really describe the feeling that much, but I am excited to see where this goes.]\n[c/d88383:The Higher ups have seen my work, and are ready to use the code for something. They didn't tell me that right away...]\n[c/d88383:Now, Eve, how do you feel?']");
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
