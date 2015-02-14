using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;


namespace PaladinPharmacyCOMv1.Example.Client.WebService
{
    /// <summary>
    /// Soap Extension that logs all incomming and outgoing soap requests. 
    /// </summary>
    /// Server client side implementation.
    ///
    /// References:
    /// Using SoapExtensions: http://msdn2.microsoft.com/en-us/magazine/cc164007.aspx
    /// Unhandled Exception Handling SoapExtension: http://www.codeproject.com/KB/aspnet/ASPNETExceptionHandling.aspx
    /// </remarks>
    public class PharmacyCOMServiceLogger : SoapExtension
    {
        public static Action<string> RequestHandler;
        public static Action<string> ResponseHandler;

        //..............................................................................................................
        //..............................................................................................................

        Stream m_originalStream = null;
        MemoryStream m_workingStream = null;

        //..............................................................................................................
        //..............................................................................................................

        #region  Methods

        //..............................................................................................................

        public override object GetInitializer(Type serviceType)
        {
            return null;
        }

        //..............................................................................................................

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return null;
        }

        //..............................................................................................................

        public override void Initialize(object initializer)
        {
            //Do nothing
            return;
        }

        //..............................................................................................................

        /// <summary>
        /// Chain stream allows Soap Extension to hook into the Soap Pipeline in 
        /// order to manipulate data comming into and out of the Soap Pipeline.
        /// </summary>
        /// <param name="stream">Original stream used by SoapMessage.</param>
        public override Stream ChainStream(Stream stream)
        {
            //Save orignal stream
            m_originalStream = stream;

            //Create new stream and save it
            m_workingStream = new MemoryStream();

            //Return new stream for soap pipleline to use
            return m_workingStream;
        }

        //..............................................................................................................

        /// <summary>
        /// Processes the incomming and outgoing SOAP messages.
        /// </summary>
        /// <param name="message">SoapMessage to process.</param>
        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeDeserialize:
                    //Copy orignal stream into working stream before the soap message is deserialized
                    Copy(m_originalStream, m_workingStream);
                    m_workingStream.Position = 0;
                    LogSoapMessage(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    LogSoapMessage(message);
                    break;
                case SoapMessageStage.BeforeSerialize:
                    //Do nothing
                    //LogSoapMessage(message);
                    break;
                case SoapMessageStage.AfterSerialize:
                    LogSoapMessage(message);

                    //Copy working stream contents into orignal stream to be sent
                    m_workingStream.Position = 0;
                    Copy(m_workingStream, m_originalStream);
                    break;
            }
        }

        //..............................................................................................................

        protected void LogSoapMessage(SoapMessage message)
        {
            string log = GetSoapMessageDetails(message);

            //Figure out message direction
            bool request = true;
            if (message.Stage == SoapMessageStage.AfterDeserialize
                || message.Stage == SoapMessageStage.BeforeDeserialize)
            {
                if (ResponseHandler != null) { ResponseHandler(log); }
            }
            else
            {
                if (RequestHandler != null) { RequestHandler(log); }
            }
        }

        /// <summary>
        /// Gets the details of the <see cref="SoapMessage"/> to be logged.
        /// </summary>
        /// <param name="message">SoapMessage to get details of.</param>
        /// <returns>String that can be logged containting the details of a SoapMessge.</returns>
        protected string GetSoapMessageDetails(SoapMessage message)
        {
            StringBuilder log = new StringBuilder();
            string msgSentOrRecieved = String.Empty;

            //Figure out message direction
            if (message.Stage == SoapMessageStage.AfterDeserialize
                || message.Stage == SoapMessageStage.BeforeDeserialize)
            {
                msgSentOrRecieved = "Received";
            }
            else { msgSentOrRecieved = "Sent"; }


            log.AppendLine(string.Format("PharmacyCOM Soap Message {0}:", msgSentOrRecieved));
            log.AppendLine();
            log.AppendLine(string.Format("URL: {0}", message.Url));
            log.AppendLine(string.Format("Action: {0}", message.Action));
            log.AppendLine(string.Format("Version: {0}", message.SoapVersion));
            log.AppendLine(string.Format("One-way: {0}", message.OneWay));
            //log.AppendLine(string.Format("ContentType: {0}", message.ContentType));
            //log.AppendLine(string.Format("ContentEncoding: {0}", message.ContentEncoding));
            if (message.Headers.Count > 0)
            {
                log.AppendLine(string.Format("Headers: "));
                foreach (SoapHeader header in message.Headers)
                {
                    log.AppendLine("\t" + header.ToString());
                }
            }
            log.AppendLine();
            log.AppendLine(SoapMessageToXMLString(message));
            log.AppendLine();

            //Return results
            return log.ToString();
        }

        //..............................................................................................................

        /// <summary>
        /// Gets a SOAP message XML contents
        /// </summary>
        /// <param name="message">SoapMessage to get XML from.</param>
        /// <returns>string containing the XML version of a <see cref="SoapMessage"/>.</returns>
        private string SoapMessageToXMLString(SoapMessage message)
        {
            StringBuilder xml = new StringBuilder();
            XmlDocument xmlDoc = new XmlDocument();
            long originalPosition = m_workingStream.Position;
            try
            {
                //Prepare working stream
                m_workingStream.Seek(0, SeekOrigin.Begin);

                //Load working stream into XmlDocument
                m_workingStream.Seek(0, SeekOrigin.Begin);
                TextReader reader = new StreamReader(m_workingStream);
                try
                {
                    xmlDoc.Load(reader);

                    //Setup XmlWriter Settings for use during xml doc output
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineChars = System.Environment.NewLine;

                    //Output Xml Document to string builder
                    using (XmlWriter xmlWriter = XmlWriter.Create(xml, xmlWriterSettings))
                    {
                        xmlDoc.WriteTo(xmlWriter);
                    }
                }
                catch
                {
                    StringBuilder sb = new StringBuilder(reader.ReadToEnd());
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                string errorMsg = String.Format("Error in PhamacyCOM Service Logger: {0}{1}", ex.Message, Environment.NewLine);
                xml.Insert(0, errorMsg);
            }
            finally
            {
                //Return to orignal position
                m_workingStream.Position = originalPosition;
            }

            //Return XML as string
            return xml.ToString();
        }

        //..............................................................................................................

        /// <summary>
        /// Copies one stream into another stream.
        /// </summary>
        /// <param name="streamFrom">Stream to get data from.</param>
        /// <param name="streamTo">Stream to put data into.</param>
        public void Copy(Stream from, Stream to)
        {
            //Read stream from into stream to
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);
            writer.Write(reader.ReadToEnd());
            writer.Flush();
        }

        //..............................................................................................................
        //..............................................................................................................

        #endregion

        //..............................................................................................................
        //..............................................................................................................
    }
}
