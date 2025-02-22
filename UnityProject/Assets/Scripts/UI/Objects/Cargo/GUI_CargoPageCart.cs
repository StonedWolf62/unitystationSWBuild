﻿using System.Collections.Generic;
using UnityEngine;
using UI.Core.NetUI;
using Systems.Cargo;

namespace UI.Objects.Cargo
{
	public class GUI_CargoPageCart : GUI_CargoPage
	{
		[SerializeField]
		private NetText_label confirmButtonText;
		[SerializeField]
		private NetText_label totalPriceText;
		[SerializeField]
		private EmptyItemList orderList;

		public override void OpenTab()
		{
			CargoManager.Instance.OnCartUpdate.AddListener(UpdateTab);
		}

		public override void UpdateTab()
		{
			DisplayCurrentCart();
			if (cargoGUI.cargoConsole.CorrectID || cargoGUI.IsAIInteracting())
			{
				confirmButtonText.MasterSetValue(CanAffordCart() ? "CONFIRM CART" : "NOT ENOUGH CREDITS");

				CheckTotalPrice();
				if (CargoManager.Instance.CurrentCart.Count == 0)
				{
					confirmButtonText.MasterSetValue("CART IS EMPTY");
					totalPriceText.MasterSetValue("");
				}
			}
			else
			{
				confirmButtonText.MasterSetValue("InvalidID");
			}

		}

		private void CheckTotalPrice()
		{
			totalPriceText.MasterSetValue($"TOTAL: {CargoManager.Instance.TotalCartPrice()} CREDITS");
		}

		public void ConfirmCart()
		{
			if (CanAffordCart() == false || (cargoGUI.cargoConsole.CorrectID == false && cargoGUI.IsAIInteracting() == false)) return;

			CargoManager.Instance.ConfirmCart();
			cargoGUI.ResetId();
		}

		private bool CanAffordCart()
		{
			return CargoManager.Instance.TotalCartPrice() <= CargoManager.Instance.Credits;
		}

		private void DisplayCurrentCart()
		{
			var currentCart = CargoManager.Instance.CurrentCart;

			orderList.Clear();
			orderList.AddItems(currentCart.Count);

			for (var i = 0; i < currentCart.Count; i++)
			{
				var item = (GUI_CargoCartItem)orderList.Entries[i];
				item.SetValues(currentCart[i]);
				item.gameObject.SetActive(true);
			}

			if (cargoGUI.cargoConsole.CorrectID || cargoGUI.IsAIInteracting())
			{
				confirmButtonText.MasterSetValue("InvalidID");
			}

			CheckTotalPrice();
		}
	}
}
