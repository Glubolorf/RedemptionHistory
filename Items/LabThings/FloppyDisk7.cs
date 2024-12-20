using System;
using Terraria.ModLoader;

namespace Redemption.Items.LabThings
{
	public class FloppyDisk7 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Floppy Disk");
			base.Tooltip.SetDefault("It reads - [c/706c6c:'One of our top scientist, Kari Johansson, has noticed his veins turn green over the past few months.]\n[c/706c6c:He has reported this, which has resulted in him being taken to the laboratory for testing and sampling.]\n[c/706c6c:Currently, what we know of this pathogen is that it's not lethal, and doesn't seem to be able to spread through any means of contact, except direct injection.]\n[c/706c6c:Kari has reported feeling strange lumps around the brighter spots of the glow, but states he isn't in any pain.]\n[c/706c6c:We're looking to further study this new pathogen, and possibly neutralize it, as Kari is essential staff in the Adam AI development.']");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 18;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 10;
		}
	}
}
