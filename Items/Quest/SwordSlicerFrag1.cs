using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class SwordSlicerFrag1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sword Slicer Fragment");
			base.Tooltip.SetDefault("Looks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 26;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}
	}
}
