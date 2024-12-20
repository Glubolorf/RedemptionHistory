using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Vanity
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class TBotVanityChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("T-Bot Chestplate");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.rare = 1;
			base.item.value = Item.buyPrice(0, 3, 0, 0);
			base.item.vanity = true;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = false;
			drawArms = false;
		}

		public override bool DrawBody()
		{
			return false;
		}
	}
}
