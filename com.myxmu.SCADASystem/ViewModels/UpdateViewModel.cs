using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.myxmu.SCADASystem.ViewModels
{
    public class UpdateViewModel<T> : ObservableObject where T : class
    {
        public T Entity { get; }

        public UpdateViewModel(T entity)
        {
            Entity = entity;
        }
    }
}
