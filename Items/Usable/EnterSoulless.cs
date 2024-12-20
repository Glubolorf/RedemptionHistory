using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.Warden;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class EnterSoulless : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadesoul Gateway");
			base.Tooltip.SetDefault("Opens a portal to the Soulless Caverns\nCan also be used to leave the Soulless Caverns\n'You feel keeping the gateway opened would be a bad idea...'");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 1;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 120;
			base.item.useTime = 120;
			base.item.UseSound = SoundID.NPCDeath52;
			base.item.useStyle = 4;
			base.item.consumable = false;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override bool UseItem(Player player)
		{
			for (int g = 0; g < 200; g++)
			{
				if (Main.npc[g].active && Main.npc[g].type == ModContent.NPCType<SoullessPortal>())
				{
					Main.npc[g].ai[0] = 3f;
					break;
				}
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<SoullessPortal>()))
			{
				NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, ModContent.NPCType<SoullessPortal>(), 0, 0f, 0f, 0f, 0f, 255);
				Main.NewText("A Shadesoul Gateway has been opened...", Color.LightSlateGray, false);
			}
			else
			{
				Main.NewText("A Shadesoul Gateway has been closed...", Color.LightSlateGray, false);
			}
			return true;
		}
	}
}
