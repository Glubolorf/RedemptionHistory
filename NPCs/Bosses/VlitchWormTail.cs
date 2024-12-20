using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class VlitchWormTail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Gigipede");
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
				Gore.NewGore(base.npc.Center, base.npc.velocity, base.mod.GetGoreSlot("Gores/VlitchCleaverGore11"), 1f);
			}
		}

		public override void AI()
		{
			if (base.npc.ai[0] % 500f == 3f)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("CorruptedWormHead"), 0, 0f, 0f, 0f, 0f, 255);
			}
			float[] ai = base.npc.ai;
			int num = 0;
			float num2;
			ai[num] = (num2 = ai[num]) + 1f;
			if (num2 >= 400f)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("CorruptedProbe"), 0, 0f, 0f, 0f, 0f, 255);
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
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			if ((double)base.npc.ai[1] < (double)Main.npc.Length)
			{
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float num3 = Main.npc[(int)base.npc.ai[1]].position.X + (float)(Main.npc[(int)base.npc.ai[1]].width / 2) - vector.X;
				float num4 = Main.npc[(int)base.npc.ai[1]].position.Y + (float)(Main.npc[(int)base.npc.ai[1]].height / 2) - vector.Y;
				base.npc.rotation = (float)Math.Atan2((double)num4, (double)num3) + 1.57f;
				float num5 = (float)Math.Sqrt((double)(num3 * num3 + num4 * num4));
				float num6 = (num5 - (float)base.npc.width) / num5;
				float num7 = num3 * num6;
				float num8 = num4 * num6;
				base.npc.velocity = Vector2.Zero;
				base.npc.position.X = base.npc.position.X + num7;
				base.npc.position.Y = base.npc.position.Y + num8;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Vector2 vector;
			vector..ctor((float)texture2D.Width * 0.5f, (float)texture2D.Height * 0.5f);
			Main.spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, null, drawColor, base.npc.rotation, vector, base.npc.scale, 0, 0f);
			return false;
		}

		public override bool CheckActive()
		{
			return !Main.npc[(int)base.npc.ai[3]].active;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return new bool?(false);
		}
	}
}
