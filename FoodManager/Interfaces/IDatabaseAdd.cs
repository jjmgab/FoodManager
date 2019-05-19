using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManager.Interfaces
{
    interface IDatabaseAdd<T>
    {
        void AddToDatabase(T item);
        bool ValidateItem(T item);
    }
}
