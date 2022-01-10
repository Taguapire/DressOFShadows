using System.Linq;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DressOfShadows
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //private Blowfish DressOfShadowsBlackBox;
        private string Global_Compare;
        private string TextoCodigo;
        private string TextoEntrada;
        private string TextoSalida;
        private DressOfShadowsCripto OperacionCripto;

        public MainPage()
        {
            InitializeComponent();
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
        private void ClickEncriptar(object sender, RoutedEventArgs e)
        {
            // Prepararar Black Box
            OperacionCripto = new();
            // Verificar Claves Existentes
            if (TBoxClaveEntrada.Password.Trim().Length > 0)
            {
                TextoCodigo = TBoxClaveEntrada.Password.Trim();
            }

            // Verificar Texto existente
            if (TBoxMensajeEntrada.Text.Trim().Length > 0)
            {
                TextoEntrada = TBoxMensajeEntrada.Text.Trim();
            }

            TextoSalida = OperacionCripto.Encriptar(TextoCodigo, TextoEntrada);
            
            TBoxMensajeSalida.Text = TextoSalida;
        }

        private void ClickDesEncriptar(object sender, RoutedEventArgs e)
        {
            // Prepararar Black Box
            OperacionCripto = new();

            // Verificar Claves Existentes
            if (TBoxClaveEntrada.Password.Trim().Length > 0)
            {
                TextoCodigo = TBoxClaveEntrada.Password.Trim();
            }

            // Verificar Texto existente
            if (TBoxMensajeSalida.Text.Trim().Length > 0)
            {
                TextoSalida = TBoxMensajeSalida.Text.Trim();
            }

            TextoEntrada = OperacionCripto.DesEncriptar(TextoCodigo, TextoSalida);

            TBoxMensajeEntrada.Text = TextoEntrada;
        }

        // Boton de Salida
        private void ClickSalir(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

    }
}
