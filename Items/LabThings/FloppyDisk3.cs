using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk3 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/7de4e8:'One of our top people in the Robotics and AI, Kari, has created an interesting piece of AI which he has named Eve AI.]\n[c/7de4e8:With enough tampering, Kari could force it to grow into something we want, and we could use an intelligent, human-like intelligence for a few robot prototypes.]\n[c/7de4e8:We've decided to name the first one Adam, because Adam and Eve, you know?]");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 1;
		}
	}
}
