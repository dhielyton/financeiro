using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle.Financeiro.Domain.Kernel
{
    public abstract class Entity
    {
        public string Id { get; private set; }

        public void GenerateId()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
