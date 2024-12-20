using System;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class IntruderArmour : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Intruder's Body Armor");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 18;
			base.item.rare = 4;
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
