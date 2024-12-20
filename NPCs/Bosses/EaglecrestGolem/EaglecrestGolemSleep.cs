using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.EaglecrestGolem
{
	public class EaglecrestGolemSleep : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sleeping Stones");
		}

		public override void SetDefaults()
		{
			base.npc.width = 80;
			base.npc.height = 80;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 50;
			base.npc.lifeMax = 200;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/StoneHit");
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.chaseable = false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 2.9f);
		}

		public override bool CheckDead()
		{
			if (!Main.dedServ)
			{
				Redemption.Inst.TitleCardUIElement.DisplayTitle("Eaglecrest Golem", 60, 90, 0.8f, 0, new Color?(Color.Gray), "Guardian of Eaglecrest Meadows", true);
			}
			base.npc.SetDefaults(ModContent.NPCType<EaglecrestGolem>(), -1f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			for (int i = 0; i < 200; i++)
			{
				if (Main.npc[i].boss)
				{
					return 0f;
				}
			}
			return SpawnCondition.OverworldDay.Chance * ((NPC.downedBoss2 && !RedeWorld.downedEaglecrestGolem && !NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolem>()) && !NPC.AnyNPCs(ModContent.NPCType<EaglecrestGolemSleep>())) ? 0.1f : 0f);
		}
	}
}
