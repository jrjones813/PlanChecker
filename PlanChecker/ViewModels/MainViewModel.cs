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
using ESAPIX.Constraints.DVH;
using Cardan.PlanChecker;

namespace PlanChecker.ViewModels
{
    public class MainViewModel : BindableBase
    {
        AppComThread VMS = AppComThread.Instance;

        public MainViewModel()
        {
            EvaluateCommand = new DelegateCommand(Evaluate);
            CreateConstraints();
        }

        private void CreateConstraints()
        {
            Constraints.AddRange(new PlanConstraint[]
                        {
                new PlanConstraint(ConstraintBuilder.Build("PTV", "Max[%] <= 110")),
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V75Gy[%] <= 15")),
                new PlanConstraint(ConstraintBuilder.Build("Rectum", "V65Gy[%] <= 35")),
                new PlanConstraint(ConstraintBuilder.Build("Bladder", "V80Gy[%] <= 15")),
                            //new PlanConstraint(new CTDateConstraint())
                        });
        }

        private void Evaluate()
        {
            foreach (var pc in Constraints)
            {
                var result = VMS.GetValue(sc =>
                {
                    //Check if we can constrain first
                    var canConstrain = pc.Constraint.CanConstrain(sc.PlanSetup);
                    //If not..report why
                    if (!canConstrain.IsSuccess) { return canConstrain; }
                    else
                    {
                        //Can constrain - so do it
                        return pc.Constraint.Constrain(sc.PlanSetup);
                    }
                });
                //Update UI
                pc.Result = result;
            }
        }

        public ObservableCollection<PlanConstraint> Constraints { get; set; } = new ObservableCollection<PlanConstraint>();

        public DelegateCommand EvaluateCommand { get; set; }

    }
}
