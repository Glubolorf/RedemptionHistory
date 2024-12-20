using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class BabbyDragonTail1 : BabbyDragonHead
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/v08/BabbyDragonTail1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Star Serpent");
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.npc.width = 40;
			base.npc.height = 36;
			base.npc.dontCountMe = true;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return new bool?(false);
		}

		public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool PreAI()
		{
			Vector2 directionVector = Main.npc[(int)base.npc.ai[1]].Center - base.npc.Center;
			base.npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
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
			if (Main.netMode != 1 && (!Main.npc[(int)base.npc.ai[1]].active || Main.npc[(int)base.npc.ai[3]].type != ModContent.NPCType<BabbyDragonHead>()))
			{
				base.npc.life = 0;
				base.npc.HitEffect(0, 10.0);
				base.npc.active = false;
				NetMessage.SendData(28, -1, -1, null, base.npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
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
				if (dirX < 0f)
				{
					base.npc.spriteDirection = 1;
				}
				else
				{
					base.npc.spriteDirection = -1;
				}
				base.npc.velocity = Vector2.Zero;
				base.npc.position.X = base.npc.position.X + posX;
				base.npc.position.Y = base.npc.position.Y + posY;
			}
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 0f;
		}
	}
}
