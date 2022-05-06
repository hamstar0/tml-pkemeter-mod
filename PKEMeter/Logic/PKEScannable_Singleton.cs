﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ModLibsCore.Classes.Loadable;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;


namespace PKEMeter.Logic {
	public partial class PKEScannable : ILoadable {
		internal static PKEScannable Instance => ModContent.GetInstance<PKEScannable>();

		////

		internal static IDictionary<string, PKEScannable> Scannables
			=> PKEScannable.Instance?.SingletonScannables;

		internal static IDictionary<int, ISet<string>> ScannableItems
			=> PKEScannable.Instance?.SingletonScannableItems;



		////////////////

		internal static PKEScannable GetScannable( string name ) {
			return PKEScannable.Scannables?.GetOrDefault( name );
		}

		internal static IDictionary<string, PKEScannable> GetScannables() {
			IDictionary<string, PKEScannable> scannables = PKEScannable.Scannables;
			if( scannables == null ) {
				return new Dictionary<string, PKEScannable>();
			}

			return new Dictionary<string, PKEScannable>( scannables );
		}

		////

		internal static bool SetScannable(
					string name,
					PKEScannable scannable,
					bool allowRepeat,
					bool runIfComplete ) {
			return PKEScannable.Instance.SetScannable_Singleton( name, scannable, allowRepeat, runIfComplete );
		}


		////////////////

		public static bool CompleteScan( string name ) {
			return PKEScannable.Instance.CompleteScan_Singleton( name );
		}



		////////////////

		private IDictionary<string, PKEScannable> SingletonScannables = null;

		private IDictionary<int, ISet<string>> SingletonScannableItems = null;



		////////////////

		void ILoadable.OnModsLoad() {
			this.SingletonScannableItems = new Dictionary<int, ISet<string>>();
			this.SingletonScannables = new Dictionary<string, PKEScannable>();

			/*this.SetScannable_Singleton( "test",
				new PKEScannable(
					canScan: (x, y) => Main.HoverItem.type == ItemID.Wood,
					onScanCompleteAction: () => Main.NewText("done"),
					itemType: ItemID.Wood
				)
			);*/
		}

		void ILoadable.OnPostModsLoad() { }

		void ILoadable.OnModsUnload() { }


		////////////////

		private bool SetScannable_Singleton(
					string name,
					PKEScannable scannable,
					bool allowRepeat,
					bool runIfComplete ) {
			if( this.SingletonScannables?.ContainsKey(name) != false ) {
				return false;
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();

			if( myplayer.AlreadyScanned.Contains(name) ) {
				if( runIfComplete ) {
					scannable.RunScanComplete();
				}

				if( !allowRepeat ) {
					return false;
				}
			}

			//

			this.SingletonScannables[name] = scannable;

			foreach( int iType in scannable.AnyOfItemTypes ) {
				this.SingletonScannableItems.Set2D( iType, name );
			}

			return true;
		}


		////

		private bool CompleteScan_Singleton( string name ) {
			if( this.SingletonScannables?.ContainsKey(name) != true ) {
				return false;
			}

			//

			var scannable = this.SingletonScannables[name];

			scannable.RunScanComplete();

			//

			foreach( int iType in scannable.AnyOfItemTypes ) {
				this.SingletonScannableItems.Remove2D( iType, name );
			}

			if( !this.SingletonScannables.Remove(name) ) {
				return false;
			}

			//

			var myplayer = Main.LocalPlayer.GetModPlayer<PKEMeterPlayer>();
			myplayer.AlreadyScanned.Add( name );

			//

			return true;
		}
	}
}
