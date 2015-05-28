using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyMessage = MessageNamespace.Message;

namespace DictionaryClient
{
    public class Client
    {
        public static async Task<MyMessage> GetDictionary(String server, MyMessage message)
        {
            try
            {
                // Create a TcpClient. 
                // Note, for this client to work you need to have a TcpServer  
                // connected to the same address as specified by the server, port 
                // combination.
                Int32 port = 13000;
                TcpClient client = await Task.Run(() => new TcpClient(server, port));

                //await client.ConnectAsync(server, port);

                var data = SerializeToStream(message).ToArray();

                // Get a client stream for reading and writing. 

                NetworkStream stream = client.GetStream();

                //send length of query
                int sendlength = data.Length;
                stream.Write(BitConverter.GetBytes(sendlength), 0, sizeof(int));

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                // Receive the TcpServer.response.

                //buffer to store the response length
                byte[] reslength = new byte[4];
                stream.Read(reslength, 0, sizeof(int));

                // Buffer to store the response bytes.
                data = new Byte[BitConverter.ToInt32(reslength, 0)];

                // Read the first batch of the TcpServer response bytes.
                stream.Read(data, 0, data.Length);

                MyMessage m = (MyMessage)DeserializeFromStream(new MemoryStream(data));

                // Close everything.
                stream.Close();
                client.Close();
                return m;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return null;
        }

        public static MyMessage Connect(String server, MyMessage message)
        {
            try
            {
                // Create a TcpClient. 
                // Note, for this client to work you need to have a TcpServer  
                // connected to the same address as specified by the server, port 
                // combination.
                Int32 port = 13000;
                TcpClient client = new TcpClient(server, port);

                //await client.ConnectAsync(server, port);

                var data = SerializeToStream(message).ToArray();

                // Get a client stream for reading and writing. 

                NetworkStream stream = client.GetStream();

                //send length of query
                int sendlength = data.Length;
                stream.Write(BitConverter.GetBytes(sendlength), 0, sizeof(int));

                // Send the message to the connected TcpServer. 
                stream.Write(data, 0, data.Length);

                // Receive the TcpServer.response.
 
                //buffer to store the response length
                byte[] reslength = new byte[4];
                stream.Read(reslength, 0, sizeof(int));

                // Buffer to store the response bytes.
                data = new Byte[BitConverter.ToInt32(reslength, 0)];

                // Read the first batch of the TcpServer response bytes.
                stream.Read(data, 0, data.Length);

                MyMessage m = (MyMessage) DeserializeFromStream(new MemoryStream(data));

                // Close everything.
                stream.Close();
                client.Close();
                return m;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return null;
        }

        /// <summary>
        /// serializes the given object into memory stream
        /// </summary>
        /// <param name="objectType">the object to be serialized</param>
        /// <returns>The serialized object as memory stream</returns>
        public static MemoryStream SerializeToStream(object objectType)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, objectType);
            return stream;
        }

        /// <summary>
        /// deserializes as an object
        /// </summary>
        /// <param name="stream">the stream to deserialize</param>
        /// <returns>the deserialized object</returns>
        public static object DeserializeFromStream(MemoryStream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            object objectType = formatter.Deserialize(stream);
            return objectType;
        }
    }
}
