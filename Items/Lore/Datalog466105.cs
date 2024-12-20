using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog466105 : ModItem
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
			base.DisplayName.SetDefault("Data Log #466105");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Welp, I've reached the next solar system.]\n[c/aee6f3:3 planets have been scanned, which is quite a disappointment...]\n[c/aee6f3:I was hoping for there to be more so I can have more to do.]\n[c/aee6f3:But it's fine I guess, the androids I've created have been keeping me company.]\n[c/aee6f3:I'll go to the planet nearest the habitable zone, 'cos robots have become pretty boring now,]\n[c/aee6f3:and I'm dying to see actual greenery, not some dull frozen wasteland.']");
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
