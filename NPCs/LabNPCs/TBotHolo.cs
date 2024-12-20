using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class TBotHolo : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hologram");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 22;
			base.npc.height = 36;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.dontTakeDamage = true;
			base.npc.HitSound = SoundID.NPCHit13;
			base.npc.DeathSound = SoundID.NPCDeath19;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.alpha = 255;
			base.npc.noGravity = true;
			base.npc.netAlways = true;
		}

		public override void AI()
		{
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 10.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 38;
				if (base.npc.frame.Y > 38)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (base.npc.alpha > 100)
			{
				base.npc.alpha -= 10;
			}
			this.timer++;
			if (this.timer == 1 && Main.netMode != 1)
			{
				Vector2 vector;
				vector..ctor(10f, -60f);
				base.npc.Center = base.npc.position + vector;
				base.npc.netUpdate = true;
			}
			if (this.timer == 60)
			{
				Main.NewText("...", new Color(100, 120, 200), false);
			}
			if (this.timer == 120)
			{
				Main.NewText("...", new Color(100, 120, 200), false);
			}
			if (this.timer == 190)
			{
				Main.NewText("... Oh?", new Color(100, 120, 200), false);
			}
			if (this.timer == 270)
			{
				Main.NewText("Oh, sorry, I uhh... Looks like you've found my birthplace, the lab!", new Color(100, 120, 200), false);
			}
			if (this.timer == 400)
			{
				int num = NPC.FindFirstNPC(base.mod.NPCType("TBot"));
				if (num >= 0)
				{
					Main.NewText("It's me, " + Main.npc[num].GivenName + " the Friendly T-Bot!", new Color(100, 120, 200), false);
				}
				else
				{
					Main.NewText("It's me, the Friendly T-Bot!", new Color(100, 120, 200), false);
				}
			}
			if (this.timer == 580)
			{
				Main.NewText("Let me just quickly access the security and shut off the next green lasers ahead of you.", new Color(100, 120, 200), false);
			}
			if (this.timer == 740)
			{
				if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
				{
					Main.NewText("Just watch out, the deep chambers of the lab are quite dangerous, even to androids like you.", new Color(100, 120, 200), false);
				}
				else
				{
					Main.NewText("Just watch out, the deep chambers of the lab are quite dangerous to organisms, so don't get sick, okay?", new Color(100, 120, 200), false);
				}
			}
			if (this.timer == 1100)
			{
				Main.NewText("Oh also, I found some of my old stuff back from when I was still stuck there, you could come check it out. It's quite pricy though...", new Color(100, 120, 200), false);
			}
			if (this.timer >= 1600)
			{
				RedeWorld.tbotLabAccess = true;
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ZoneAccessPanel5"), 1, false, 0, false, false);
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
				Main.NewText("Good luck!", new Color(100, 120, 200), false);
				base.npc.active = false;
			}
		}

		public int timer;
	}
}
