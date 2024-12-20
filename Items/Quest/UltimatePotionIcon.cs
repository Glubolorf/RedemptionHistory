using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class UltimatePotionIcon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ultimate Potion");
			base.Tooltip.SetDefault("Icon for the quest since the chat box doesn't like animated sprites");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 34;
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.rare = -11;
		}
	}
}
