using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class XenChomper : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xen Chomper");
		}

		public override void SetDefaults()
		{
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.width = 42;
			base.item.height = 30;
			base.item.uniqueStack = true;
			base.item.rare = -11;
		}
	}
}
