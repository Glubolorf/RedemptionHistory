using System;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Weapons.PreHM.Summon;
using Redemption.NPCs.Bosses;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class KingChickenBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Treasure Bag");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 24;
			base.item.height = 24;
			base.item.rare = 9;
			base.item.expert = true;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<KingChicken>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<KingChickenMask>(), 1);
			}
			if (Main.rand.Next(10) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<CrownOfTheKing>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<EggStaff>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<SpiritOfLife>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<Grain>(), 1);
			player.QuickSpawnItem(264, 1);
		}
	}
}
