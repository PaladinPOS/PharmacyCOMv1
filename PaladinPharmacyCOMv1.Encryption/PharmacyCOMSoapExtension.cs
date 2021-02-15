using System;
using System.IO;
using System.Web.Services.Protocols;

namespace PaladinPharmacyCOMv1.Encryption
{
    class PharmacyCOMSoapExtension : SoapExtension
    {
        private Stream inStream;
        private Stream outStream;

        public override Stream ChainStream(Stream stream)
        {
            outStream = stream;
            inStream = new MemoryStream();
            return inStream;
        }
        public override object GetInitializer(Type serviceType)
        {
            return null;
        }
        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }
        public override void Initialize(object initializer)
        {
            return;
        }
        public override void ProcessMessage(SoapMessage message)
        {
            string soapMsg;
            StreamReader sRdr;
            StreamWriter sWrt;

            bool encryptionEnabled = false; //read from settings file
            string password = "PalPharmCom"; //read from settings file
            var encryption = new PharmacyCOMEncryption(password);

            switch (message.Stage)
            {
                case SoapMessageStage.BeforeDeserialize:

                    sRdr = new StreamReader(outStream);
                    sWrt = new StreamWriter(inStream);

                    if (encryptionEnabled)
                    {
                        soapMsg = encryption.Decrypt(sRdr.ReadToEnd());
                    }
                    else
                    {
                        soapMsg = sRdr.ReadToEnd();
                    }

                    sWrt.Write(soapMsg);
                    sWrt.Flush();

                    inStream.Position = 0;
                    
                    break;

                case SoapMessageStage.AfterSerialize:

                    inStream.Position = 0;

                    sRdr = new StreamReader(inStream);
                    sWrt = new StreamWriter(outStream);

                    if (encryptionEnabled)
                    {
                        soapMsg = encryption.Encrypt(sRdr.ReadToEnd());
                    }
                    else
                    {
                        soapMsg = sRdr.ReadToEnd();
                    }

                    sWrt.Write(soapMsg);
                    sWrt.Flush();

                    break;

                default:
                    break;
            }
        }
    }
}
