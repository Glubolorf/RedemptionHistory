using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Accessories.PreHM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.PreHM;
using Redemption.Items.Weapons.PreHM.Magic;
using Redemption.Items.Weapons.PreHM.Melee;
using Redemption.Items.Weapons.PreHM.Ranged;
using Redemption.Items.Weapons.PreHM.Summon;
using Redemption.NPCs.Bosses.TheKeeper;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class TheKeeperBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					glowMasks[i] = Main.glowMaskTexture[i];
				}
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/Usable/" + base.GetType().Name + "_Glow");
				TheKeeperBag.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
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
			base.item.glowMask = TheKeeperBag.customGlowMask;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<Keeper>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<TheKeeperMask>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<OldGathicWaraxe>(), 1);
			}
			int num = Main.rand.Next(5);
			if (num == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<KeepersBow>(), 1);
			}
			if (num == 1)
			{
				player.QuickSpawnItem(ModContent.ItemType<KeepersStaff>(), 1);
			}
			if (num == 2)
			{
				player.QuickSpawnItem(ModContent.ItemType<KeepersClaw>(), 1);
			}
			if (num == 3)
			{
				player.QuickSpawnItem(ModContent.ItemType<KeepersKnife>(), 1);
			}
			if (num == 4)
			{
				player.QuickSpawnItem(ModContent.ItemType<KeepersSummon>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<DarkShard>(), Main.rand.Next(3, 5));
			player.QuickSpawnItem(ModContent.ItemType<HeartEmblem>(), 1);
		}

		public static short customGlowMask;
	}
}
