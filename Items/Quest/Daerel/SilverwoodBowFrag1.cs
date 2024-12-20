using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest.Daerel
{
	public class SilverwoodBowFrag1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Silverwood Bow Fragment");
			base.Tooltip.SetDefault("Looks like a certain Undead can repair this, if you have both pieces");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 22;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}
	}
}
