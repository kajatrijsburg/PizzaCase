using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCase
{
    [DataContractAttribute]
    internal class EncryptedMessage(string message, string iv)
    {
        [DataMember]
        public string message = message;
        [DataMember]
        public string IV = iv;
    }
}
