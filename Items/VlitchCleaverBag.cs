using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class VlitchCleaverBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			if (Main.netMode != 2)
			{
				Texture2D[] array = new Texture2D[Main.glowMaskTexture.Length + 1];
				for (int i = 0; i < Main.glowMaskTexture.Length; i++)
				{
					array[i] = Main.glowMaskTexture[i];
				}
				array[array.Length - 1] = base.mod.GetTexture("Items/" + base.GetType().Name + "_Glow");
				VlitchCleaverBag.customGlowMask = (short)(array.Length - 1);
				Main.glowMaskTexture = array;
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
			this.bossBagNPC = base.mod.NPCType("VlitchCleaver");
			base.item.glowMask = VlitchCleaverBag.customGlowMask;
		}

		public override bool CanRightClick()
		{
			return true;
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
				player.QuickSpawnItem(base.mod.ItemType("GirusDagger"), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(base.mod.ItemType("GirusLance"), 1);
			}
			player.QuickSpawnItem(base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(12, 24));
			player.QuickSpawnItem(base.mod.ItemType("VlitchBattery"), Main.rand.Next(1, 3));
		}

		public static short customGlowMask;
	}
}
