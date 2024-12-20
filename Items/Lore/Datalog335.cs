using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog335 : ModItem
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
			base.DisplayName.SetDefault("Data Log #335");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'About a year to go until I reach Nabu III.]\n[c/aee6f3:I am STILL dealing with these damn feelings of hunger and tiredness,]\n[c/aee6f3:and have only been getting worse from here. Humans can last 11 days without sleep,]\n[c/aee6f3:the only thing that stopped them from feeling worse was death.]\n[c/aee6f3:I can't even bloody die from fatigue, I can't starve to death either,]\n[c/aee6f3:I'm just stuck like this... For a million damn years!']");
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
