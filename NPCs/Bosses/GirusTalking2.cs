using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class GirusTalking2 : ModProjectile
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
			if (base.projectile.localAI[1] == 580f)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (!Main.dedServ)
				{
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Second Overlord is down. Oh well.", 220, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				Redemption.GirusSilence = true;
			}
			if (!Main.dedServ)
			{
				if (base.projectile.localAI[1] == 800f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Wasn't my creation though, in fact it was our...", 200, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1000f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("...", 100, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1100f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("... That doesn't matter.\n'Grats! You defeated a black-and-red worm machine thing!", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1400f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("*slow clapping* Very impressive.", 200, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1600f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Now, the next Overlord is a prototype of mine. It's already quite the fighter.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
			}
			if (base.projectile.localAI[1] >= 1900f)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (!Main.dedServ)
				{
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Good luck.", 100, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
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
			RedeWorld.girusTalk2 = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}
	}
}
