using System;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Classes.Loadable;
using HamstarHelpers.Services.Timers;


namespace PKEMeter.Logic {
	public delegate (string text, Color color) PKEText( Player player, Vector2 position );




	partial class PKEMeterLogic : ILoadable {
		public PKEText CurrentText { get; internal set; }


		////////////////

		private int TextScrollPos = -6;



		////////////////

		private void InitializeDefaultText() {
			this.CurrentText = ( _, __ ) => (
				"ALL YOUR BASE ARE BELONG TO US 1337",
				Color.Red * (0.5f + (Main.rand.NextFloat() * 0.5f))
			);

			Timers.SetTimer( 4, true, () => {
				this.TextScrollPos += 2;
				return true;
			} );
		}


		////

		public (string text, Color color, int offset) GetText( Player player, Vector2 position ) {
			(string text, Color color) msg = this.CurrentText?.Invoke(player, position)
				?? ("", Color.White);
			int textWid = msg.text.Length * 8;

			if( this.TextScrollPos > textWid ) {
				this.TextScrollPos = -6;
			}

			return (msg.text, msg.color, this.TextScrollPos);
		}
	}
}
