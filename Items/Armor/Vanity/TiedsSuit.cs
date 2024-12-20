using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class TiedsSuit : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tied's Marvelous Suit");
			base.Tooltip.SetDefault("'Great for impersonating devs!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 22;
			base.item.rare = 9;
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = false;
		}
	}
}
