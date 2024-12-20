using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class ForestGolemHibernating : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Forest Golem");
		}

		public override void SetDefaults()
		{
			base.npc.width = 60;
			base.npc.height = 58;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 10;
			base.npc.lifeMax = 50;
			base.npc.HitSound = SoundID.NPCHit11;
			base.npc.DeathSound = SoundID.NPCHit11;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 3, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, 910, 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, 910, 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, 910, 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 3, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 1f);
		}

		public override void AI()
		{
			if (Main.raining)
			{
				this.regenTimer++;
				if (this.regenTimer >= 30 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
			if (base.npc.wet && !base.npc.lavaWet)
			{
				this.regenTimer++;
				if (this.regenTimer >= 20 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
		}

		public override bool CheckDead()
		{
			if (!RedeConfigClient.Instance.NoCombatText)
			{
				CombatText.NewText(base.npc.getRect(), Color.ForestGreen, "*Groan*", true, true);
			}
			base.npc.SetDefaults(ModContent.NPCType<ForestGolem>(), -1f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDayRain.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 2) ? 0.02f : 0f);
		}

		private int regenTimer;
	}
}
