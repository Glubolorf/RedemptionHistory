using System;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Armor;
using Redemption.Items.DruidDamageClass.SeedBags;
using Redemption.Items.Weapons;
using Redemption.NPCs.Bosses.OmegaOblit;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class OmegaOblitBag : ModItem
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
				OmegaOblitBag.customGlowMask = (short)(glowMasks.Length - 1);
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
			base.item.glowMask = OmegaOblitBag.customGlowMask;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override int BossBagNPC
		{
			get
			{
				return ModContent.NPCType<OO>();
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
				player.QuickSpawnItem(ModContent.ItemType<PlasmaJawser>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<OmegaClaw>(), 1);
			}
			if (Main.rand.Next(3) == 0)
			{
				player.QuickSpawnItem(ModContent.ItemType<GloopContainer>(), 1);
			}
			player.QuickSpawnItem(ModContent.ItemType<CorruptedXenomite>(), Main.rand.Next(30, 40));
			player.QuickSpawnItem(ModContent.ItemType<VlitchBattery>(), Main.rand.Next(4, 6));
			player.QuickSpawnItem(ModContent.ItemType<OblitBrain>(), 1);
			player.QuickSpawnItem(ModContent.ItemType<ObliterationDrive>(), 1);
		}

		public static short customGlowMask;
	}
}
