using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class HallamHoodie : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hallam's Casual Hoodie");
			base.Tooltip.SetDefault("'Great for impersonating devs!'");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 20;
			base.item.rare = 9;
			base.item.expert = true;
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
