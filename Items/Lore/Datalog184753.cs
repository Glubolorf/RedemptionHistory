using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog184753 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Lore/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #184753");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'I have basically harvested this planet's titanium dry.]\n[c/aee6f3:The alien tech I found in that strange structure has come in handy,]\n[c/aee6f3:I've augmented it into my spaceship's thrusters, so I can reach planets much faster.]\n[c/aee6f3:Hmm... I should give the ship a name... Well, I called robot-self King Slayer III,]\n[c/aee6f3:so the ship must be just as cool. How about: Ship of the Slayer! Or SoS for short?]\n[c/aee6f3:Well, it's finally time to explore beyond the Vorti Star System.]\n[c/aee6f3:I have 999,493.8 years to go... And my overwhelming pain still hasn't settled.]\n[c/aee6f3:I'll just have to live with it forever now.']");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 9;
		}
	}
}
