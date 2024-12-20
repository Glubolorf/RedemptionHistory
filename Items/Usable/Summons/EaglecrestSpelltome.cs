using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Usable.Summons
{
	public class EaglecrestSpelltome : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Eaglecrest Spelltome");
			base.Tooltip.SetDefault("Summons Eaglecrest Golem by force\nSold by Zephos/Daerel after Eater of Worlds/Brain of Cthulhu is defeated");
		}

		public override void SetDefaults()
		{
			base.item.UseSound = SoundID.Item1;
			base.item.useStyle = 2;
			base.item.useTurn = true;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 17;
			base.item.useTime = 17;
			base.item.consumable = true;
			base.item.width = 24;
			base.item.height = 38;
			base.item.maxStack = 1;
			base.item.value = Item.buyPrice(0, 10, 0, 0);
			base.item.rare = 6;
		}

		public override bool CanUseItem(Player player)
		{
			return !NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolem>()) && !NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolemSleep>()) && !SLWorld.subworld;
		}

		public override bool UseItem(Player player)
		{
			NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<EaglecrestGolemSleep>());
			Main.NewText("A sleeping stone appears...", Color.Gray, false);
			return true;
		}
	}
}
