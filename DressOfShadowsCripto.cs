using System;
using System.Text;
using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Utilities.Encoders;

namespace DressOfShadows
{
    /// <summary>
    /// Class that provides blowfish encryption.
    /// </summary>
    /// 

    // Engines de BounceCastle
    // AESFastEngine
    // 

    public class DressOfShadowsCripto
    {
        private const int KEY256BIT = 32;
        private const int KEY448BIT = 56;

        private static readonly Encoding Encoding = Encoding.UTF8;
        public string ErrorDeRetorno { get; set; }

        public DressOfShadowsCripto()
        {
        }

        // ************************************************************
        // Procedimientos AES Encriptar
        // ************************************************************
        public string AESEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (key.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword256(key);

            try
            {
                AesEngine engine = new();

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(KeyGenerada));

                cipher.Init(true, keyBytes);


                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                _ = cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos AES Desencriptar
        // ************************************************************
        public string AESDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (keyString.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword256(keyString);

            AesEngine engine = new();

            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(KeyGenerada)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos BlowFish Encriptar
        // ************************************************************
        public string BlowfishEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (key.Length > KEY448BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 56 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword448(key);

            try
            {
                BlowfishEngine engine = new();

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(KeyGenerada));

                cipher.Init(true, keyBytes);

                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                _ = cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception)
            {
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos BlowFish DesEncriptar
        // ************************************************************
        public string BlowfishDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (keyString.Length > KEY448BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 56 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword448(keyString);

            BlowfishEngine engine = new();

            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(KeyGenerada)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos Rijndael Encriptar
        // ************************************************************
        public string RijndaelEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (key.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword256(key);

            try
            {
                RijndaelEngine engine = new(256);

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(KeyGenerada));

                cipher.Init(true, keyBytes);


                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                _ = cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos Rijndael DesEncriptar
        // ************************************************************
        public string RijndaelDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (keyString.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword256(keyString);

            RijndaelEngine engine = new(256);

            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(KeyGenerada)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos TwoFish Encriptar
        // ************************************************************
        public string TwofishEncrypt(string strValue, string key)
        {
            ErrorDeRetorno = "";

            if (strValue == "" || key == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (key.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword256(key);

            try
            {
                TwofishEngine engine = new();

                PaddedBufferedBlockCipher cipher = new(engine);

                KeyParameter keyBytes = new(Encoding.GetBytes(KeyGenerada));

                cipher.Init(true, keyBytes);

                byte[] inB = Encoding.GetBytes(strValue);

                byte[] outB = new byte[cipher.GetOutputSize(inB.Length)];

                int len1 = cipher.ProcessBytes(inB, 0, inB.Length, outB, 0);

                cipher.DoFinal(outB, len1);

                return BitConverter.ToString(outB).Replace("-", "");
            }
            catch (Exception)
            {
                ErrorDeRetorno = "Los datos ingresados estan incorrectos";
                return "";
            }
        }

        // ************************************************************
        // Procedimientos TwoFish DesEncriptar
        // ************************************************************
        public string TwofishDecrypt(string name, string keyString)
        {
            ErrorDeRetorno = "";

            if (name == "" || keyString == "")
            {
                ErrorDeRetorno = "Los datos ingresados estan incompletos";
                return "";
            }

            if (keyString.Length > KEY256BIT)
            {
                ErrorDeRetorno = "La clave no puede ser mayor a 32 Caracteres";
                return "";
            }

            string KeyGenerada = GenerarPassword256(keyString);

            TwofishEngine engine = new();

            PaddedBufferedBlockCipher cipher = new(engine);

            StringBuilder result = new();

            try
            {
                cipher.Init(false, new KeyParameter(Encoding.GetBytes(KeyGenerada)));
            }
            catch (IndexOutOfRangeException)
            {
                ErrorDeRetorno = "Ingrese los datos a decodificar correctamente";
                return "";
            }

            byte[] out1;

            try
            {
                out1 = Hex.Decode(name);
            }
            catch (System.IO.IOException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no tienen el formato correcto.";
                return "";
            }

            byte[] out2 = new byte[cipher.GetOutputSize(out1.Length)];

            int len2 = cipher.ProcessBytes(out1, 0, out1.Length, out2, 0);

            try
            {
                _ = cipher.DoFinal(out2, len2); //Pad block corrupted error happens here
            }
            catch (DataLengthException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }
            catch (InvalidCipherTextException)
            {
                ErrorDeRetorno = "Los datos ingresados a decodificar no concuerdan con la clave";
                return "";
            }

            string s2 = BitConverter.ToString(out2);

            for (int i = 0; i < s2.Length; i++)
            {
                char c = s2[i];
                if (c != 0)
                {
                    _ = result.Append(c.ToString());
                }
            }

            ErrorDeRetorno = "";

            return FromHexString(result.ToString().Replace("-", ""));
        }

        // ************************************************************
        // Procedimientos Utilitarios
        // ************************************************************
        private string FromHexString(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.GetString(bytes);
        }

        private string GenerarPassword256(string pPassword)
        {
            char separator = ',';

            byte[] LvSalt = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

            Argon2Config config = new()
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                Password = Encoding.ASCII.GetBytes(pPassword),
                Salt = LvSalt, // >= 8 bytes if not null
                //Secret = secret, // from somewhere
                //AssociatedData = associatedData, // from somewhere
                HashLength = 13 // >= 4
            };

            Argon2 argon2A = new(config);

            string hashString;

            using (SecureArray<byte> hashA = argon2A.Hash())
            {
                hashString = config.EncodeString(hashA.Buffer);
            }

            string[] divisor = hashString.Split(separator);

            return divisor[2].Substring(2);
        }

        private string GenerarPassword448(string pPassword)
        {
            char separator = ',';

            byte[] LvSalt = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

            Argon2Config config = new()
            {
                Type = Argon2Type.DataIndependentAddressing,
                Version = Argon2Version.Nineteen,
                TimeCost = 10,
                MemoryCost = 32768,
                Lanes = 5,
                Threads = Environment.ProcessorCount, // higher than "Lanes" doesn't help (or hurt)
                Password = Encoding.ASCII.GetBytes(pPassword),
                Salt = LvSalt, // >= 8 bytes if not null
                //Secret = secret, // from somewhere
                //AssociatedData = associatedData, // from somewhere
                HashLength = 31 // >= 4
            };

            Argon2 argon2A = new(config);

            string hashString;

            using (SecureArray<byte> hashA = argon2A.Hash())
            {
                hashString = config.EncodeString(hashA.Buffer);
            }

            string[] divisor = hashString.Split(separator);

            return divisor[2].Substring(2);
        }
    }
}
