using System;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ShopifyExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Get a shop's products:
        // This code snippet was taken from a button I made in a Visual Studio 2010 Beta 2 Windows Forms Application.
        // The code GETs and then outputs the XML representing all of the shop's products.
        private void button1_Click(object sender, EventArgs e)
        {
            // Create a request for the URL.         
            WebRequest request = WebRequest.Create("https://a4dfbafe855c5a954e7fece9eb50ca03:16e9f1ae8a60cc13be1415bfa55dfd05@teststorecloud17.myshopify.com/admin/api/2019-10/orders.json");
            //WebRequest request = WebRequest.Create("http://<your_shop>.myshopify.com/admin/products.xml");
            // Set the credentials.
            request.Credentials = new NetworkCredential("<a4dfbafe855c5a954e7fece9eb50ca03>", "<16e9f1ae8a60cc13be1415bfa55dfd05>");
            // Get the response.
            HttpWebResponse response = null;
            try
            {
                // This is where the HTTP GET actually occurs.
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
            // Display the status. You want to see "OK" here.
            MessageBox.Show(response.StatusDescription);

            try
            {
                // Get the stream containing content returned by the server.
                Stream dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content. This is the XML that represents all the products for the shop.
                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                MessageBox.Show(responseFromServer);

                // Cleanup the streams and the response.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
            
            
        }


        //Add a product: Read about - System.Net.WebRequest
        // This code snippet was taken from another button I made in a Visual Studio 2010 Beta 2 Windows Forms Application.
        // The code POSTs the XML representing a new product to a shop. After the code executes, the product should be visible via admin.
        private void button2_Click(object sender, EventArgs e)
        {
            // Create a request for the URL.         
            WebRequest request = WebRequest.Create("https://a4dfbafe855c5a954e7fece9eb50ca03:16e9f1ae8a60cc13be1415bfa55dfd05@teststorecloud17.myshopify.com/admin/api/2019-10/orders.json");
            request.Credentials = new NetworkCredential("<a4dfbafe855c5a954e7fece9eb50ca03>", "<16e9f1ae8a60cc13be1415bfa55dfd05>");

            // This is the JSON that represents the new order. 
            // Note that the double quotes are a way for VS to allow quotes in a string literal
            string toSendJson = 
                
				
            // Prepare the WebRequest to POST 
            request.Method = "POST";
            // This ended up being really important! The code won't work without it
            request.ContentType = "application/xml";

            // Turn the XML into a byte buffer to prepare it for transmission 
            byte[] lbPostBuffer = System.Text.Encoding.UTF8.GetBytes(toSendJson);
            request.ContentLength = lbPostBuffer.Length;
            Stream loPostData = request.GetRequestStream();
            loPostData.Write(lbPostBuffer, 0, lbPostBuffer.Length);
            loPostData.Close();

            HttpWebResponse response = null;
            try
            {
                // This is where the HTTP POST actually occurs.
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
            // Display the status.
            MessageBox.Show(response.StatusDescription);
            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //MessageBox.Show(a.ToString());
            MessageBox.Show(responseFromServer);
            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
