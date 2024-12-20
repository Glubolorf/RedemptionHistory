using System;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class BottledLostSoulIcon : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Quest/BottledLostSoulIcon";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bottled Lost Soul");
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
