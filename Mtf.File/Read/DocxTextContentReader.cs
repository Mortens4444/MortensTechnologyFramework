using System.Text;
using System.Xml;

namespace Mtf.File.Read
{
    public class DocxTextContentReader
    {
        /// <summary>
        /// The docx file after renaming it to zip, contains a file /work/document.xml. This function will provide the text inside it.
        /// </summary>
        /// <param name="filename">The path to the document.xml file</param>
        /// <returns>Plain text in the document</returns>
        public string GetTextContent(string filename)
        {
            var document = new XmlDocument();
            document.Load(filename);
            var resultBuilder = new StringBuilder();
            var elements = document.GetElementsByTagName("t", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
            foreach (XmlNode element in elements)
            {
                resultBuilder.AppendLine(element.InnerText);
            }
            return resultBuilder.ToString();
        }
    }
}
