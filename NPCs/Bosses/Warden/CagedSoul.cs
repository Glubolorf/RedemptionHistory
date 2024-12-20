using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class CagedSoul : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Caged Soul Piece");
			Main.npcFrameCount[base.npc.type] = 8;
		}

		public override void SetDefaults()
		{
			base.npc.width = 34;
			base.npc.height = 48;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 15000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath6;
			base.npc.lavaImmune = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 0;
			base.npc.noGravity = true;
		}

		public override bool CheckActive()
		{
			return !NPC.AnyNPCs(ModContent.NPCType<WardenIdle>());
		}

		public override void AI()
		{
			if (!NPC.AnyNPCs(ModContent.NPCType<WardenIdle>()))
			{
				base.npc.active = false;
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || player.dead || !player.active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 50;
				if (base.npc.frame.Y >= 400)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glow = ModContent.GetTexture("Redemption/NPCs/Bosses/Warden/CagedSoul_Glow");
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
			BaseDrawing.DrawAura(spriteBatch, Main.npcTexture[base.npc.type], 0, base.npc.position + new Vector2(0f, 46f), base.npc.width, base.npc.height, this.auraPercent, 4f, base.npc.scale, base.npc.rotation, base.npc.spriteDirection, 4, base.npc.frame, 0f, 0f, new Color?(RedeColor.COLOR_GLOWPULSE));
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.Draw(glow, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White) * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, 0f, 0f, 100, default(Color), 4f);
					Main.dust[dustIndex].velocity *= 15f;
				}
				Player player = Main.player[(int)base.npc.ai[0]];
				player.statLife += (int)((float)player.statLifeMax2 * 0.1f);
				player.HealEffect((int)((float)player.statLifeMax2 * 0.1f), true);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}

		public float auraPercent;

		public bool auraDirection = true;
	}
}
