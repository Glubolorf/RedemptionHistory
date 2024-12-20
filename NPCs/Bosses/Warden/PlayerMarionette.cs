using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class PlayerMarionette : ModNPC
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Soulless/SoullessMarionette_Cross";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Marionette Cross");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 30000;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.width = 52;
			base.npc.height = 32;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.netAlways = true;
			base.npc.HitSound = SoundID.NPCHit48;
			base.npc.DeathSound = SoundID.NPCDeath50;
			base.npc.dontTakeDamage = true;
		}

		public override bool CheckActive()
		{
			Player player = Main.player[(int)base.npc.ai[0]];
			return player.dead || !player.active;
		}

		public override void AI()
		{
			Player player = Main.player[(int)base.npc.ai[0]];
			if (player.dead || !player.active)
			{
				base.npc.active = false;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 7.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 216)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			player.position = base.npc.position - new Vector2(0f, -200f);
			float obj = base.npc.ai[2];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					base.npc.dontTakeDamage = false;
					base.npc.ai[1] += 1f;
					if (base.npc.ai[1] == 10f)
					{
						base.npc.Shoot(new Vector2(this.leftSide ? (player.Center.X + 400f) : (player.Center.X - 400f), player.Center.Y), ModContent.ProjectileType<ShadowMaw>(), 500, Vector2.Zero, false, SoundID.Item1.WithVolume(0f), "", (float)player.whoAmI, 0f);
					}
					if (base.npc.ai[1] > 30f)
					{
						base.npc.velocity.X = (this.leftSide ? 0.9f : -0.9f);
						if (!RedeHelper.AnyProjectiles(ModContent.ProjectileType<ShadowMaw>()))
						{
							base.npc.alpha = 255;
							base.npc.active = false;
						}
					}
					if (base.npc.ai[1] == 380f && !Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ShadeMaw1"), player.position);
					}
				}
			}
			else
			{
				base.npc.dontTakeDamage = true;
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] == 1f)
				{
					this.leftSide = (player.Center.X < 17616f);
				}
				if (base.npc.Distance(new Vector2(1099f, 1230f) * 16f) < 50f)
				{
					base.npc.velocity *= 0f;
					base.npc.ai[1] = 0f;
					base.npc.ai[2] = 1f;
					base.npc.netUpdate = true;
				}
				else
				{
					base.npc.MoveToVector2(new Vector2(1099f, 1230f) * 16f, 10f);
				}
			}
			if (base.npc.ai[1] % 30f == 0f)
			{
				int steps = (int)base.npc.Distance(player.Center) / 32;
				for (int i = 0; i < steps; i++)
				{
					if (Utils.NextBool(Main.rand, 4))
					{
						Dust dust = Dust.NewDustDirect(Vector2.Lerp(base.npc.Center, player.Center, (float)i / (float)steps), 2, 2, 261, 0f, 0f, 0, default(Color), 2f);
						dust.velocity *= 0f;
						dust.noGravity = true;
					}
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			new Vector2(base.npc.Center.X, base.npc.Center.Y);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			if (this.auraDirection)
			{
				this.auraPercent += 0.1f;
				this.auraDirection = (this.auraPercent < 1f);
			}
			else
			{
				this.auraPercent -= 0.1f;
				this.auraDirection = (this.auraPercent <= 0f);
			}
			BaseDrawing.DrawAura(spriteBatch, Main.npcTexture[base.npc.type], 0, base.npc.position + new Vector2(0f, 12f), base.npc.width, base.npc.height, this.auraPercent, 4f, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, 4, base.npc.frame, 0f, 0f, new Color?(RedeColor.COLOR_GLOWPULSE));
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 30; i++)
				{
					int dustIndex2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, ModContent.DustType<VoidFlame>(), 0f, 0f, 100, default(Color), 2f);
					Main.dust[dustIndex2].velocity *= 2.6f;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public bool leftSide;

		public float auraPercent;

		public bool auraDirection = true;
	}
}
