using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	public class GirusTalking1 : ModProjectile
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
			Player player = Main.LocalPlayer;
			base.projectile.localAI[1] += 1f;
			if (base.projectile.localAI[1] == 540f)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				if (!Main.dedServ)
				{
					Redemption.Inst.DialogueUIElement.DisplayDialogue("...", 100, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				Redemption.GirusSilence = true;
			}
			if (!Main.dedServ)
			{
				if (base.projectile.localAI[1] == 640f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("...Hm?", 260, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 900f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("Would you look at that, an Overlord Unit has been taken down.", 400, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1300f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					Redemption.Inst.DialogueUIElement.DisplayDialogue("What a shame. That one was almost useful.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
				}
				if (base.projectile.localAI[1] == 1600f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("You know, I never expected one of my own to take down an Overlord..?", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("You know, I never expected someone like you to take down an Overlord.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
				}
				if (base.projectile.localAI[1] == 1900f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Huh... Very interesting. I never expected this.", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Keep on going. It's rather interesting to see you attempt such a feat...", 300, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
				}
				if (base.projectile.localAI[1] == 2100f)
				{
					Main.PlaySound(12, -1, -1, 1, 1f, 0f);
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower || player.IsFullTBot())
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Anyway, carry on your way, my underling. Hmm...", 200, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
					else
					{
						Redemption.Inst.DialogueUIElement.DisplayDialogue("Hehehehe...", 200, 1, 0.6f, "???:", 1f, new Color?(RedeColor.GirusTier), null, null, null, 0, 0);
					}
				}
			}
			if (base.projectile.localAI[1] >= 2300f)
			{
				base.projectile.Kill();
			}
			if (MoRDialogueUI.Visible)
			{
				Redemption.Inst.DialogueUIElement.TextColor = new Color?(RedeColor.GirusTier);
			}
		}

		public override void Kill(int timeleft)
		{
			RedeWorld.girusTalk1 = true;
			if (Main.netMode != 0)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			Redemption.GirusSilence = false;
		}
	}
}
