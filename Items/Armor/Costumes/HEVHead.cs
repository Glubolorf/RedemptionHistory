using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Costumes
{
	public class HEVHead : EquipTexture
	{
		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}
	}
}
