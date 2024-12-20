using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class LostSoulCatcher : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vacuum Bottle");
			base.Tooltip.SetDefault("'For capturing the souls of the innocent!'\nCatch a Lost Soul in a bug net while this is in your inventory");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 26;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}
	}
}
