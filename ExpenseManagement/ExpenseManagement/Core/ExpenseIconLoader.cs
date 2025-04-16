using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ExpenseManagement.Views
{
    public class ExpenseIconsLoader
    {
        private Panel _plListIcon;
        private IconButton _selectedIconButton;
        private IconChar? _iconSelect;
        private Dictionary<string, IconButton> _iconButtons;

        public ExpenseIconsLoader(Panel plListIcon)
        {
            _plListIcon = plListIcon;
            _iconButtons = new Dictionary<string, IconButton>();
        }

        public IconChar? LoadIcons(Action<IconChar?> iconSelectedCallback)
        {
            _plListIcon.Controls.Clear();
            _iconButtons.Clear();

            var expenseIcons = new List<(IconChar icon, string name)>
            {
                (IconChar.MoneyBill, "Money Bill"),
                (IconChar.MoneyCheck, "Money Check"),
                (IconChar.CreditCard, "Credit Card"),
                (IconChar.Wallet, "Wallet"),
                (IconChar.Coins, "Coins"),
                (IconChar.CashRegister, "Cash Register"),
                (IconChar.Receipt, "Receipt"),
                (IconChar.ShoppingCart, "Shopping Cart"),
                (IconChar.ShoppingBasket, "Shopping Basket"),
                (IconChar.ShoppingBag, "Shopping Bag"),
                (IconChar.PiggyBank, "Piggy Bank"),
                (IconChar.Donate, "Donate"),
                (IconChar.HandHoldingUsd, "Hand Holding USD"),
                (IconChar.ChartLine, "Chart Line"),
                (IconChar.ChartPie, "Chart Pie"),
                (IconChar.BalanceScale, "Balance Scale"),
                (IconChar.FileInvoiceDollar, "Invoice Dollar"),
                (IconChar.HandHolding, "Hand Holding Money"),
                (IconChar.Bank, "Bank"),
                (IconChar.FileInvoice, "Invoice"),
                (IconChar.ClipboardList, "Clipboard List"),
                (IconChar.Percentage, "Percentage"),
                (IconChar.Calculator, "Calculator"),
                (IconChar.ClipboardCheck, "Clipboard Check"),
                (IconChar.DollarSign, "Dollar Sign"),
                (IconChar.ExchangeAlt, "Exchange"),
                (IconChar.ChartBar, "Chart Bar"),
                (IconChar.Clipboard, "Clipboard"),
                (IconChar.ChartArea, "Chart Area"),
                (IconChar.Gift, "Gift"),
                (IconChar.Tags, "Tags"),
                (IconChar.Tag, "Tag"),
                (IconChar.MoneyBillWave, "Money Bill Wave"),
                (IconChar.MoneyBillAlt, "Money Bill Alt"),
                (IconChar.MoneyCheckAlt, "Money Check Alt"),
                (IconChar.HandHoldingHeart, "Hand Holding Heart"),
                (IconChar.Poll, "Poll"),
                (IconChar.PollH, "Poll Horizontal"),
                (IconChar.HandsHelping, "Hands Helping"),
                (IconChar.UserTie, "User Tie"),
                (IconChar.Users, "Users"),
                (IconChar.Handshake, "Handshake"),
                (IconChar.BusinessTime, "Business Time"),
                (IconChar.Suitcase, "Suitcase"),
                (IconChar.Briefcase, "Briefcase"),
                (IconChar.BalanceScaleRight, "Balance Scale Right"),
                (IconChar.BalanceScaleLeft, "Balance Scale Left"),
                (IconChar.University, "University"),
                (IconChar.Building, "Building"),
                (IconChar.FileContract, "File Contract"),
                (IconChar.FileSignature, "File Signature"),
                (IconChar.File, "File"),
                (IconChar.FileAlt, "File Alt"),
                (IconChar.Bell, "Bell"),
                (IconChar.BellSlash, "Bell Slash"),
                (IconChar.CheckCircle, "Check Circle"),
                (IconChar.ExclamationTriangle, "Exclamation Triangle"),
                (IconChar.CommentDollar, "Comment Dollar"),
                (IconChar.Gavel, "Gavel"),
                (IconChar.Hammer, "Hammer"),
                (IconChar.Industry, "Industry"),
                (IconChar.Store, "Store"),
                (IconChar.StoreAlt, "Store Alt"),
                (IconChar.Truck, "Truck"),
                (IconChar.User, "User"),
                (IconChar.UserCheck, "User Check"),
                (IconChar.UserClock, "User Clock"),
                (IconChar.UserCog, "User Cog"),
                (IconChar.UserEdit, "User Edit"),
                (IconChar.UserMinus, "User Minus"),
                (IconChar.UserPlus, "User Plus"),
                (IconChar.UserShield, "User Shield"),
                (IconChar.UserTimes, "User Times"),
                (IconChar.UsersCog, "Users Cog"),
                (IconChar.HandHolding, "Hand Holding"),
                (IconChar.Hands, "Hands"),
                (IconChar.Sitemap, "Sitemap"),
                (IconChar.ThList, "Th List"),
                (IconChar.ThLarge, "Th Large"),
                (IconChar.List, "List"),
                (IconChar.Tasks, "Tasks"),
                (IconChar.Bars, "Bars"),
                (IconChar.Braille, "Braille"),
                (IconChar.GripLines, "Grip Lines"),
                (IconChar.GripLinesVertical, "Grip Lines Vertical"),
                (IconChar.LayerGroup, "Layer Group"),
                (IconChar.ObjectGroup, "Object Group"),
                (IconChar.ObjectUngroup, "Object Ungroup")
            };

            int x = 5, y = 5;
            int iconSize = 50;
            int spacing = 5;
            int columns = Math.Max(1, _plListIcon.Width / (iconSize + spacing));

            foreach (var (icon, name) in expenseIcons)
            {
                var btnIcon = new IconButton
                {
                    IconChar = icon,
                    IconColor = Color.Black,
                    IconSize = 32,
                    Width = iconSize,
                    Height = iconSize,
                    BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Tag = name
                };

                btnIcon.FlatAppearance.BorderSize = 1;
                btnIcon.Click += (sender, e) => IconButton_Click(sender, e, icon, iconSelectedCallback);

                _plListIcon.Controls.Add(btnIcon);
                _iconButtons[name] = btnIcon;

                btnIcon.Location = new Point(x, y);

                if ((_plListIcon.Controls.Count % columns) == 0)
                {
                    x = 5;
                    y += iconSize + spacing;
                }
                else
                {
                    x += iconSize + spacing;
                }
            }

            return _iconSelect;
        }

        private void IconButton_Click(object sender, EventArgs e, IconChar icon, Action<IconChar?> iconSelectedCallback)
        {
            SelectIcon(icon);
            iconSelectedCallback?.Invoke(_iconSelect);
        }

        public void SelectIconByName(string name)
        {
            if (_iconButtons.TryGetValue(name, out var button))
            {
                SelectIcon(button.IconChar);
            }
        }

        private void SelectIcon(IconChar icon)
        {
            if (_selectedIconButton != null)
            {
                _selectedIconButton.BackColor = Color.White;
            }

            _selectedIconButton = _iconButtons.Values.FirstOrDefault(btn => btn.IconChar == icon);
            if (_selectedIconButton != null)
            {
                _selectedIconButton.BackColor = Color.LightBlue;
                _iconSelect = icon;
            }
        }
    }
}
