using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/87d883:'The Xenomite particles are very fine but complex, with the ability to quickly get rid of our most powerful antibiotics and other medicine.]\n[c/87d883:While we have developed a 'cure' for it, the Xenomite replicates way too fast for it to be effective.]\n[c/87d883:Currently we'd need a constant drip of the cure for it to just slow down it,]\n[c/87d883:as we haven't had time to make it potent enough to destroy all Xenomite particles.']");
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
