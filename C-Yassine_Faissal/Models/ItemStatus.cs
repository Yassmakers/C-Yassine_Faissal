using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Yassine_Faissal.Models
{
    // Deze enumeratie representeert de status van een item en wordt gebruikt in de Item.cs-klasse.
    public enum ItemStatus
    {
        Available,
        Borrowed,
        Reserved,
        Maintenance
    }
}

