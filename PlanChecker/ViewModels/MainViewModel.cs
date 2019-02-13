using ESAPIX.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESAPIX.Common;
using VMS.TPS.Common.Model.API;
using System.Collections.ObjectModel;
using Prism.Commands;
using System.Windows;

namespace PlanChecker.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            EvaluateCommand = new DelegateCommand(Evaluate);
        }

        private void Evaluate()
        {
            MessageBox.Show("CLICKED!");
        }

        public ObservableCollection<PlanContraint> Contraints { get; set; }

        public DelegateCommand EvaluateCommand { get; set; }
 
    }
}
           