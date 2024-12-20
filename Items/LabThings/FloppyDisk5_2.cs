using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk5_2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/d88383:'I've told Eve about possibly giving her a mechanical body,]\n[c/d88383:like how my co-workers used the original source code for creating Adam and the Adam AI.]\n[c/d88383:She seemed very excited about it. That surprised me, as I didn't know she could grow emotions.]\n[c/d88383:This got me thinking about Adams, would they be fine with basically being forced to think one way?]\n[c/d88383:And how would Eve feel about this, if she got to know about this?']");
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
