using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/b883d8:'Last thing I was expecting to be recruited for was military research, but sure, I'm fine with helping the army out with their stuff.]\n[c/b883d8:The payment is nice, I can do anything on the side if I have time. But it does seem a little... Useless or weird,]\n[c/b883d8:to invest time in high-tech weaponry, as we've been in the longest time of peace, atleast for the first world.]\n[c/b883d8:Hopefully we never have to use any of this against anyone.']");
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
