using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class GirusTalking3 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ominous Voice");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 144;
			base.projectile.height = 144;
			base.projectile.friendly = false;
		}

		public override void AI()
		{
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] == 600f)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (!Main.dedServ)
				{
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Welp, so much for that prototype.", 200, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				Redemption.GirusSilence = true;
			}
			if (!Main.dedServ)
			{
				if (base.projectile.localAI[1] == 800f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("I've noted the flaws so I can perfect the design.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1100f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("...", 150, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1250f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Say, you would've now defeated every Overlord I had.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1550f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Well, bad or good news depending on you, that statement is now false.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1850f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("One more has joined the ranks, and they are now contesting for YOUR place.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 2150f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (RedeWorld.downedSlayer)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I think you two have already met.", 250, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
					else if (RedeWorld.slayerDeath > 1)
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Now's your chance to gain revenge on them.", 250, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("I don't think you two have met.", 250, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
				}
				if (base.projectile.localAI[1] == 2400f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("So.", 100, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 2500f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("I'll be watching you two fight.", 200, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 2700f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("I want the strongest to be on my side.", 220, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 2920f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Good luck.", 120, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
			}
			if (base.projectile.localAI[1] >= 3040f)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (!Main.dedServ)
				{
					Redemption.Inst.DialogueUIElement.DisplayDialogue("You'll need it.", 160, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				base.projectile.Kill();
			}
			if (MoRDialogueUI.Visible)
			{
				Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.GirusTier);
			}
		}

		public override void Kill(int timeleft)
		{
			RedeWorld.girusTalk3 = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}
	}
}
