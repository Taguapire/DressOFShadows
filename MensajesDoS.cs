using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressOfShadows
{
    internal class MensajesDoS
    {
        private readonly string Mensaje1es = "Los datos introducidos no están completos.";
        private readonly string Mensaje1en = "The data entered is not complete.";

        private readonly string Mensaje2es = "La clave no puede ser mayor a 32 caracteres.";
        private readonly string Mensaje2en = "The password cannot be longer than 32 characters.";

        private readonly string Mensaje3es = "Los datos introducidos no están correctos.";
        private readonly string Mensaje3en = "The data entered is not correct.";

        private readonly string Mensaje4es = "Ingrese los datos a decodificar correctamente";
        private readonly string Mensaje4en = "Enter the data to be decoded correctly.";

        private readonly string Mensaje5es = "Los datos ingresados para decodificar no tienen el formato correcto.";
        private readonly string Mensaje5en = "The data entered for decoding is not formatted correctly.";

        private readonly string Mensaje6es = "Los datos ingresados para decodificar no concuerdan con la clave.";
        private readonly string Mensaje6en = "The data entered for decoding does not match the key.";

        private readonly string Mensaje7es = "La clave no puede ser mayor a 56 Caracteres.";
        private readonly string Mensaje7en = "The key cannot be longer than 56 characters.";

        private readonly string Mensaje8es = "Seleccione una función criptográfica.";
        private readonly string Mensaje8en = "Select a cryptographic function.";

        private readonly string Mensaje9es = "Error o daño en mensaje encriptado.";
        private readonly string Mensaje9en = "Error or damage in encrypted message.";

        public String GetMensaje1(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje1es : Mensaje1en;
            return sSalida;
        }

        public String GetMensaje2(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje2es : Mensaje2en;
            return sSalida;
        }

        public String GetMensaje3(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje3es : Mensaje3en;
            return sSalida;
        }

        public String GetMensaje4(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje4es : Mensaje4en;
            return sSalida;
        }

        public String GetMensaje5(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje5es : Mensaje5en;
            return sSalida;
        }

        public String GetMensaje6(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje6es : Mensaje6en;
            return sSalida;
        }

        public String GetMensaje7(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje7es : Mensaje7en;
            return sSalida;
        }

        public String GetMensaje8(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje8es : Mensaje8en;
            return sSalida;
        }

        public String GetMensaje9(String pIdioma)
        {
            String sSalida;
            sSalida = pIdioma == "es" ? Mensaje9es : Mensaje9en;
            return sSalida;
        }
    }
}
