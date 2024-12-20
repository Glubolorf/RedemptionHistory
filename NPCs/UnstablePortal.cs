using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class UnstablePortal : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Unstable Portal");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 44;
			base.npc.friendly = false;
			base.npc.damage = 120;
			base.npc.defense = -100;
			base.npc.lifeMax = 1000;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 2;
			this.aiType = 34;
			this.animationType = 34;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("UnstablePortalBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 25; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 242, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 242, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			base.npc.rotation += 0.04f;
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 242, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Overworld.Chance * ((Main.hardMode && RedeWorld.downedXenomiteCrystal) ? 0.02f : 0f);
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(1) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(164, 200, true);
			}
		}
	}
}
