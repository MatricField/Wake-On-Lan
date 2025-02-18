using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using System;
using Wake_On_Lan.ViewModels;

namespace Wake_On_Lan.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Send_Click(object sender, RoutedEventArgs args)
        {
            var btn = (Button)sender;
            ArgumentNullException.ThrowIfNull(DataContext);
            var vm = (MainWindowViewModel)DataContext;
            if (!vm.TrySendMagicPacket())
            {
                FlyoutBase.ShowAttachedFlyout(btn);
            }
        }
    }
}