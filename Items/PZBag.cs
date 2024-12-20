using System;
using Redemption.Items.Armor;
using Redemption.Items.LabThings;
using Redemption.Items.Weapons;
using Redemption.NPCs.LabNPCs.New;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class PZBag : ModItem
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
			base.item.rare = 11;
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
				return ModContent.NPCType<PZ2BodyCover>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<PZMask>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<PZGauntlet>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<SwarmerGun>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<XeniumSaber>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<MedicKit1>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<BluePrints>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<HeartOfInfection>(), 1);
		}
	}
}
