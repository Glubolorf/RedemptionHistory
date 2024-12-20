using System;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Weapons.HM.Magic;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.NPCs.Bosses.KSIII;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class SlayerBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cyber Loot Box");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 32;
			base.item.height = 32;
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
				return ModContent.NPCType<KS3_Body>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<KingSlayerMask>(), 1);
			}
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<SlayerFlamethrower>(), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(ModContent.ItemType<SlayerNanogun>(), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(ModContent.ItemType<SlayerFist>(), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(ModContent.ItemType<SlayerGun>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<KingCore>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<SlayerMedal>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<Holokey>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<CyberPlating>(), Main.rand.Next(12, 16));
			player.QuickSpawnItem(ModContent.ItemType<PocketShieldProjector>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<StarcruiserRadar>(), 1);
		}
	}
}
