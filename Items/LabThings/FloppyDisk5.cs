using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk5 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/d88383:'(1/4) ... At last, my prototype for a constantly evolving AI is finally done!]\n[c/d88383:Finally, after years and years of studying computer coding and... stuff,]\n[c/d88383:I have created possibly the next huge leap in Artificial Intelligence!]\n[c/d88383:Now, to give it a name... How about, Eve?']");
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
