using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Dusts;
using Redemption.NPCs.HM;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class VlitchWormTail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Gigapede");
		}

		public override void SetDefaults()
		{
			base.npc.width = 122;
			base.npc.height = 92;
			base.npc.damage = 140;
			base.npc.defense = 250;
			base.npc.lifeMax = 1;
			base.npc.boss = true;
			base.npc.friendly = false;
			base.npc.knockBackResist = 0f;
			base.npc.behindTiles = true;
			base.npc.noTileCollide = true;
			base.npc.buffImmune[20] = true;
			base.npc.buffImmune[31] = true;
			base.npc.buffImmune[39] = true;
			base.npc.buffImmune[24] = true;
			base.npc.netAlways = true;
			base.npc.noGravity = true;
			base.npc.dontCountMe = true;
			base.npc.HitSound = SoundID.NPCHit4;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.Center, base.npc.velocity, base.mod.GetGoreSlot("Gores/Boss/VlitchWormGore3"), 1f);
			}
		}

		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (projectile.penetrate != 1)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].active && (Main.npc[i].whoAmI == base.npc.realLife || (Main.npc[i].realLife >= 0 && Main.npc[i].realLife == base.npc.realLife)))
					{
						Main.npc[i].immune[projectile.owner] = 10;
					}
				}
				damage = (int)((float)damage * 0.44f);
			}
			if (projectile.ranged && (projectile.width < 20 || projectile.height < 20) && Main.rand.Next(2) == 0)
			{
				if (!Main.dedServ)
				{
					if (Main.rand.Next(3) == 0)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BulletBounce1").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
					else if (Main.rand.Next(3) == 1)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BulletBounce2").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
					else
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/BulletBounce3").WithVolume(0.4f).WithPitchVariance(0.1f), -1, -1);
					}
				}
				damage = (int)((float)damage * 0.5f);
				if (projectile.penetrate == 1)
				{
					projectile.penetrate = 2;
				}
				projectile.velocity.X = -projectile.velocity.X + Utils.NextFloat(Main.rand, -3f, 3f);
				projectile.velocity.Y = -projectile.velocity.Y + Utils.NextFloat(Main.rand, -3f, 3f);
				projectile.friendly = false;
				projectile.hostile = false;
			}
		}

		public override void AI()
		{
			if (NPC.AnyNPCs(ModContent.NPCType<VlitchCore1>()))
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<VlitchCore2>()))
			{
				base.npc.dontTakeDamage = true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<VlitchCore3>()))
			{
				base.npc.dontTakeDamage = true;
			}
			if (!NPC.AnyNPCs(ModContent.NPCType<VlitchCore1>()) && !NPC.AnyNPCs(ModContent.NPCType<VlitchCore2>()) && !NPC.AnyNPCs(ModContent.NPCType<VlitchCore3>()))
			{
				base.npc.dontTakeDamage = false;
			}
			if (base.npc.ai[0] % 500f == 3f && NPC.CountNPCS(ModContent.NPCType<CorruptedWormHead>()) <= 4)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<CorruptedWormHead>(), 0, 0f, 0f, 0f, 0f, 255);
			}
			float[] ai = base.npc.ai;
			int num = 0;
			float num2 = ai[num];
			ai[num] = num2 + 1f;
			if (num2 >= 400f && NPC.CountNPCS(ModContent.NPCType<CorruptedProbe>()) <= 2)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, ModContent.NPCType<CorruptedProbe>(), 0, 0f, 0f, 0f, 0f, 255);
				base.npc.ai[0] = 0f;
			}
			if (base.npc.ai[3] > 0f)
			{
				base.npc.realLife = (int)base.npc.ai[3];
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead)
			{
				base.npc.TargetClosest(true);
			}
			if (Main.player[base.npc.target].dead && base.npc.timeLeft > 300)
			{
				base.npc.timeLeft = 300;
			}
			if (Main.netMode != 1 && !Main.npc[(int)base.npc.ai[1]].active)
			{
				base.npc.life = 0;
				base.npc.HitEffect(0, 10.0);
				base.npc.active = false;
				NetMessage.SendData(28, -1, -1, null, base.npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
			}
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, ModContent.DustType<VlitchFlame>(), 0f, 0f, 0, default(Color), 1f);
			}
			if ((double)base.npc.ai[1] < 200.0)
			{
				Vector2 npcCenter = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float dirX = Main.npc[(int)base.npc.ai[1]].position.X + (float)(Main.npc[(int)base.npc.ai[1]].width / 2) - npcCenter.X;
				float dirY = Main.npc[(int)base.npc.ai[1]].position.Y + (float)(Main.npc[(int)base.npc.ai[1]].height / 2) - npcCenter.Y;
				base.npc.rotation = (float)Math.Atan2((double)dirY, (double)dirX) + 1.57f;
				float length = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));
				float dist = (length - (float)base.npc.width) / length;
				float posX = dirX * dist;
				float posY = dirY * dist;
				base.npc.velocity = Vector2.Zero;
				base.npc.position.X = base.npc.position.X + posX;
				base.npc.position.Y = base.npc.position.Y + posY;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/VlitchWormTail_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			Main.spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, null, drawColor, base.npc.rotation, origin, base.npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		public override bool CheckActive()
		{
			return !Main.npc[(int)base.npc.ai[1]].active;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return new bool?(false);
		}
	}
}
