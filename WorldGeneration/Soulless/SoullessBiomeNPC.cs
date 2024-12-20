using System;
using System.Collections.Generic;
using Redemption.NPCs.Bosses.Warden;
using Redemption.NPCs.Minibosses;
using Redemption.NPCs.Soulless;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.WorldGeneration.Soulless
{
	public class SoullessBiomeNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
			if (spawnInfo.player.GetModPlayer<RedePlayer>().ZoneSoulless)
			{
				pool.Clear();
				if (NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()))
				{
					return;
				}
				pool.Add(ModContent.NPCType<SoullessWanderer>(), 0.07f);
				pool.Add(ModContent.NPCType<TheSoulless2>(), 0.005f);
				pool.Add(ModContent.NPCType<SmallShadesoulNPC>(), 0.05f);
				pool.Add(ModContent.NPCType<ShadesoulNPC>(), 0.02f);
				pool.Add(ModContent.NPCType<Shadebug>(), 0.02f);
				if (!NPC.AnyNPCs(ModContent.NPCType<SoullessMarionette_Doll>()))
				{
					pool.Add(ModContent.NPCType<SoullessMarionette_Doll>(), 0.007f);
				}
				pool.Add(ModContent.NPCType<LaughingMaskSmall>(), 0.01f);
				pool.Add(ModContent.NPCType<LaughingMaskMedium>(), 0.01f);
				pool.Add(ModContent.NPCType<LaughingMaskBig>(), 0.01f);
				pool.Add(ModContent.NPCType<SpookyEyes>(), 0.02f);
				pool.Add(ModContent.NPCType<Echo>(), 0.02f);
				if (!NPC.AnyNPCs(ModContent.NPCType<MansionWraith2>()) && RedeWorld.downedMansionWraith && !NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()) && !spawnInfo.player.Hitbox.Intersects(ArenaPlayer.wardenArena))
				{
					pool.Add(ModContent.NPCType<MansionWraith2>(), 0.001f);
				}
			}
		}
	}
}
