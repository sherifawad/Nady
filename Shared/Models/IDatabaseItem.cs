using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Models
{
    public interface IDatabaseItem<T>
    {
        T Id { get; set; }
    }
}
