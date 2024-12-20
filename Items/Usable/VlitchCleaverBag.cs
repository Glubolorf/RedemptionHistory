using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Accessories.HM;
using Redemption.Items.Armor.Vanity;
using Redemption.Items.Materials.HM;
using Redemption.Items.Weapons.HM.Melee;
using Redemption.NPCs.Bosses.VCleaver;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class VlitchCleaverBag : ModItem
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
				VlitchCleaverBag.customGlowMask = (short)(glowMasks.Length - 1);
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
			base.item.glowMask = VlitchCleaverBag.customGlowMask;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<VlitchCleaver>();
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
				player.QuickSpawnItem(ModContent.ItemType<GirusDagger>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<GirusLance>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(12, 24));
			player.QuickSpawnItem(ModContent.ItemType<VlitchBattery>(), Main.rand.Next(1, 3));
			player.QuickSpawnItem(ModContent.ItemType<BrokenBlade>(), 1);
		}

		public static short customGlowMask;
	}
}
