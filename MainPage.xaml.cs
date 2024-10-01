using QRCoder;
using System;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DressOfShadows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string TextoCodigo = "";
        private string TextoEntrada = "";
        private string TextoSalida = "";
        private DressOfShadowsCripto OperacionCripto;
        private Herramientas VarHerramientas = new();
        private ApplicationView AplicacionPrincipal;
        private MensajesDoS XMensajesDos = new();

        public MainPage()
        {
            InitializeComponent();
            AplicacionPrincipal = ApplicationView.GetForCurrentView();
            //AplicacionPrincipal.Title = "Dress Of Shadows";
            VarHerramientas.BuscarIdioma();
            EstablecerTextoControles();
            ApplicationView.PreferredLaunchViewSize = new Size(720, 510);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        // Muestra Password
        private void ClibkBtnVerPassword(object sender, RoutedEventArgs e)
        {
            if (TBoxClaveEntrada.PasswordRevealMode == PasswordRevealMode.Peek)
            {
                TBoxClaveEntrada.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else if (TBoxClaveEntrada.PasswordRevealMode == PasswordRevealMode.Hidden)
            {
                TBoxClaveEntrada.PasswordRevealMode = PasswordRevealMode.Visible;
            }
            else if (TBoxClaveEntrada.PasswordRevealMode == PasswordRevealMode.Visible)
            {
                TBoxClaveEntrada.PasswordRevealMode = PasswordRevealMode.Hidden;
            }
        }

        // Encriptar Mensaje
        private async void ClickEncriptar(object sender, RoutedEventArgs e)
        {
            // Prepararar Black Box
            OperacionCripto = new();

            // Verificar Claves Existentes
            TextoCodigo = TBoxClaveEntrada.Password.Trim().Length > 0 ? TBoxClaveEntrada.Password.Trim() : "";

            // Verificar Texto existente
            TextoEntrada = TBoxMensajeEntrada.Text.Trim().Length > 0 ? TBoxMensajeEntrada.Text.Trim() : "";

            if ((bool)ChBoxAES.IsChecked)
            {
                TextoSalida = OperacionCripto.AESEncrypt(TextoEntrada, TextoCodigo);
            }
            else if ((bool)ChBoxBlowFish.IsChecked)
            {
                TextoSalida = OperacionCripto.BlowfishEncrypt(TextoEntrada, TextoCodigo);
            }
            else if ((bool)ChBoxRijndael.IsChecked)
            {
                TextoSalida = OperacionCripto.RijndaelEncrypt(TextoEntrada, TextoCodigo);
            }
            else if ((bool)ChBoxTwoFish.IsChecked)
            {
                TextoSalida = OperacionCripto.TwofishEncrypt(TextoEntrada, TextoCodigo);
            }
            else
            {
                MessageDialog LvrMessageDialog = new(XMensajesDos.GetMensaje8(VarHerramientas.GetIdiomaTwoLetterISOLanguageName()));
                _ = await LvrMessageDialog.ShowAsync();
                return;
            }

            if (OperacionCripto.ErrorDeRetorno == "")
            {
                TBoxMensajeSalida.Text = TextoSalida;
            }
            else
            {
                MessageDialog LvrMessageDialog = new(OperacionCripto.ErrorDeRetorno);
                _ = await LvrMessageDialog.ShowAsync();
            }
        }

        private async void ClickDesEncriptar(object sender, RoutedEventArgs e)
        {
            // Prepararar Black Box
            OperacionCripto = new();

            // Verificar Claves Existentes
            TextoCodigo = TBoxClaveEntrada.Password.Trim().Length > 0 ? TBoxClaveEntrada.Password.Trim() : "";

            // Verificar Texto existente
            TextoSalida = TBoxMensajeSalida.Text.Trim().Length > 0 ? TBoxMensajeSalida.Text.Trim() : "";

            if ((bool)ChBoxAES.IsChecked)
            {
                TextoEntrada = OperacionCripto.AESDecrypt(TextoSalida, TextoCodigo);
            }
            else if ((bool)ChBoxBlowFish.IsChecked)
            {
                TextoEntrada = OperacionCripto.BlowfishDecrypt(TextoSalida, TextoCodigo);
            }
            else if ((bool)ChBoxRijndael.IsChecked)
            {
                TextoEntrada = OperacionCripto.RijndaelDecrypt(TextoSalida, TextoCodigo);
            }
            else if ((bool)ChBoxTwoFish.IsChecked)
            {
                TextoEntrada = OperacionCripto.TwofishDecrypt(TextoSalida, TextoCodigo);
            }
            else
            {
                MessageDialog LvrMessageDialog = new(XMensajesDos.GetMensaje8(VarHerramientas.GetIdiomaTwoLetterISOLanguageName()));
                _ = await LvrMessageDialog.ShowAsync();
                return;
            }

            if (OperacionCripto.ErrorDeRetorno == "")
            {
                TBoxMensajeEntrada.Text = TextoEntrada;
            }
            else
            {
                MessageDialog LvrMessageDialog = new(OperacionCripto.ErrorDeRetorno);
                _ = await LvrMessageDialog.ShowAsync();
            }
        }

        // Boton de Salida
        private void ClickSalir(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void ClickBtnVerQR(object sender, RoutedEventArgs e)
        {
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(TextoSalida, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrCodeData);

            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(20);

            InMemoryRandomAccessStream stream = new();

            DataWriter writer = new(stream.GetOutputStreamAt(0));
            writer.WriteBytes(qrCodeAsPngByteArr);
            await writer.StoreAsync();
            var image = new BitmapImage();
            await image.SetSourceAsync(stream);

            imagenQRMensaje.Source = image;
        }

        private void AsignarMotorCripto(object sender, RoutedEventArgs e)
        {
            CheckBox CualChequeado = new();

            CualChequeado = (CheckBox)sender;

            switch ((string)CualChequeado.Content)
            {
                case "AES":
                    ChBoxAES.IsChecked = true;
                    ChBoxBlowFish.IsChecked = false;
                    ChBoxRijndael.IsChecked = false;
                    ChBoxTwoFish.IsChecked = false;
                    break;
                case "BlowFish":
                    ChBoxAES.IsChecked = false;
                    ChBoxBlowFish.IsChecked = true;
                    ChBoxRijndael.IsChecked = false;
                    ChBoxTwoFish.IsChecked = false;
                    break;
                case "Rijndael":
                    ChBoxAES.IsChecked = false;
                    ChBoxBlowFish.IsChecked = false;
                    ChBoxRijndael.IsChecked = true;
                    ChBoxTwoFish.IsChecked = false;
                    break;
                case "TwoFish":
                    ChBoxAES.IsChecked = false;
                    ChBoxBlowFish.IsChecked = false;
                    ChBoxRijndael.IsChecked = false;
                    ChBoxTwoFish.IsChecked = true;
                    break;
            }
        }
        private void EstablecerTextoControles()
        {
            if (VarHerramientas.GetIdiomaTwoLetterISOLanguageName() == "es")
            {
                TBoxMensajeEntrada.Header = "Mensaje Normal";
                TBoxMensajeSalida.Header = "Mensaje Encriptado";
                BtnEncriptar.Content = "Encriptar";
                BtnDesencriptar.Content = "Desencriptar";
                BtnSalir.Content = "Salida";
                TBoxClaveEntrada.Header = "Ingrese Clave para Encriptar";
                BtnVerPassword.Content = "Mostrar";
                BtnVerQR.Content = "Mostrar QR";
            }
            else
            {
                TBoxMensajeEntrada.Header = "Normal message";
                TBoxMensajeSalida.Header = "Encrypted Message";
                BtnEncriptar.Content = "Encrypt";
                BtnDesencriptar.Content = "Decrypt";
                BtnSalir.Content = "Exit";
                TBoxClaveEntrada.Header = "Enter Key to Encrypt";
                BtnVerPassword.Content = "Show";
                BtnVerQR.Content = "Show QR";
            }
        }
    }
}