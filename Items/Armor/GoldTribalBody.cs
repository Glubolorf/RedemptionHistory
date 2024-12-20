using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class GoldTribalBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Gold Tribal Plate");
		}

		public override void SetDefaults()
		{
			base.item.width = 12;
			base.item.height = 8;
			base.item.rare = 3;
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
