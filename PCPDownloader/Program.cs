using System;
using System.Xml;
using System.Net;
using System.IO;

namespace PCPDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            //just in case there is an error
            try
            {
                //sets loop count to 0
                int count = 0;

                //creates web client
                WebClient client = new WebClient();

                //sets xml reader settings and creates the xml reader
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = DtdProcessing.Ignore;
                XmlReader xmlReader = XmlReader.Create("http://pcpcomic.ucam.org/navcomic.html", settings);

                //parses the xml
                while (xmlReader.Read())
                {
                    //if it finds an img tag
                    if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "img"))
                    {
                        //if it has any attributes
                        if (xmlReader.HasAttributes)
                        {
                            //increments the counter each time it finds a src value for the img tag
                            count++;
                            //creates the directory if it does not already exist
                            Directory.CreateDirectory(@"PCP");
                            //sets filename for the strip using the counter value
                            string localFilename = @"PCP\strip" + count.ToString() + ".png";
                            //downloads the strip using the img src element
                            client.DownloadFile(xmlReader.GetAttribute("src"), localFilename);
                            //outputs which strip it downloaded
                            Console.WriteLine("Downloaded Strip "+ count.ToString());
                        }
                    }
                }
            }
            //oops
            catch
            {
                //continue even if there is an error
            }
        }
    }
}
