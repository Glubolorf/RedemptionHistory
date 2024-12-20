using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
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
				glowMasks[glowMasks.Length - 1] = base.mod.GetTexture("Items/" + base.GetType().Name + "_Glow");
				VlitchGigipedeBag.customGlowMask = (short)(glowMasks.Length - 1);
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
				return base.mod.NPCType("VlitchWormHead");
			}
		}

		public override void OpenBossBag(Player player)
		{
			if (Main.rand.Next(20) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("IntruderMask"), 1);
				player.QuickSpawnItem(base.mod.ItemType("IntruderArmour"), 1);
				player.QuickSpawnItem(base.mod.ItemType("IntruderPants"), 1);
			}
			if (Main.rand.Next(14) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("GirusMask"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("CorruptedRocketLauncher"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("CorruptedDoubleRifle"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(18, 28));
			player.QuickSpawnItem(base.mod.ItemType("VlitchScale"), Main.rand.Next(25, 35));
			player.QuickSpawnItem(base.mod.ItemType("CorruptedStarliteBar"), Main.rand.Next(20, 25));
			player.QuickSpawnItem(base.mod.ItemType("VlitchBattery"), Main.rand.Next(2, 4));
			player.QuickSpawnItem(base.mod.ItemType("MiniVlitchCoreItem"), 1);
		}

		public static short customGlowMask;
	}
}
