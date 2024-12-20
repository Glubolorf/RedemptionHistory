using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Thorn
{
	[AutoloadBossHead]
	public class RockBarrier : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rock Orbital");
		}

		public override bool CheckActive()
		{
			return NPC.CountNPCS(ModContent.NPCType<Akka>()) < 1;
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 50;
			base.npc.damage = 100;
			base.npc.defense = 0;
			base.npc.lifeMax = 1000;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.lavaImmune = true;
			base.npc.dontTakeDamage = true;
		}

		public override void AI()
		{
			double rad = (double)base.npc.ai[1] * 0.017453292519943295;
			NPC host = Main.npc[(int)base.npc.ai[0]];
			base.npc.position.X = host.Center.X - (float)((int)(Math.Cos(rad) * this.dist)) - (float)(base.npc.width / 2);
			base.npc.position.Y = host.Center.Y - (float)((int)(Math.Sin(rad) * this.dist)) - (float)(base.npc.height / 2);
			base.npc.ai[1] += 1f;
			if (host.life <= 0 || !host.active || host.type != ModContent.NPCType<Akka>())
			{
				base.npc.active = false;
				base.npc.life = 0;
				base.npc.checkDead();
				base.npc.HitEffect(0, 10.0);
			}
			float num = 8f;
			Vector2 vector = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
			float hostX = host.position.X + (float)(host.width / 2);
			float hostY = host.position.Y + (float)(host.height / 2);
			hostX = (float)((int)(hostX / 8f) * 8);
			hostY = (float)((int)(hostY / 8f) * 8);
			vector.X = (float)((int)(vector.X / 8f) * 8);
			vector.Y = (float)((int)(vector.Y / 8f) * 8);
			hostX -= vector.X;
			hostY -= vector.Y;
			float rootXY = (float)Math.Sqrt((double)(hostX * hostX + hostY * hostY));
			if (rootXY == 0f)
			{
				hostX = base.npc.velocity.X;
				hostY = base.npc.velocity.Y;
			}
			else
			{
				rootXY = num / rootXY;
				hostX *= rootXY;
				hostY *= rootXY;
			}
			base.npc.rotation = (float)Math.Atan2((double)hostY, (double)hostX) + 3.14f + MathHelper.ToRadians(-90f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 4; i++)
				{
					Dust dust = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 1, 0f, 0f, 100, default(Color), 2f);
					dust.velocity = -base.npc.DirectionTo(dust.position) * 5f;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glow = base.mod.GetTexture("NPCs/Bosses/Thorn/RockBarrier_Glow");
			Vector2 position = base.npc.Center - Main.screenPosition;
			Rectangle rect = new Rectangle(0, 0, texture.Width, texture.Height);
			Vector2 origin = new Vector2((float)texture.Width / 2f, (float)texture.Width / 2f);
			spriteBatch.Draw(texture, position, new Rectangle?(rect), lightColor * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, origin, base.npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(glow, position, new Rectangle?(rect), base.npc.GetAlpha(RedeColor.FadeColour1) * ((float)(255 - base.npc.alpha) / 255f), base.npc.rotation, origin, base.npc.scale, SpriteEffects.None, 1f);
			return false;
		}

		private readonly double dist = 1800.0;
	}
}
