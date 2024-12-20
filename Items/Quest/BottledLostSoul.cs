using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Quest
{
	public class BottledLostSoul : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bottled Lost Soul");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(4, 8));
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
