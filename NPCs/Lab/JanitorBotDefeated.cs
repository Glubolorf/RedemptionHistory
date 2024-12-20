using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Lab
{
	public class JanitorBotDefeated : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Lab/JanitorBot";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Janitor");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 46;
			base.npc.height = 40;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.noGravity = false;
			base.npc.noTileCollide = false;
			base.npc.value = 5200f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.netAlways = true;
			base.npc.alpha = 0;
			base.npc.dontTakeDamage = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 6.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 44;
				if (base.npc.frame.Y > 132)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			this.walkCounter += base.npc.velocity.X * 0.5f;
			if (this.walkCounter >= 3f || this.walkCounter <= -3f)
			{
				this.walkCounter = 0f;
				this.walkFrame++;
				if (this.walkFrame >= 7)
				{
					this.walkCounter = 0f;
					this.walkFrame = 0;
				}
			}
			Vector2 TheDoor = new Vector2(this.Origin.X + 3040f, this.Origin.Y + 336f);
			if (player.IsFullTBot())
			{
				base.npc.ai[0] = 390f;
				if (Vector2.Distance(base.npc.Center, TheDoor) < 32f)
				{
					base.npc.velocity *= 0f;
					base.npc.ai[1] += 1f;
				}
				else
				{
					this.MoveToVector2(TheDoor);
				}
			}
			else
			{
				base.npc.ai[0] += 1f;
				if (base.npc.ai[0] == 10f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Okay, okay!", true, false);
				}
				if (base.npc.ai[0] == 100f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Alright fine, you probably can handle yourself here.", true, false);
				}
				if (base.npc.ai[0] == 260f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "Here, have this Lab Access thing and piss off!", true, false);
				}
				if (base.npc.ai[0] == 390f && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Colors.RarityYellow, "I got moppin' to do.", true, false);
				}
				if (base.npc.ai[0] >= 390f)
				{
					if (Vector2.Distance(base.npc.Center, TheDoor) < 32f)
					{
						base.npc.velocity *= 0f;
						base.npc.ai[1] += 1f;
					}
					else
					{
						this.MoveToVector2(TheDoor);
					}
				}
			}
			if (base.npc.ai[1] >= 1f)
			{
				base.npc.ai[2] += 1f;
				if (base.npc.ai[2] == 59f)
				{
					for (int g = 0; g < 2; g++)
					{
						int goreIndex = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
						Main.gore[goreIndex].scale = 1f;
						Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
						Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					}
				}
				if (base.npc.ai[2] >= 60f)
				{
					base.npc.velocity = new Vector2(0f, -500f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public void MoveToVector2(Vector2 p)
		{
			float moveSpeed = 2f;
			float velMultiplier = 1f;
			Vector2 dist = p - base.npc.Center;
			float length = (dist == Vector2.Zero) ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			base.npc.velocity = ((length == 0f) ? Vector2.Zero : Vector2.Normalize(dist));
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			base.npc.velocity *= moveSpeed;
			base.npc.velocity *= velMultiplier;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D walkAni = base.mod.GetTexture("NPCs/Lab/JanitorBotWalkAway");
			int spriteDirection = base.npc.spriteDirection;
			if (base.npc.velocity.X == 0f)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (base.npc.velocity.X != 0f)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = walkAni.Height / 7;
				int y6 = num214 * this.walkFrame;
				Main.spriteBatch.Draw(walkAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, walkAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)walkAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public Vector2 Origin = new Vector2((float)((int)((float)Main.maxTilesX * 0.55f)), (float)((int)((float)Main.maxTilesY * 0.65f))) * 16f;

		private int walkFrame;

		private float walkCounter;
	}
}
