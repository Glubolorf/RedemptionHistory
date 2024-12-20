using System;
using Redemption.Items.Accessories.PreHM;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.PostML
{
	public class CrownOfThorns : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crown of Thorns");
			base.Tooltip.SetDefault("Increases damage of all Thorn-related weapons\nEvery 5th use of a weapon shoots a spread of thorns\nIncreased life regeneration while in the Jungle");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 32;
			base.item.value = Item.sellPrice(0, 9, 50, 0);
			base.item.expert = true;
			base.item.accessory = true;
		}

		public override bool CanEquipAccessory(Player player, int slot)
		{
			if (slot < 10)
			{
				int maxAccessoryIndex = 5 + player.extraAccessorySlots;
				for (int i = 3; i < 3 + maxAccessoryIndex; i++)
				{
					if (slot != i && player.armor[i].type == ModContent.ItemType<CircletOfBrambles>())
					{
						return false;
					}
				}
			}
			return true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).thornCrown = true;
			if (player.ZoneJungle)
			{
				player.lifeRegen += 2;
			}
		}
	}
}
