using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class DarkSoul5 : ModNPC
	{
		public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = base.npc.lifeMax;
			base.npc.damage = base.npc.damage;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dying Soul");
		}

		public override void SetDefaults()
		{
			base.npc.width = 20;
			base.npc.height = 26;
			base.npc.friendly = false;
			base.npc.damage = 15;
			base.npc.defense = 0;
			base.npc.lifeMax = 60;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 173, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
					Main.dust[num].noGravity = true;
					Main.dust[num].velocity *= 1.9f;
				}
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(4) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 58, 1, false, 0, false, false);
			}
		}

		public override bool PreAI()
		{
			if (Main.rand.Next(1) == 0)
			{
				int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 173, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 20, default(Color), 3f);
				Main.dust[num].noGravity = true;
			}
			base.npc.TargetClosest(true);
			int num2 = (int)base.npc.ai[0];
			if (num2 < 0 || num2 >= 200 || !Main.npc[num2].active || Main.npc[num2].type != base.mod.NPCType("TheKeeper"))
			{
				base.npc.active = false;
				return false;
			}
			this.rot += 0.07f;
			base.npc.netUpdate = true;
			Vector2 vector = Main.npc[num2].Center - base.npc.Center;
			vector.Normalize();
			vector *= 9f;
			base.npc.rotation = Utils.ToRotation(vector);
			NPC npc = Main.npc[(int)base.npc.ai[0]];
			base.npc.Center = npc.Center + RedeHelper.RotateVector(default(Vector2), this.rotVec, this.rot + base.npc.ai[2] * 0.628f);
			return false;
		}

		public override bool CheckActive()
		{
			return false;
		}

		public float rot;

		public Vector2 rotVec = new Vector2(0f, 170f);
	}
}
