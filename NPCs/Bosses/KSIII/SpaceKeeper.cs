using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class SpaceKeeper : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Space Keeper");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 6000;
			base.npc.damage = 0;
			base.npc.defense = 20;
			base.npc.knockBackResist = 0f;
			base.npc.width = 44;
			base.npc.height = 68;
			base.npc.dontTakeDamage = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit42;
			base.npc.DeathSound = SoundID.NPCDeath14;
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 220)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.ai[3] == 1f)
			{
				this.chargeCounter++;
				if (this.chargeCounter > 5)
				{
					this.chargeFrame++;
					this.chargeCounter = 0;
				}
				if (this.chargeFrame >= 4)
				{
					this.chargeFrame = 0;
				}
			}
			NPC host = Main.npc[(int)base.npc.ai[0]];
			if (host.life <= 0 || !host.active || host.type != ModContent.NPCType<KS3_Body>())
			{
				base.npc.active = false;
				base.npc.life = 0;
			}
			if (host.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.ai[2] += 1f;
			if (base.npc.ai[2] == 1f)
			{
				for (int i = 0; i < 16; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.npc.Center.X - 1f, base.npc.Center.Y - 1f), 2, 2, 92, 0f, 0f, 100, Color.White, 4f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / 16f * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
			}
			if (base.npc.ai[2] > 180f && host.life < 10000)
			{
				base.npc.ai[3] = 1f;
				Dust dust = Dust.NewDustDirect(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 92, 0f, 0f, 100, default(Color), 2f);
				dust.velocity = -host.DirectionTo(dust.position) * 20f;
				dust.noGravity = true;
				base.npc.netUpdate = true;
			}
			if (host.ai[0] == 13f)
			{
				base.npc.ai[3] = 0f;
				NPC npc2 = base.npc;
				npc2.velocity.Y = npc2.velocity.Y - 0.5f;
				if (Vector2.Distance(base.npc.Center, host.Center) > 1500f)
				{
					base.npc.life = 0;
					base.npc.active = false;
					return;
				}
			}
			else
			{
				float obj = base.npc.ai[1];
				if (0f.Equals(obj))
				{
					base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], new Vector2(-250f, -250f), 11f, 15f);
					return;
				}
				if (1f.Equals(obj))
				{
					base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], new Vector2(250f, -250f), 11f, 15f);
					return;
				}
				if (2f.Equals(obj))
				{
					base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], new Vector2(-250f, 250f), 11f, 15f);
					return;
				}
				if (!3f.Equals(obj))
				{
					return;
				}
				base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], new Vector2(250f, 250f), 11f, 15f);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D chargeAni = base.mod.GetTexture("NPCs/Bosses/KSIII/SpaceKeeperCharge");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.ai[3] == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else if (base.npc.ai[3] == 1f)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = chargeAni.Height / 4;
				int y6 = num214 * this.chargeFrame;
				Main.spriteBatch.Draw(chargeAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, chargeAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)chargeAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public int chargeFrame;

		public int chargeCounter;
	}
}
