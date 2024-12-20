using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	public class ManaBarrierPro2 : ModNPC
	{
		public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = base.npc.lifeMax;
			base.npc.damage = base.npc.damage;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mana Barrier");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 80;
			base.npc.height = 80;
			base.npc.friendly = false;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.immortal = true;
		}

		public override bool PreAI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 82;
				if (base.npc.frame.Y > 164)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			base.npc.TargetClosest(true);
			int num = (int)base.npc.ai[0];
			if (num < 0 || num >= 200 || !Main.npc[num].active || Main.npc[num].type != base.mod.NPCType("ThornPZ"))
			{
				base.npc.active = false;
				return false;
			}
			this.rot += 0.07f;
			base.npc.netUpdate = true;
			Vector2 vector = Main.npc[num].Center - base.npc.Center;
			vector.Normalize();
			vector *= 9f;
			base.npc.rotation = Utils.ToRotation(vector);
			NPC npc2 = Main.npc[(int)base.npc.ai[0]];
			base.npc.Center = npc2.Center + RedeHelper.RotateVector(default(Vector2), this.rotVec, this.rot + base.npc.ai[2] * 0.628f);
			return false;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (projectile.magic)
			{
				if (projectile.penetrate == 1)
				{
					projectile.penetrate = 2;
				}
				projectile.damage = damage / 4;
				projectile.velocity.X = -projectile.velocity.X;
				projectile.velocity.Y = -projectile.velocity.Y;
				projectile.friendly = false;
				projectile.hostile = true;
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool CheckActive()
		{
			return false;
		}

		public float rot;

		public Vector2 rotVec = new Vector2(0f, 110f);
	}
}
