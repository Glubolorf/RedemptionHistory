using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		12
	})]
	public class MaskOfGrief : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mask of Grief");
			base.Tooltip.SetDefault("Decreases enemy aggro while in Soulless Caverns\n25% increased damage while in the Soulless Cavern");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 7, 50, 0);
			base.item.rare = 11;
			base.item.accessory = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.GetModPlayer<RedePlayer>().ZoneSoulless)
			{
				player.aggro -= 30;
				player.allDamage += 0.25f;
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
	}
}
