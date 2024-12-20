using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class MartianScamHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Martian Scam Artist Hat");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 14;
			base.item.rare = 6;
			base.item.vanity = true;
		}

		public override bool DrawHead()
		{
			return true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
	}
}
