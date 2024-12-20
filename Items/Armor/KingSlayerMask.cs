using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KingSlayerMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III Mask");
			base.Tooltip.SetDefault("'Was he a slayer of kings or the king of slayers?'\n'Or maybe he thought it was a cool name'");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.rare = 2;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return false;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
