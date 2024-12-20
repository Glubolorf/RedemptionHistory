using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class Blisterling2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Blisterling");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 28;
			base.npc.friendly = false;
			base.npc.damage = 90;
			base.npc.defense = 0;
			base.npc.lifeMax = 950;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 0f;
			base.npc.noGravity = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 16;
			this.aiType = 58;
			this.animationType = 58;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.9f;
				}
			}
		}

		public override void AI()
		{
			this.fightTimer++;
			if (this.fightTimer == 120)
			{
				this.jumpAttack = true;
			}
			if (!this.jumpAttack)
			{
				base.npc.noTileCollide = false;
			}
			if (this.jumpAttack)
			{
				this.jumpTimer++;
				base.npc.noTileCollide = true;
				base.npc.velocity.X = 0f;
				if (this.jumpTimer == 1)
				{
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					Math.Atan2((double)(vector8.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector8.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = 0f;
					base.npc.velocity.Y = -15f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Color color = default(Color);
					Rectangle rectangle = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count = 30;
					for (int i = 1; i <= count; i++)
					{
						int dust = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 273, 0f, 0f, 100, color, 2.5f);
						Main.dust[dust].noGravity = false;
					}
					return;
				}
				if (this.jumpTimer >= 68)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + 0.15f;
				}
				if (this.jumpTimer >= 180 && base.npc.wet)
				{
					this.fightTimer = 0;
					this.jumpAttack = false;
					this.jumpTimer = 0;
				}
			}
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff>(), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(ModContent.BuffType<XenomiteDebuff2>(), Main.rand.Next(250, 500), true);
			}
		}

		private int fightTimer;

		private bool jumpAttack;

		private int jumpTimer;
	}
}
