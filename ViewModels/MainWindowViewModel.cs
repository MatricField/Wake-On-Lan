using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Wake_On_Lan.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private static readonly UdpClient udpClient = new();

        private const int DiscardPort = 9;

        private static void SendMagicPacket(IPAddress address, PhysicalAddress macAddress)
        {
            var payload = new byte[102];
            for (var i = 0; i < 6; i++)
            {
                payload[i] = 0xFF;
            }
            var macBytes = macAddress.GetAddressBytes();
            for (var i = 0; i < 16; i++)
            {
                Array.ConstrainedCopy(macBytes, 0, payload, 6 + 6 * i, macBytes.Length);
            }
            udpClient.Send(payload, new IPEndPoint(address, DiscardPort));
        }

        private string? _IP;

        public string? IP
        {
            get => _IP;
            set => this.SetProperty(ref _IP, value);
        }

        private string? _MAC;

        public string? MAC
        {
            get => _MAC;
            set => this.SetProperty(ref _MAC, value);
        }

        public bool TrySendMagicPacket()
        {
            if (IPAddress.TryParse(IP, out var ipAddress) && PhysicalAddress.TryParse(MAC, out var macAddress))
            {
                SendMagicPacket(ipAddress, macAddress);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
