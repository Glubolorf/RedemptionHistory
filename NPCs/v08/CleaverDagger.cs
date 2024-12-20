using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class CleaverDagger : ModNPC
	{
		public override void ScaleExpertStats(int playerXPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = base.npc.lifeMax;
			base.npc.damage = base.npc.damage;
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Girus Dagger");
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 40;
			base.npc.friendly = false;
			base.npc.damage = 72;
			base.npc.defense = 30;
			base.npc.lifeMax = 2500;
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
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 3f);
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
			base.npc.TargetClosest(true);
			int num = (int)base.npc.ai[0];
			if (num < 0 || num >= 200 || !Main.npc[num].active || Main.npc[num].type != base.mod.NPCType("VlitchCleaver"))
			{
				base.npc.active = false;
				return false;
			}
			this.rot += 0.04f;
			base.npc.netUpdate = true;
			Vector2 vector = Main.npc[num].Center - base.npc.Center;
			vector.Normalize();
			vector *= 9f;
			base.npc.rotation = Utils.ToRotation(vector);
			NPC npc = Main.npc[(int)base.npc.ai[0]];
			base.npc.Center = npc.Center + RedeHelper.RotateVector(default(Vector2), this.rotVec, this.rot + base.npc.ai[2] * 0.628f);
			return false;
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (Main.rand.Next(2) == 0 && !projectile.minion)
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/CleaverDagger_Glow");
			SpriteEffects spriteEffects = (base.npc.spriteDirection == -1) ? 0 : 1;
			spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, spriteEffects, 0f);
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
