using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Weapons.HM.Ranged;
using Redemption.NPCs.Bosses;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class VlitchGigipedeBag : ModItem
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
				VlitchGigipedeBag.customGlowMask = (short)(glowMasks.Length - 1);
				Main.glowMaskTexture = glowMasks;
			}
			base.DisplayName.SetDefault("Treasure Box");
			base.Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		}

		public override void SetDefaults()
		{
			base.item.maxStack = 999;
			base.item.consumable = true;
			base.item.width = 24;
			base.item.height = 24;
			base.item.rare = 10;
			base.item.expert = true;
			base.item.glowMask = VlitchGigipedeBag.customGlowMask;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<VlitchWormHead>();
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(20) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<IntruderMask>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<IntruderArmour>(), 1);
				player.QuickSpawnItem(ModContent.ItemType<IntruderPants>(), 1);
			}
			if (Main.rand.Next(14) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<GirusMask>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<CorruptedRocketLauncher>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<CorruptedDoubleRifle>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(18, 28));
			player.QuickSpawnItem(ModContent.ItemType<VlitchScale>(), Main.rand.Next(25, 35));
			player.QuickSpawnItem(ModContent.ItemType<CorruptedStarliteBar>(), Main.rand.Next(20, 25));
			player.QuickSpawnItem(ModContent.ItemType<VlitchBattery>(), Main.rand.Next(2, 4));
			player.QuickSpawnItem(ModContent.ItemType<MiniVlitchCoreItem>(), 1);
		}

		public static short customGlowMask;
	}
}
