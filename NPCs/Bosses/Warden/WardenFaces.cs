using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Warden
{
	public class WardenFaces : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mask");
			Main.npcFrameCount[base.npc.type] = 5;
		}

		public override void SetDefaults()
		{
			base.npc.width = 18;
			base.npc.height = 24;
			base.npc.dontTakeDamage = true;
			base.npc.noGravity = true;
			base.npc.aiStyle = -1;
			base.npc.lifeMax = 1;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.noTileCollide = true;
			base.npc.alpha = 255;
			base.npc.npcSlots = 0f;
		}

		public override bool CheckActive()
		{
			return false;
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			NPC host = Main.npc[(int)base.npc.ai[0]];
			Vector2 MainFace = new Vector2((host.spriteDirection == 1) ? (host.Center.X + 10f) : (host.Center.X - 10f), host.Center.Y - 36f);
			Vector2 MainFace2 = new Vector2((float)((host.spriteDirection == 1) ? 10 : -10), -36f);
			base.npc.spriteDirection = host.spriteDirection;
			if (!host.active || host.type != ModContent.NPCType<WardenIdle>() || (host.ai[0] == 25f && host.ai[2] >= 70f))
			{
				for (int i = 0; i < 10; i++)
				{
					Dust dust = Dust.NewDustDirect(base.npc.position, base.npc.width, base.npc.height, 261, 0f, 0f, 100, default(Color), 2f);
					dust.velocity = -base.npc.DirectionTo(dust.position);
				}
				base.npc.active = false;
				return;
			}
			float obj = base.npc.ai[1];
			if (!0f.Equals(obj))
			{
				if (!1f.Equals(obj))
				{
					if (!2f.Equals(obj))
					{
						if (!3f.Equals(obj))
						{
							if (!4f.Equals(obj))
							{
								return;
							}
							base.npc.GivenName = "Mask of Calm";
							base.npc.frame.Y = 104;
							if (host.ai[3] == 4f)
							{
								base.npc.Center = MainFace;
								base.npc.alpha = 255;
								return;
							}
							if (host.ai[1] == 1f || (host.ai[1] == 3f && host.ai[0] == 5f) || host.ai[0] == 24f)
							{
								base.npc.Center = MainFace + new Vector2(10f, -40f);
							}
							if (host.ai[0] == 7f)
							{
								base.npc.GivenName = "";
								base.npc.alpha = 255;
								return;
							}
							base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], MainFace2 + new Vector2(10f, -40f), 8f, 20f);
							base.npc.alpha = host.alpha;
							return;
						}
						else
						{
							base.npc.GivenName = "Mask of Grief";
							base.npc.frame.Y = 78;
							if (host.ai[3] == 3f)
							{
								base.npc.Center = MainFace;
								base.npc.alpha = 255;
								return;
							}
							if (host.ai[1] == 1f || (host.ai[1] == 3f && host.ai[0] == 5f) || host.ai[0] == 24f)
							{
								base.npc.Center = MainFace + new Vector2(30f, -20f);
							}
							if (host.ai[0] == 7f)
							{
								base.npc.GivenName = "";
								base.npc.alpha = 255;
								return;
							}
							base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], MainFace2 + new Vector2(30f, -20f), 8f, 20f);
							base.npc.alpha = host.alpha;
							return;
						}
					}
					else
					{
						base.npc.GivenName = "Mask of Struggle";
						base.npc.frame.Y = 52;
						if (host.ai[3] == 2f)
						{
							base.npc.Center = MainFace;
							base.npc.alpha = 255;
							return;
						}
						if (host.ai[1] == 1f || (host.ai[1] == 3f && host.ai[0] == 5f) || host.ai[0] == 24f)
						{
							base.npc.Center = MainFace + new Vector2(-20f, 30f);
						}
						if (host.ai[0] == 7f)
						{
							base.npc.GivenName = "";
							base.npc.alpha = 255;
							return;
						}
						base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], MainFace2 + new Vector2(-20f, 30f), 8f, 20f);
						base.npc.alpha = host.alpha;
						return;
					}
				}
				else
				{
					base.npc.GivenName = "Mask of Wrath";
					base.npc.frame.Y = 26;
					if (host.ai[3] == 1f)
					{
						base.npc.Center = MainFace;
						base.npc.alpha = 255;
						return;
					}
					if (host.ai[1] == 1f || (host.ai[1] == 3f && host.ai[0] == 5f) || host.ai[0] == 24f)
					{
						base.npc.Center = MainFace + new Vector2(30f, 20f);
					}
					if (host.ai[0] == 7f)
					{
						base.npc.GivenName = "";
						base.npc.alpha = 255;
						return;
					}
					base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], MainFace2 + new Vector2(30f, 20f), 8f, 20f);
					base.npc.alpha = host.alpha;
					return;
				}
			}
			else
			{
				base.npc.GivenName = "Mask of Shock";
				base.npc.frame.Y = 0;
				if (host.ai[3] == 0f)
				{
					base.npc.Center = MainFace;
					base.npc.alpha = 255;
					return;
				}
				if (host.ai[1] == 1f || (host.ai[1] == 3f && host.ai[0] == 5f) || host.ai[0] == 24f)
				{
					base.npc.Center = MainFace + new Vector2(-30f, -20f);
				}
				if (host.ai[0] == 7f)
				{
					base.npc.GivenName = "";
					base.npc.alpha = 255;
					return;
				}
				base.npc.MoveToNPC(Main.npc[(int)base.npc.ai[0]], MainFace2 + new Vector2(-30f, -20f), 8f, 20f);
				base.npc.alpha = host.alpha;
				return;
			}
		}
	}
}
